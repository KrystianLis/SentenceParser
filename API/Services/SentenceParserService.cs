using API.Interfaces;
using API.Models;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Services
{
    public class SentenceParserService : ISentenceParserService
    {
        private readonly ISentenceRepository _repo;
        private readonly IMapper _mapper;

        public SentenceParserService(ISentenceRepository sentenceRepository, IMapper mapper)
        {
            _repo = sentenceRepository;
            _mapper = mapper;
        }

        public async Task<int> AddSentenceAsync(SentenceDto sentenceDto)
        {
            var sentence = _mapper.Map<Sentence>(sentenceDto);
            await _repo.AddAsync(sentence);
            return sentence.Id;
        }

        public async Task<SentenceDto> CreateXmlAsync(int id)
        {
            var sentenceModel = await _repo.GetAsync(id);

            if(sentenceModel is null)
            {
                throw new ArgumentNullException();
            }

            var sentences = await Task.Run(() => GetWordsFromText(sentenceModel.Value));

            var doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"));

            var words = new XElement("text",

                from sentence in sentences
                select new XElement("sentence",
                    from word in sentence.Words
                    select new XElement("word", word)
                ));

            doc.Add(words);
            var wr = new StringWriter();
            doc.Save(wr);

            wr.ToString();

            return new SentenceDto
            {
                Value = wr.ToString()
            };
        }

        public async Task<SentenceDto> CreateCsvAsync(int id)
        {
            var sentenceModel = await _repo.GetAsync(id);

            if (sentenceModel is null)
            {
                throw new ArgumentNullException();
            }

            var sentences = await Task.Run(() => GetWordsFromText(sentenceModel.Value));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Word 1, Word 2, Word 3, Word 4, Word 5, Word 6, Word 7, Word 8");

            int i = 1;
            foreach (var sentence in sentences)
            {
                sb.AppendLine($"Sentence {i++}, " + string.Join(", ", sentence.Words));
            }

            return new SentenceDto
            {
                Value = sb.ToString()
            };
        }

        private IList<SentenceWordsDto> GetWordsFromText(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException();
            }

            var sentences = Regex.Split(input, @"(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?|\!)\s+").Where(x => x != string.Empty);

            string wordPattern = @"(\b[^\s]+\b)";

            var list = new List<SentenceWordsDto>();

            foreach (var sentence in sentences)
            {

                var words = Regex
                  .Matches(sentence, wordPattern)
                  .OfType<Match>()
                  .Select(match => match.Value)
                  .ToArray();
                Array.Sort(words);

                list.Add(new SentenceWordsDto
                {
                    Words = words.ToList()
                });
            }

            return list;
        }
    }
}
