using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SentenceRepository : ISentenceRepository
    {
        private readonly ParserContext _parserContext;

        public SentenceRepository(ParserContext parserContext)
        {
            _parserContext = parserContext;
        }

        public async Task<Sentence> AddAsync(Sentence sentence)
        {
            await _parserContext.Sentences.AddAsync(sentence);
            await _parserContext.SaveChangesAsync();

            return sentence;
        }

        public async Task<Sentence> GetAsync(int id)
        {
            return await _parserContext.Sentences.FindAsync(id);
        }
    }
}
