using System.Collections.Generic;
using DynamoDBUI.Models;
using DynamoDBUI.Utils;

namespace DynamoDBUI.Services
{
    /// <summary>
    /// Mengelola daftar ConnectionProfile + service DynamoDB aktif untuk tiap koneksi.
    /// </summary>
    public class ConnectionManager
    {
        public List<ConnectionProfile> Profiles { get; private set; }
        private readonly Dictionary<string, DynamoDbService> _services = new Dictionary<string, DynamoDbService>();

        public ConnectionManager()
        {
            Profiles = ConnectionStore.Load();
        }

        public void AddConnection(ConnectionProfile profile)
        {
            Profiles.Add(profile);
            ConnectionStore.Save(Profiles);
        }

        public void RemoveConnection(string name)
        {
            Profiles.RemoveAll(p => p.Name == name);
            _services.Remove(name);
            ConnectionStore.Save(Profiles);
        }

        public DynamoDbService GetService(string connectionName)
        {
            if (_services.ContainsKey(connectionName))
                return _services[connectionName];

            var profile = Profiles.Find(p => p.Name == connectionName);
            if (profile == null) return null;

            var service = new DynamoDbService(profile);
            service.Connect();
            _services[connectionName] = service;
            return service;
        }
    }
}