
using System;
using System.IO;
using System.Collections.Generic;
using EMP_record_management.Models;
using EMP_record_management.Interfaces;

namespace EMP_record_management.Storage
{
    internal abstract class EmployeeStorageBase : Iemp
    {
        protected void ValidatePath(
            string filePath,
            string expectedExtension,
            bool requireExistingFile)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty.");

            if (!string.Equals(
                Path.GetExtension(filePath),
                expectedExtension,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    $"Expected a {expectedExtension} file.");
            }

            if (requireExistingFile && !File.Exists(filePath))
                throw new FileNotFoundException("File not found.", filePath);

            if (!requireExistingFile)
            {
                string? directory = Path.GetDirectoryName(
                    Path.GetFullPath(filePath));

                if (directory == null || !Directory.Exists(directory))
                    throw new DirectoryNotFoundException(
                        "The destination directory does not exist.");
            }
        }

        public abstract void save(
            List<emp> employees, string filePath);

        public abstract List<emp> restore(string filePath);
    }
}
