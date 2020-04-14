using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Service.Abstractions
{
    public interface IImageValidatorService
    {
        //meg kell oldani, hogy a feltölött fájl méretét lehessen itt megvizsgálni
        Task<bool> ValidateSize();

        //meg kell oldani, hogy a feltölött fájl dimenzióit
        Task<bool> ValidateDimensions();
        Task<bool> ValidateExtensions();
    }
}
