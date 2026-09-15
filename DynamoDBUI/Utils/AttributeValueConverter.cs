using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBUI.Utils
{
    /// <summary>
    /// Konversi antara AttributeValue (DynamoDB) <-> tipe .NET biasa.
    /// </summary>
    public static class AttributeValueConverter
    {
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
                    if (dt.Columns[col].DataType == typeof(double) && av.N != null)
                    {
                        row[col] = double.Parse(av.N, System.Globalization.CultureInfo.InvariantCulture);
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
        /// </summary>
        public static AttributeValue InferAttributeValue(string rawValue)
        {
            var value = CsvLineParser.TrimQuotes(rawValue.Trim());

            if (double.TryParse(value, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double num)
                && !rawValue.Trim().StartsWith("'") && !rawValue.Trim().StartsWith("\""))
            {
                return new AttributeValue { N = num.ToString(System.Globalization.CultureInfo.InvariantCulture) };
            }

            if (value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return new AttributeValue { BOOL = bool.Parse(value) };
            }

            return new AttributeValue { S = value };
        }
    }
}