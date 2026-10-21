
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using EMP_record_management.Models;

namespace EMP_record_management.Storage
{
    internal class jsonfile : EmployeeStorageBase
    {
        public override void save(
            List<emp> employees, string filePath)
        {
            ValidatePath(filePath, ".json", false);

            string json = JsonSerializer.Serialize(
                employees,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);
        }

        public override List<emp> restore(string filePath)
        {
            ValidatePath(filePath, ".json", true);

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<emp>>(json)
                ?? new List<emp>();
        }
    }
}
