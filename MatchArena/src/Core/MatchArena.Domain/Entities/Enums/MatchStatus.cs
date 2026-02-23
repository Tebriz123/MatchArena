using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchArena.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MatchStatus
    {
        Pending = 1,
        Accepted = 2,
        Completed = 3,
        Cancelled = 4
    }
}
