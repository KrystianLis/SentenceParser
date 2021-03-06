﻿using AutoMapper;
using Domain.Models;
using static API.Mappings.IMapFrom;

namespace API.Models
{
    public class SentenceDto : IMapFrom<Sentence>
    {
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sentence, SentenceDto>().ReverseMap();
        }
    }
}
