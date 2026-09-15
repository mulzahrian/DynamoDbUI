namespace DynamoDBUI.Models
{
    /// <summary>
    /// Representasi satu koneksi DynamoDB (local atau remote).
    /// </summary>
    public class ConnectionProfile
    {
        public string Name { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; }
        public string ServiceUrl { get; set; }

        public override string ToString() => Name;
    }
}