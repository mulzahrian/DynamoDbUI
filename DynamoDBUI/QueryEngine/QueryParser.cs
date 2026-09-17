using System.Linq;
using System.Text.RegularExpressions;
using DynamoDBUI.Utils;

namespace DynamoDBUI.QueryEngine
{
    /// <summary>
    /// Parser untuk mini query language:
    ///   VIEW table [FINDBY col=val] [ORDER BY col [ASC|DESC]]
    ///   CREATE TABLE table (col TYPE PK, col TYPE SK, col TYPE, ...)
    ///   INSERT INTO table (col1,col2) VALUES (val1,val2)
    ///   DROP TABLE table
    /// </summary>
    public static class QueryParser
    {
        public static ParsedQuery Parse(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                throw new QueryParseException("Query kosong.");

            string query = raw.Trim().TrimEnd(';').Trim();

            if (Regex.IsMatch(query, @"^VIEW\s+", RegexOptions.IgnoreCase))
                return ParseView(query);

            if (Regex.IsMatch(query, @"^CREATE\s+TABLE\s+", RegexOptions.IgnoreCase))
                return ParseCreateTable(query);

            if (Regex.IsMatch(query, @"^INSERT\s+INTO\s+", RegexOptions.IgnoreCase))
                return ParseInsert(query);

            if (Regex.IsMatch(query, @"^DROP\s+TABLE\s+", RegexOptions.IgnoreCase))
                return ParseDropTable(query);

            throw new QueryParseException(
                "Perintah tidak dikenali. Gunakan VIEW, CREATE TABLE, INSERT INTO, atau DROP TABLE.");
        }

        private static ParsedQuery ParseView(string query)
        {
            var pattern = @"^VIEW\s+(?<table>[\w.-]+)" +
                          @"(\s+FINDBY\s+(?<col>\w+)\s*=\s*(?<val>.+?))?" +
                          @"(\s+ORDER\s+BY\s+(?<ordercol>\w+)(\s+(?<dir>ASC|DESC))?)?$";

            var m = Regex.Match(query, pattern, RegexOptions.IgnoreCase);
            if (!m.Success)
                throw new QueryParseException(
                    "Format VIEW salah. Contoh: VIEW Users FINDBY Name='Budi' ORDER BY Age DESC");

            var pq = new ParsedQuery
            {
                Type = QueryType.View,
                TableName = m.Groups["table"].Value
            };

            if (m.Groups["col"].Success)
            {
                pq.FindByColumn = m.Groups["col"].Value;
                pq.FindByValue = CsvLineParser.TrimQuotes(m.Groups["val"].Value.Trim());
            }

            if (m.Groups["ordercol"].Success)
            {
                pq.OrderByColumn = m.Groups["ordercol"].Value;
                pq.OrderDirection = m.Groups["dir"].Success
                    ? m.Groups["dir"].Value.ToUpperInvariant()
                    : "ASC";
            }

            return pq;
        }

        private static ParsedQuery ParseCreateTable(string query)
        {
            var pattern = @"^CREATE\s+TABLE\s+(?<table>[\w.-]+)\s*\((?<cols>.+)\)$";
            var m = Regex.Match(query, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (!m.Success)
                throw new QueryParseException(
                    "Format CREATE TABLE salah. Contoh: CREATE TABLE Users (Id STRING PK, Age NUMBER)");

            var pq = new ParsedQuery
            {
                Type = QueryType.CreateTable,
                TableName = m.Groups["table"].Value
            };

            var colDefs = CsvLineParser.Split(m.Groups["cols"].Value);
            foreach (var def in colDefs)
            {
                var tokens = def.Trim().Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 2)
                    throw new QueryParseException($"Definisi kolom tidak valid: '{def}'. Format: Nama TYPE [PK|SK]");

                pq.Columns.Add(new ColumnDefinition
                {
                    Name = tokens[0],
                    Type = tokens[1].ToUpperInvariant(),
                    IsPartitionKey = tokens.Any(t => t.Equals("PK", System.StringComparison.OrdinalIgnoreCase)),
                    IsSortKey = tokens.Any(t => t.Equals("SK", System.StringComparison.OrdinalIgnoreCase))
                });
            }

            return pq;
        }

        private static ParsedQuery ParseInsert(string query)
        {
            var pattern = @"^INSERT\s+INTO\s+(?<table>[\w.-]+)\s*\((?<cols>.+?)\)\s*VALUES\s*\((?<vals>.+)\)$";
            var m = Regex.Match(query, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (!m.Success)
                throw new QueryParseException(
                    "Format INSERT salah. Contoh: INSERT INTO Users (Id, Name, Age) VALUES ('1','Budi',25)");

            var pq = new ParsedQuery
            {
                Type = QueryType.Insert,
                TableName = m.Groups["table"].Value,
                InsertColumns = CsvLineParser.Split(m.Groups["cols"].Value),
                InsertValues = CsvLineParser.Split(m.Groups["vals"].Value)
            };

            if (pq.InsertColumns.Count != pq.InsertValues.Count)
                throw new QueryParseException("Jumlah kolom dan value tidak sama.");

            return pq;
        }

        private static ParsedQuery ParseDropTable(string query)
        {
            var pattern = @"^DROP\s+TABLE\s+(?<table>[\w.-]+)$";
            var m = Regex.Match(query, pattern, RegexOptions.IgnoreCase);
            if (!m.Success)
                throw new QueryParseException("Format DROP TABLE salah. Contoh: DROP TABLE Users");

            return new ParsedQuery { Type = QueryType.DropTable, TableName = m.Groups["table"].Value };
        }
    }
}