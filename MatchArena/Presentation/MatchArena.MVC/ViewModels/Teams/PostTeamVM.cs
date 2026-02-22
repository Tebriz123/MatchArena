namespace MatchArena.MVC.ViewModels
{
    public class PostTeamVM
    {
        public string Name { get; set; }
        public IFormFile? Photo { get; set; }
        public string City { get; set; }
        public int MaxPlayer { get; set; }
        public string Information { get; set; }

    }

}
