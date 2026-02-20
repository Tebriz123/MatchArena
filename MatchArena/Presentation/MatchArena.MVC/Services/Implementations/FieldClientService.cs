using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Category;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class FieldClientService:IFieldClientService
    {
        private readonly HttpClient _httpClient;
        public FieldClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
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
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(fieldVM.Name), "Name");
            content.Add(new StringContent(fieldVM.City), "City");
            content.Add(new StringContent(fieldVM.Address), "Address");
            content.Add(new StringContent(fieldVM.PricePerHour.ToString()), "PricePerHour");
            content.Add(new StringContent(fieldVM.StartTime.ToString()), "StartTime");
            content.Add(new StringContent(fieldVM.EndTime.ToString()), "EndTime");
            content.Add(new StringContent(fieldVM.FieldInformation), "FieldInformation");

            var primaryStream = fieldVM.PrimaryPhoto.OpenReadStream();
            var primaryContent = new StreamContent(primaryStream);
            primaryContent.Headers.ContentType = new MediaTypeHeaderValue(fieldVM.PrimaryPhoto.ContentType);
            content.Add(primaryContent, "PrimaryPhoto", fieldVM.PrimaryPhoto.FileName);

            foreach (var photo in fieldVM.AdditionalPhotos)
            {
                var stream = photo.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
                content.Add(fileContent, "AdditionalPhotos", photo.FileName);
            }

            var response = await _httpClient.PostAsync("Fields", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutFieldVM fieldVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(fieldVM.Name), "Name");
            content.Add(new StringContent(fieldVM.City), "City");
            content.Add(new StringContent(fieldVM.Address), "Address");
            content.Add(new StringContent(fieldVM.PricePerHour.ToString()), "PricePerHour");
            content.Add(new StringContent(fieldVM.StartTime.ToString()), "StartTime");
            content.Add(new StringContent(fieldVM.EndTime.ToString()), "EndTime");
            content.Add(new StringContent(fieldVM.FieldInformation), "FieldInformation");

            if (fieldVM.PrimaryPhoto is not null)
            {
                var primaryStream = fieldVM.PrimaryPhoto.OpenReadStream();
                var primaryContent = new StreamContent(primaryStream);
                primaryContent.Headers.ContentType = new MediaTypeHeaderValue(fieldVM.PrimaryPhoto.ContentType);
                content.Add(primaryContent, "PrimaryPhoto", fieldVM.PrimaryPhoto.FileName);
            }

            if (fieldVM.AdditionalPhotos is not null)
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
    }
}
