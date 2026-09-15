using System;

namespace DynamoDBUI.QueryEngine
{
    public class QueryParseException : Exception
    {
        public QueryParseException(string message) : base(message) { }
    }
}