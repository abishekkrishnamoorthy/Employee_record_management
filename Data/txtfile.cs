
using System;
using System.IO;
using System.Collections.Generic;
using EMP_record_management.Models;

namespace EMP_record_management.Storage
{
    internal class txtfile : EmployeeStorageBase
    {
        public override void save(
            List<emp> employees, string filePath)
        {
            ValidatePath(filePath, ".txt", false);

            using StreamWriter writer = new StreamWriter(filePath);

            foreach (emp employee in employees)
            {
                writer.WriteLine(
                    $"{employee.Name}|{employee.Age}|{employee.Salary}");
            }
        }

        public override List<emp> restore(string filePath)
        {
            ValidatePath(filePath, ".txt", true);

            List<emp> employees = new List<emp>();

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] data = line.Split('|');

                if (data.Length != 3 ||
                    !int.TryParse(data[1], out int age) ||
                    !decimal.TryParse(
                        data[2],
                        System.Globalization.NumberStyles.Number,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal salary))
                {
                    throw new FormatException(
                        "Invalid employee data in TXT file.");
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
    }
}
