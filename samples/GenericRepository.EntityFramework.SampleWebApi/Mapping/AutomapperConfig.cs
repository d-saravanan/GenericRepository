using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GenericRepository.EntityFramework.SampleWebApi.RequestModels;
using GenericRepository.EntityFramework.SampleCore.Entities;
using GenericRepository.EntityFramework.SampleWebApi.Dtos;

namespace GenericRepository.EntityFramework.SampleWebApi.Mapping
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration;

        public static void Configure()
        {
            RequestModelToEntityMapping();
        }

        private static void RequestModelToEntityMapping()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CountryRequestModel, Country>();
            });
        }

        public static void InitProfiles<TEntity, TDto>()
            where TDto : IDto
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new PaingatedEntityProfile<TEntity, TDto>()));
        }
    }

    public class PaingatedEntityProfile<TEntity, TDto> : Profile
        where TDto : IDto
    {
        protected override void Configure()
        {
            CreateMap<TEntity, TDto>();
            CreateMap<PaginatedList<TEntity>, PaginatedDto<TDto>>()
                            .ForMember(dest => dest.Items,
                                       opt => opt.MapFrom(
                                           src => src.Select(
                                               entity => Mapper.Map<TEntity, TDto>(entity))));
        }
    }

}