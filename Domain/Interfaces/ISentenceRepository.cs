using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISentenceRepository
    {
        Task<Sentence> GetAsync(int id);
        Task<Sentence> AddAsync(Sentence sentence);
    }
}
