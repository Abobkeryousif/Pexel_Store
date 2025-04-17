using Microsoft.AspNetCore.Http;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Entities;


namespace Pexel.Application.Contracts.Services
{
    public interface IImageService 
    {
        Task<List<string>> AddImageAsync(IFormFileCollection file , string src);
        void DeleteImageAsync(string src);
    }
}
