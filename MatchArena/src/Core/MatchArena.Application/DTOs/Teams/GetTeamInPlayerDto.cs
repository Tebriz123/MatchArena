using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Teams
{
    public class GetTeamInPlayerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string City { get; set; }
        public double Rating { get; set; }
    }
}
