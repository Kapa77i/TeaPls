using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeaPls.Models;

namespace TeaPls.Services
{
    public interface ITeaService<T>
    {
        Task AddTea(string text, string description);
        Task<IEnumerable<Tea>> GetTeas();
        Task<Tea> GetTea(int id);
        Task RemoveTea(int id);
    }
}
