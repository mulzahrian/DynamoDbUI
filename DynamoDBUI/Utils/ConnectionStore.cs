using System;
using System.Collections.Generic;
using System.IO;
using DynamoDBUI.Models;

namespace DynamoDBUI.Utils
{
    /// <summary>
    /// Load/Save daftar ConnectionProfile ke file lokal (plain text, pipe-delimited).
    /// Catatan: ini untuk kebutuhan dev/local (DynamoDB Local). Jangan dipakai
    /// untuk menyimpan credential AWS produksi asli tanpa enkripsi.
    /// </summary>
    public static class ConnectionStore
    {
        private static readonly string FolderPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DynamoDBUI");

        private static readonly string FilePath = Path.Combine(FolderPath, "connections.txt");

        public static List<ConnectionProfile> Load()
        {
            var list = new List<ConnectionProfile>();

            if (!File.Exists(FilePath)) return list;

            foreach (var line in File.ReadAllLines(FilePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (parts.Length != 5) continue;

                list.Add(new ConnectionProfile
                {
                    Name = parts[0],
                    AccessKey = parts[1],
                    SecretKey = parts[2],
                    Region = parts[3],
                    ServiceUrl = parts[4]
                });
            }

            return list;
        }

        public static void Save(List<ConnectionProfile> connections)
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            var lines = new List<string>();
            foreach (var c in connections)
            {
                lines.Add($"{c.Name}|{c.AccessKey}|{c.SecretKey}|{c.Region}|{c.ServiceUrl}");
            }

            File.WriteAllLines(FilePath, lines);
        }
    }
}