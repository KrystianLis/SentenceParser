using API.Interfaces;
using API.Models;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<SentenceToWordsDto> GetWordsAsync(int id)
        {
            var sentence = await _repo.GetAsync(id);

            if(sentence is null)
            {
                throw new ArgumentNullException();
            }

            var words = await Task.Run(() => GetWordsFromText(sentence.Value));

            return new SentenceToWordsDto
            {
                Words = words,
            };
        }

        private string[] GetWordsFromText(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException();
            }

            string trimInput = input.Trim();
            string[] words = System.Text.RegularExpressions.Regex.Split(trimInput, @"\s{1,}");
            Array.Sort(words);

            return words;
        }
    }
}
