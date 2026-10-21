
using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using EMP_record_management.Models;

namespace EMP_record_management.Storage
{
    internal class csvfile : EmployeeStorageBase
    {
        public override void save(
            List<emp> employees, string filePath)
        {
            ValidatePath(filePath, ".csv", false);

            using StreamWriter writer = new StreamWriter(filePath);

            writer.WriteLine("Name,Age,Salary");

            foreach (emp employee in employees)
            {
                string name = employee.Name.Replace("\"", "\"\"");

                writer.WriteLine(
                    $"\"{name}\",{employee.Age}," +
                    employee.Salary.ToString(
                        CultureInfo.InvariantCulture));
            }
        }

        public override List<emp> restore(string filePath)
        {
            ValidatePath(filePath, ".csv", true);

            string[] lines = File.ReadAllLines(filePath);
            List<emp> employees = new List<emp>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] data = ParseCsvLine(lines[i]);

                if (data.Length != 3 ||
                    !int.TryParse(data[1], out int age) ||
                    !decimal.TryParse(
                        data[2],
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal salary))
                {
                    throw new FormatException(
                        $"Invalid CSV data at line {i + 1}.");
                }

                employees.Add(new emp
                {
                    Name = data[0],
                    Age = age,
                    Salary = salary
                });
            }

            return employees;
        }

        private static string[] ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            System.Text.StringBuilder field =
                new System.Text.StringBuilder();

            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes &&
                        i + 1 < line.Length &&
                        line[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(c);
                }
            }

            if (inQuotes)
                throw new FormatException("Unclosed quote in CSV data.");

            fields.Add(field.ToString());
            return fields.ToArray();
        }
    }
}
