using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchArena.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PlayerPosition
    {
        GoalKeeper = 1,
        Defender = 2,
        Midfielder = 3,
        Froward = 4

    }
}
