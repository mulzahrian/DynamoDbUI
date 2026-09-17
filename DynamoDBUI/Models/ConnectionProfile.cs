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

        /// <summary>
        /// Wajib diisi kalau AccessKey/SecretKey berupa temporary credentials
        /// (AWS STS, contoh Access Key ID diawali "ASIA"). Kosongkan untuk
        /// long-term IAM user credentials (Access Key ID diawali "AKIA") atau
        /// DynamoDB Local.
        /// </summary>
        public string SessionToken { get; set; }
        public string Region { get; set; }
        public string ServiceUrl { get; set; }

        public override string ToString() => Name;
    }
}