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
        public ProductClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<List<GetProductItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetProductItemVM>>("Products");
        }
        public async Task<GetProductVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetProductVM>($"Products/{id}");
        }

        public async Task<bool> CreateAsync(PostProductVM productVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(productVM.Name), "Name");
            content.Add(new StringContent(productVM.Price.ToString()), "Price");
            content.Add(new StringContent(productVM.Description), "Description");
            content.Add(new StringContent(productVM.CategoryId.ToString()), "CategoryId");

            var primaryStream = productVM.PrimaryPhoto.OpenReadStream();
            var primaryContent = new StreamContent(primaryStream);
            primaryContent.Headers.ContentType = new MediaTypeHeaderValue(productVM.PrimaryPhoto.ContentType);
            content.Add(primaryContent, "PrimaryPhoto", productVM.PrimaryPhoto.FileName);

            foreach (var photo in productVM.AdditionalPhotos)
            {
                var stream = photo.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
                content.Add(fileContent, "AdditionalPhotos", photo.FileName);
            }

            foreach (var sizeId in productVM.SizeIds)
                content.Add(new StringContent(sizeId.ToString()), "SizeIds");

            foreach (var colorId in productVM.ColorIds)
                content.Add(new StringContent(colorId.ToString()), "ColorIds");

            var response = await _httpClient.PostAsync("Products", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutProductVM productVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(productVM.Name), "Name");
            content.Add(new StringContent(productVM.Price.ToString()), "Price");
            content.Add(new StringContent(productVM.Description), "Description");
            content.Add(new StringContent(productVM.CategoryId.ToString()), "CategoryId");

            if (productVM.PrimaryPhoto is not null)
            {
                var primaryStream = productVM.PrimaryPhoto.OpenReadStream();
                var primaryContent = new StreamContent(primaryStream);
                primaryContent.Headers.ContentType = new MediaTypeHeaderValue(productVM.PrimaryPhoto.ContentType);
                content.Add(primaryContent, "PrimaryPhoto", productVM.PrimaryPhoto.FileName);
            }

            if (productVM.AdditionalPhotos is not null)
            {
                foreach (var photo in productVM.AdditionalPhotos)
                {
                    var stream = photo.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
                    content.Add(fileContent, "AdditionalPhotos", photo.FileName);
                }
            }

            foreach (var sizeId in productVM.SizeIds)
                content.Add(new StringContent(sizeId.ToString()), "SizeIds");

            foreach (var colorId in productVM.ColorIds)
                content.Add(new StringContent(colorId.ToString()), "ColorIds");

            var response = await _httpClient.PutAsync($"Products/{id}", content);
            return response.IsSuccessStatusCode;
        }

    }
}
