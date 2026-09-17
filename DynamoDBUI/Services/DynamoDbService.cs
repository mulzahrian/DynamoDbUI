using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using DynamoDBUI.Models;
using DynamoDBUI.QueryEngine;

namespace DynamoDBUI.Services
{
    /// <summary>
    /// Wrapper semua operasi ke DynamoDB untuk satu ConnectionProfile.
    /// </summary>
    public class DynamoDbService
    {
        private readonly ConnectionProfile _profile;
        private AmazonDynamoDBClient _client;

        public DynamoDbService(ConnectionProfile profile)
        {
            _profile = profile;
        }

        public bool IsConnected => _client != null;

        public void Connect()
        {
            var credentials = new BasicAWSCredentials(_profile.AccessKey, _profile.SecretKey);

            var config = new AmazonDynamoDBConfig
            {
                ServiceURL = _profile.ServiceUrl,
                AuthenticationRegion = _profile.Region,
                Timeout = TimeSpan.FromSeconds(6),
                MaxErrorRetry = 1
            };

            _client = new AmazonDynamoDBClient(credentials, config);
        }

        public async Task<List<string>> ListTablesAsync()
        {
            EnsureConnected();
            var response = await _client.ListTablesAsync();
            return response.TableNames;
        }

        public async Task CreateTableAsync(string tableName, List<ColumnDefinition> columns)
        {
            EnsureConnected();

            var pk = columns.FirstOrDefault(c => c.IsPartitionKey);
            if (pk == null)
                throw new InvalidOperationException("Harus ada satu kolom dengan tanda PK (partition key).");

            var sk = columns.FirstOrDefault(c => c.IsSortKey);

            var attributeDefs = new List<AttributeDefinition>
            {
                new AttributeDefinition(pk.Name, ToScalarType(pk.Type))
            };
            var keySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement(pk.Name, KeyType.HASH)
            };

            if (sk != null)
            {
                attributeDefs.Add(new AttributeDefinition(sk.Name, ToScalarType(sk.Type)));
                keySchema.Add(new KeySchemaElement(sk.Name, KeyType.RANGE));
            }

            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = attributeDefs,
                KeySchema = keySchema,
                BillingMode = BillingMode.PAY_PER_REQUEST
            };

            await _client.CreateTableAsync(request);
        }

        public async Task DropTableAsync(string tableName)
        {
            EnsureConnected();
            await _client.DeleteTableAsync(tableName);
        }

        public async Task<List<Dictionary<string, AttributeValue>>> ScanAsync(
            string tableName, string filterColumn = null, string filterValue = null)
        {
            EnsureConnected();

            var request = new ScanRequest { TableName = tableName };

            if (!string.IsNullOrEmpty(filterColumn))
            {
                request.FilterExpression = "#c = :v";
                request.ExpressionAttributeNames = new Dictionary<string, string> { { "#c", filterColumn } };
                request.ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":v", Utils.AttributeValueConverter.InferAttributeValue(filterValue) }
                };
            }

            var response = await _client.ScanAsync(request);
            return response.Items;
        }

        public async Task PutItemAsync(string tableName, Dictionary<string, AttributeValue> item)
        {
            EnsureConnected();
            await _client.PutItemAsync(new PutItemRequest
            {
                TableName = tableName,
                Item = item
            });
        }

        /// <summary>
        /// Ambil daftar nama kolom untuk ditampilkan di sidebar: kolom key schema (PK/SK)
        /// digabung dengan atribut lain yang terdeteksi dari satu contoh item (kalau ada).
        /// DynamoDB schemaless di luar key, jadi ini best-effort saja.
        /// </summary>
        public async Task<List<string>> GetColumnsAsync(string tableName)
        {
            EnsureConnected();

            var columns = new List<string>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var describe = await _client.DescribeTableAsync(new DescribeTableRequest { TableName = tableName });
            foreach (var key in describe.Table.KeySchema)
            {
                string label = key.KeyType == KeyType.HASH ? $"{key.AttributeName} (PK)" : $"{key.AttributeName} (SK)";
                columns.Add(label);
                seen.Add(key.AttributeName);
            }

            try
            {
                var scan = await _client.ScanAsync(new ScanRequest { TableName = tableName, Limit = 1 });
                if (scan.Items.Count > 0)
                {
                    foreach (var attributeName in scan.Items[0].Keys)
                    {
                        if (seen.Add(attributeName))
                            columns.Add(attributeName);
                    }
                }
            }
            catch
            {
                // Kalau scan gagal (misal permission terbatas), cukup tampilkan kolom dari key schema.
            }

            return columns;
        }

        private void EnsureConnected()
        {
            if (_client == null)
                throw new InvalidOperationException("Belum terhubung. Klik connection di sidebar dulu.");
        }

        private static ScalarAttributeType ToScalarType(string type)
        {
            switch (type.ToUpperInvariant())
            {
                case "NUMBER": return ScalarAttributeType.N;
                case "STRING": return ScalarAttributeType.S;
                default: return ScalarAttributeType.S;
            }
        }
    }
}