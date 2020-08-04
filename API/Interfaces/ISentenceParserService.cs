using System.Collections.Generic;

namespace API.Interfaces
{
    public interface ISentenceParserService
    {
        IEnumerable<string> GetWordsFromText(string input);
    }
}
