using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using DynamoDBUI.Services;
using DynamoDBUI.Utils;

namespace DynamoDBUI.QueryEngine
{
    public class QueryResult
    {
        public DataTable Data;
        public string Message;
        public long ElapsedMs;
    }

    /// <summary>
    /// Mengeksekusi ParsedQuery ke DynamoDbService dan mengembalikan hasil siap tampil.
    /// </summary>
    public static class QueryExecutor
    {
        public static async Task<QueryResult> ExecuteAsync(ParsedQuery pq, DynamoDbService service)
        {
            var sw = Stopwatch.StartNew();
            var result = new QueryResult();

            switch (pq.Type)
            {
                case QueryType.View:
                    var items = await service.ScanAsync(pq.TableName, pq.FindByColumn, pq.FindByValue);
                    var dt = AttributeValueConverter.BuildDataTable(items);

                    if (!string.IsNullOrEmpty(pq.OrderByColumn) && dt.Columns.Contains(pq.OrderByColumn))
                    {
                        dt.DefaultView.Sort = $"[{pq.OrderByColumn}] {pq.OrderDirection}";
                        dt = dt.DefaultView.ToTable();
                    }

                    result.Data = dt;
                    result.Message = $"{dt.Rows.Count} baris ditemukan dari table '{pq.TableName}'.";
                    break;

                case QueryType.CreateTable:
                    await service.CreateTableAsync(pq.TableName, pq.Columns);
                    result.Message = $"Table '{pq.TableName}' berhasil dibuat.";
                    break;

                case QueryType.Insert:
                    var item = new Dictionary<string, AttributeValue>();
                    for (int i = 0; i < pq.InsertColumns.Count; i++)
                    {
                        item[pq.InsertColumns[i]] = AttributeValueConverter.InferAttributeValue(pq.InsertValues[i]);
                    }
                    await service.PutItemAsync(pq.TableName, item);
                    result.Message = $"1 baris berhasil di-insert ke '{pq.TableName}'.";
                    break;

                case QueryType.DropTable:
                    await service.DropTableAsync(pq.TableName);
                    result.Message = $"Table '{pq.TableName}' berhasil dihapus.";
                    break;
            }

            sw.Stop();
            result.ElapsedMs = sw.ElapsedMilliseconds;
            return result;
        }
    }
}