
using System;
using System.Collections.Generic;
using EMP_record_management.Models;
using EMP_record_management.Interfaces;
using EMP_record_management.Storage;

namespace EMP_record_management.Services
{
    internal class EmployeeService
    {
        // Save employee data in the selected format
        public void SaveEmployees(
            IReadOnlyList<emp> employees,
            string filePath,
            string format)
        {
            Iemp storage;

            if (format.Equals("TXT", StringComparison.OrdinalIgnoreCase))
            {
                storage = new txtfile();
            }
            else if (format.Equals("CSV", StringComparison.OrdinalIgnoreCase))
            {
                storage = new csvfile();
            }
            else if (format.Equals("JSON", StringComparison.OrdinalIgnoreCase))
            {
                storage = new jsonfile();
            }
            else
            {
                throw new ArgumentException("Unsupported file format.");
            }

            storage.save(new List<emp>(employees), filePath);
        }

        // Restore employee data based on file extension
        public List<emp> RestoreEmployees(string filePath)
        {
            Iemp storage;

            if (filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                storage = new txtfile();
            }
            else if (filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                storage = new csvfile();
            }
            else if (filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                storage = new jsonfile();
            }
            else
            {
                throw new ArgumentException("Unsupported file extension.");
            }

            return storage.restore(filePath);
        }
    }
}
