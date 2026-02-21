using MatchArena.Domain;

namespace MatchArena.MVC.ViewModels
{
    public class PostPlayerVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public string? Image { get; set; }
        public IFormFile Photo { get; set; }
        public string Information { get; set; }
        public string City { get; set; }
        public PlayerPosition Position { get; set; }
        public PlayerLevel Level { get; set; }

    }
}
