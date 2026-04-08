using MatchArena.Domain.Entities;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Products;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class ProductClientService : IProductClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtToken()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<GetProductItemVM>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Products");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<List<GetProductItemVM>>();
        }

        public async Task<GetProductVM?> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"Products/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetProductVM>();
        }

        public async Task<bool> CreateAsync(PostProductVM vm)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(vm.Name), "Name");
            content.Add(new StringContent(vm.Price.ToString()), "Price");
            content.Add(new StringContent(vm.Description), "Description");
            content.Add(new StringContent(vm.CategoryId.ToString()), "CategoryId");

            foreach (var sizeId in vm.SizeIds)
                content.Add(new StringContent(sizeId.ToString()), "SizeIds");

            foreach (var colorId in vm.ColorIds)
                content.Add(new StringContent(colorId.ToString()), "ColorIds");

            if (vm.PrimaryPhoto is not null)
            {
                var stream = vm.PrimaryPhoto.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(vm.PrimaryPhoto.ContentType);
                content.Add(fileContent, "PrimaryPhoto", vm.PrimaryPhoto.FileName);
            }

            if (vm.AdditionalPhotos is not null)
            {
                foreach (var photo in vm.AdditionalPhotos)
                {
                    var stream = photo.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
                    content.Add(fileContent, "AdditionalPhotos", photo.FileName);
                }
            }

            var response = await _httpClient.PostAsync("Products", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutProductVM vm)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(vm.Name), "Name");
            content.Add(new StringContent(vm.Price.ToString()), "Price");
            content.Add(new StringContent(vm.Description), "Description");
            content.Add(new StringContent(vm.CategoryId.ToString()), "CategoryId");

            foreach (var sizeId in vm.SizeIds)
                content.Add(new StringContent(sizeId.ToString()), "SizeIds");

            foreach (var colorId in vm.ColorIds)
                content.Add(new StringContent(colorId.ToString()), "ColorIds");

            if (vm.PrimaryPhoto is not null)
            {
                var stream = vm.PrimaryPhoto.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(vm.PrimaryPhoto.ContentType);
                content.Add(fileContent, "PrimaryPhoto", vm.PrimaryPhoto.FileName);
            }

            if (vm.AdditionalPhotos is not null)
            {
                foreach (var photo in vm.AdditionalPhotos)
                {
                    var stream = photo.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
                    content.Add(fileContent, "AdditionalPhotos", photo.FileName);
                }
            }

            var response = await _httpClient.PutAsync($"Products/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"Products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
