using MatchArena.Domain.Entities;
using MatchArena.Domain;

namespace MatchArena.MVC.ViewModels
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
        
}
