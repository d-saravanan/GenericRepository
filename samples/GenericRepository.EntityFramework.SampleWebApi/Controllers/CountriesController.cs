﻿using AutoMapper;
using GenericRepository.EntityFramework.SampleCore.Entities;
using GenericRepository.EntityFramework.SampleWebApi.Dtos;
using GenericRepository.EntityFramework.SampleWebApi.RequestModels;
using GenericService.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GenericRepository.EntityFramework.SampleWebApi.Controllers
{
    public class CountriesController : BaseApiController
    {
        private readonly GenericService<Country, int> _countryService;
        public CountriesController(GenericService<Country, int> countryService, IMapper mapper) : base(mapper)
        {
            _countryService = countryService;
        }

        // GET api/countries?pageindex=1&pagesize=5
        public async Task<PaginatedDto<CountryDto>> GetCountries(int pageIndex, int pageSize)
        {
            PaginatedList<Country> countries = await _countryService.SearchAsync(new CountrySearchCondition
            {
                PageNo = pageIndex,
                RecordsPerPage = pageSize
            });
            PaginatedDto<CountryDto> countryPaginatedDto = _mapper.Map<PaginatedList<Country>, PaginatedDto<CountryDto>>(countries);
            return countryPaginatedDto;
            //return DtoResult<Country, CountryDto>(countries);
        }

        // GET api/countries/1
        public async Task<CountryDto> GetCountry(int id)
        {
            Country country = await _countryService.GetByIdAsync(id);
            if (country == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return DtoResult<Country, CountryDto>(country);
        }

        // POST api/countries
        public async Task<HttpResponseMessage> PostCountry(CountryRequestModel requestModel)
        {
            Country country = _mapper.Map<CountryRequestModel, Country>(requestModel);
            country.CreatedOn = DateTimeOffset.Now;

            await _countryService.AddAsync(country);

            CountryDto countryDto = _mapper.Map<Country, CountryDto>(country);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, countryDto);
            response.Headers.Location = GetCountryLink(country.Id);

            return response;
        }

        // PUT api/countries/1
        public async Task<CountryDto> PutCountry(int id, CountryRequestModel requestModel)
        {
            Country country = await _countryService.GetByIdAsync(id);
            if (country == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // NOTE: AutoMapper goes bananas over this as 
            // EF creates the poroxy class for Country
            // Country updatedCountry = Mapper.Map<CountryRequestModel, Country>(requestModel, country);

            Country updatedCountry = requestModel.ToCountry(country);
            await _countryService.UpdateAsync(country);

            CountryDto countryDto = _mapper.Map<Country, CountryDto>(country);
            return countryDto;
        }

        // DELETE api/countries/1
        public async Task<HttpResponseMessage> DeleteCountry(int id)
        {
            await _countryService.DeleteAsync(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}