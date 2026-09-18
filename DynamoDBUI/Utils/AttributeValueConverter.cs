using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBUI.Utils
{
    /// <summary>
    /// Konversi antara AttributeValue (DynamoDB) <-> tipe .NET biasa.
    /// </summary>
    public static class AttributeValueConverter
    {
        // Pola angka valid untuk DynamoDB N (tanpa notasi ilmiah/pemisah ribuan),
        // dicek lewat regex saja (bukan double.Parse) supaya digit besar
        // (mis. ID berbasis timestamp 16+ digit) tidak kehilangan presisi.
        private static readonly Regex NumericPattern = new Regex(@"^[+-]?\d+(\.\d+)?$", RegexOptions.Compiled);

        public static string ToDisplayString(AttributeValue av)
        {
            if (av == null) return string.Empty;
            if (av.S != null) return av.S;
            if (av.N != null) return av.N;
            if (av.BOOL != null) return av.BOOL.ToString();
            if (av.NULL == true) return "NULL";
            if (av.SS != null && av.SS.Count > 0) return string.Join(",", av.SS);
            if (av.NS != null && av.NS.Count > 0) return string.Join(",", av.NS);
            if (av.M != null && av.M.Count > 0) return "[Map]";
            if (av.L != null && av.L.Count > 0) return "[List]";
            return string.Empty;
        }

        /// <summary>
        /// Bangun DataTable dari hasil Scan/Query, dengan tipe kolom otomatis
        /// (Number -> double supaya bisa di-sort numerik, selain itu string).
        /// </summary>
        public static DataTable BuildDataTable(List<Dictionary<string, AttributeValue>> items)
        {
            var dt = new DataTable();

            var columnNames = items
                .SelectMany(item => item.Keys)
                .Distinct()
                .ToList();

            foreach (var col in columnNames)
            {
                // Deteksi tipe kolom dari item pertama yang punya kolom ini
                var sample = items.FirstOrDefault(i => i.ContainsKey(col));
                Type colType = typeof(string);
                if (sample != null && sample[col].N != null)
                {
                    colType = typeof(double);
                }
                dt.Columns.Add(col, colType);
            }

            foreach (var item in items)
            {
                var row = dt.NewRow();
                foreach (var col in columnNames)
                {
                    if (!item.ContainsKey(col))
                    {
                        row[col] = DBNull.Value;
                        continue;
                    }

                    var av = item[col];

                    // Attribute NULL DynamoDB (bukan "missing") -> selalu DBNull,
                    // apapun tipe kolomnya.
                    if (av.NULL == true)
                    {
                        row[col] = DBNull.Value;
                        continue;
                    }

                    if (dt.Columns[col].DataType == typeof(double))
                    {
                        // Kolom di-set double karena row lain punya angka di sini,
                        // tapi row ini ternyata bukan number (data schemaless yang
                        // tipenya campuran antar item) -> DBNull daripada crash.
                        row[col] = av.N != null
                            ? (object)double.Parse(av.N, System.Globalization.CultureInfo.InvariantCulture)
                            : DBNull.Value;
                    }
                    else
                    {
                        row[col] = ToDisplayString(av);
                    }
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// Ubah string mentah dari query editor jadi AttributeValue,
        /// dengan inferensi tipe: angka -> N, selain itu -> S.
        /// Dipakai untuk INSERT, di mana tipe kolom harus pasti (satu AttributeValue).
        /// </summary>
        public static AttributeValue InferAttributeValue(string rawValue)
        {
            string trimmedRaw = rawValue.Trim();
            bool isQuoted = trimmedRaw.StartsWith("'") || trimmedRaw.StartsWith("\"");
            var value = CsvLineParser.TrimQuotes(trimmedRaw);

            if (!isQuoted && NumericPattern.IsMatch(value))
            {
                // Simpan digit apa adanya (bukan hasil re-format double) supaya angka
                // besar (mis. ID berbasis timestamp 16+ digit) tidak berubah jadi
                // notasi ilmiah atau kehilangan presisi.
                return new AttributeValue { N = value };
            }

            if (value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return new AttributeValue { BOOL = bool.Parse(value) };
            }

            return new AttributeValue { S = value };
        }

        /// <summary>
        /// Sama seperti <see cref="InferAttributeValue"/>, tapi untuk FINDBY (filter).
        /// DynamoDB itu schemaless, jadi kolom yang sama bisa saja disimpan sebagai
        /// Number di satu item dan String di item lain (contoh umum: ID berbasis
        /// timestamp yang terlihat seperti angka tapi sebenarnya disimpan sebagai
        /// String). Kalau value tidak diberi tanda kutip dan terlihat seperti angka,
        /// kembalikan KEDUA kandidat tipe (N dan S) supaya FINDBY tetap ketemu
        /// walau tipe aslinya beda dari dugaan. Kalau user memberi tanda kutip
        /// eksplisit (mis. FINDBY id = '123'), paksa String saja.
        /// </summary>
        public static List<AttributeValue> InferAttributeValueCandidates(string rawValue)
        {
            string trimmedRaw = rawValue.Trim();
            bool isQuoted = trimmedRaw.StartsWith("'") || trimmedRaw.StartsWith("\"");
            var value = CsvLineParser.TrimQuotes(trimmedRaw);

            if (!isQuoted && value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                !isQuoted && value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return new List<AttributeValue> { new AttributeValue { BOOL = bool.Parse(value) } };
            }

            if (!isQuoted && NumericPattern.IsMatch(value))
            {
                return new List<AttributeValue>
                {
                    new AttributeValue { N = value },
                    new AttributeValue { S = value }
                };
            }

            return new List<AttributeValue> { new AttributeValue { S = value } };
        }
    }
}