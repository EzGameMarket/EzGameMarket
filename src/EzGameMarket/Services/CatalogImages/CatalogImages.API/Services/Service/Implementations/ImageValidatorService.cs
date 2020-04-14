using CatalogImages.API.Services.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Service.Implementations
{
    public class ImageValidatorService : IImageValidatorService
    {
        public Task<bool> ValidateDimensions()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateExtensions()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateSize()
        {
            throw new NotImplementedException();
        }
    }
}
