using System.Collections.Generic;

namespace DynamoDBUI.QueryEngine
{
    public enum QueryType
    {
        View,
        CreateTable,
        Insert,
        DropTable
    }

    public class ColumnDefinition
    {
        public string Name;
        public string Type; // STRING, NUMBER, BOOL
        public bool IsPartitionKey;
        public bool IsSortKey;
    }

    public class ParsedQuery
    {
        public QueryType Type;
        public string TableName;

        // VIEW ... FINDBY
        public string FindByColumn;
        public string FindByValue;

        // ORDER BY
        public string OrderByColumn;
        public string OrderDirection = "ASC";

        // CREATE TABLE
        public List<ColumnDefinition> Columns = new List<ColumnDefinition>();

        // INSERT INTO
        public List<string> InsertColumns = new List<string>();
        public List<string> InsertValues = new List<string>();
    }
}