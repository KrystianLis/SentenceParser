using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ISentenceParserService
    {
        Task<int> AddSentenceAsync(SentenceDto sentenceDto);
        Task<string> CreateCsvAsync(int id);
        Task<string> CreateXmlAsync(int id);
    }
}
