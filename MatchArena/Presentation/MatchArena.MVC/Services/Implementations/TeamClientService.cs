using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using System.Net.Http.Headers;

public class TeamClientService : ITeamClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TeamClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

    public async Task<List<GetTeamItemVM>?> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GetTeamItemVM>>("Teams");
    }

    public async Task<GetTeamVM?> GetByIdAsync(long id)
    {
        return await _httpClient.GetFromJsonAsync<GetTeamVM>($"Teams/{id}");
    }

    public async Task<bool> CreateAsync(PostTeamVM teamVM)
    {
        AddJwtToken();

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(teamVM.Name ?? ""), "Name");
        content.Add(new StringContent(teamVM.City ?? ""), "City");
        content.Add(new StringContent(teamVM.MaxPlayer.ToString()), "MaxPlayer");
        content.Add(new StringContent(teamVM.Information ?? ""), "Information");

        var stream = teamVM.Photo.OpenReadStream();
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(teamVM.Photo.ContentType);
        content.Add(fileContent, "Photo", teamVM.Photo.FileName);

        var response = await _httpClient.PostAsync("Teams", content);

        var statusCode = response.StatusCode;
        var errorBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"==> Teams Create Status: {statusCode}");
        Console.WriteLine($"==> Teams Create Body: {errorBody}");

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(long id, PutTeamVM teamVM)
    {
        AddJwtToken();

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(teamVM.Name), "Name");
        content.Add(new StringContent(teamVM.CaptainName), "CaptainName");
        content.Add(new StringContent(teamVM.City), "City");
        content.Add(new StringContent(teamVM.Information), "Information");

        if (teamVM.Photo is not null)
        {
            var stream = teamVM.Photo.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(teamVM.Photo.ContentType);
            content.Add(fileContent, "Photo", teamVM.Photo.FileName);
        }

        var response = await _httpClient.PutAsync($"Teams/{id}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        AddJwtToken();
        var response = await _httpClient.DeleteAsync($"Teams?id={id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> JoinTeamAsync(long teamId)
    {
        AddJwtToken();
        var response = await _httpClient.PostAsync($"Teams/{teamId}/join", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemovePlayerAsync(long playerId)
    {
        AddJwtToken();
        var response = await _httpClient.DeleteAsync($"Teams/players/{playerId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendInviteAsync(long teamId, long playerId)
    {
        AddJwtToken();
        var response = await _httpClient.PostAsync($"Teams/{teamId}/invite/{playerId}", null);
        return response.IsSuccessStatusCode;
    }
}