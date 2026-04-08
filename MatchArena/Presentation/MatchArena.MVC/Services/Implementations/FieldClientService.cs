using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class FieldClientService : IFieldClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FieldClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtToken()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<GetFieldItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetFieldItemVM>>("Fields");
        }

        public async Task<GetFieldVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetFieldVM>($"Fields/{id}");
        }

        public async Task<bool> CreateAsync(PostFieldVM fieldVM)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(fieldVM.Name ?? ""), "Name");
            content.Add(new StringContent(fieldVM.City ?? ""), "City");
            content.Add(new StringContent(fieldVM.Address ?? ""), "Address");
            content.Add(new StringContent(fieldVM.PricePerHour.ToString()), "PricePerHour");
            content.Add(new StringContent(fieldVM.StartTime.ToString()), "StartTime");
            content.Add(new StringContent(fieldVM.EndTime.ToString()), "EndTime");
            content.Add(new StringContent(fieldVM.FieldInformation ?? ""), "FieldInformation");

            if (fieldVM.PrimaryPhoto is not null)
            {
                var primaryStream = fieldVM.PrimaryPhoto.OpenReadStream();
                var primaryContent = new StreamContent(primaryStream);
                primaryContent.Headers.ContentType = new MediaTypeHeaderValue(fieldVM.PrimaryPhoto.ContentType);
                content.Add(primaryContent, "PrimaryPhoto", fieldVM.PrimaryPhoto.FileName);
            }

            if (fieldVM.AdditionalPhotos is not null && fieldVM.AdditionalPhotos.Any())
            {
                foreach (var photo in fieldVM.AdditionalPhotos)
                {
                    var stream = photo.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
                    content.Add(fileContent, "AdditionalPhotos", photo.FileName);
                }
            }

            var response = await _httpClient.PostAsync("Fields", content);

            var statusCode = response.StatusCode;
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"==> Fields Create Status: {statusCode}");
            Console.WriteLine($"==> Fields Create Body: {errorBody}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutFieldVM fieldVM)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(fieldVM.Name ?? ""), "Name");
            content.Add(new StringContent(fieldVM.City ?? ""), "City");
            content.Add(new StringContent(fieldVM.Address ?? ""), "Address");
            content.Add(new StringContent(fieldVM.PricePerHour.ToString()), "PricePerHour");
            content.Add(new StringContent(fieldVM.StartTime.ToString()), "StartTime");
            content.Add(new StringContent(fieldVM.EndTime.ToString()), "EndTime");
            content.Add(new StringContent(fieldVM.FieldInformation ?? ""), "FieldInformation");

            if (fieldVM.PrimaryPhoto is not null)
            {
                var primaryStream = fieldVM.PrimaryPhoto.OpenReadStream();
                var primaryContent = new StreamContent(primaryStream);
                primaryContent.Headers.ContentType = new MediaTypeHeaderValue(fieldVM.PrimaryPhoto.ContentType);
                content.Add(primaryContent, "PrimaryPhoto", fieldVM.PrimaryPhoto.FileName);
            }

            if (fieldVM.AdditionalPhotos is not null && fieldVM.AdditionalPhotos.Any())
            {
                foreach (var photo in fieldVM.AdditionalPhotos)
                {
                    var stream = photo.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
                    content.Add(fileContent, "AdditionalPhotos", photo.FileName);
                }
            }

            var response = await _httpClient.PutAsync($"Fields/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"Fields?id={id}");
            return response.IsSuccessStatusCode;
        }
    }
}