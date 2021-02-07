using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using NotiCore.API.Helpers;
using NotiCore.API.Infraestructure.RequestValidators;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.Implementation
{
    public class SourceService : ISourceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPredictNewsWebsiteService _predictionService;
        public SourceService(DataContext context, IMapper mapper, IPredictNewsWebsiteService predictionService)
        {
            _context = context;
            _mapper = mapper;
            _predictionService = predictionService;
        }

        public async Task<Source> AddSourceAsync(AddSourceRequest SourceRequest)
        {
            // Model validation
            var requestValidator = new AddSourceRequestValidator();
            requestValidator.ValidateAndThrow(SourceRequest);

            var mappedSource = _mapper.Map<Source>(SourceRequest);

            // Validate url
            if (!UrlCustomHelper.IsValidUrl(mappedSource.Url))
                ErrorsHelper.ThrowValidationError("Invalid URL", "The url provided is not a valid url", "Url");

            mappedSource.Url = UrlCustomHelper.GetAbsoluteUri(mappedSource.Url);

            // Validate if source is not already registered or has invalid foreign key
            AddSourceContextValidation(mappedSource);

            // Check if it is a compatible source
            if (!await IsValidNewsSiteAsync(mappedSource.Url))
                ErrorsHelper.ThrowValidationError("Unable to verify if provided site is a news or blog site", "We could not verify the veracity of the site regarding whether its content is news or blogs", "Url");
             
            mappedSource.IsActive = true;
            var AddedSource = _context.Add(mappedSource);
            _context.SaveChanges();
            
            return _context.Sources.SingleOrDefault(s=> s.SourceId == AddedSource.Entity.SourceId);
        }

        public async Task<bool> IsValidNewsSiteAsync(string url)
        {
            var prediction = await _predictionService.GetPredictionFromUrlAsync(url);
            return prediction.Prediction.Equals("1");
        }

        #region private methods
        private void AddSourceContextValidation(Source source)
        {
            var language = _context.Languages.SingleOrDefault(l => l.LanguageId == source.LanguageId && l.IsActive);
            if(language == null)
                ErrorsHelper.ThrowValidationError("Language not found", "The selected language Id was not found", "LanguageId");
            
            var contextSource = _context.Sources.SingleOrDefault(s=> s.Url.Equals(source.Url));
            if (contextSource != null)
            {
                if (!contextSource.IsActive)
                    ErrorsHelper.ThrowValidationError("Invalid Source", "This source is not supported", "Url");

                ErrorsHelper.ThrowValidationError("Duplicated source", "A source with same url is already registered", "Url");
            }
        }

        #endregion

    }
}
