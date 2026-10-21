
using EMP_record_management.Models;
using System.Collections.Generic;

namespace EMP_record_management.Interfaces
{
    internal interface Iemp
    {
        void save(List<emp> employees, string filePath);

        List<emp> restore(string filePath);
    }
}
