using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchArena.Domain
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PlayerLevel
    {
        Beginner = 1,
        Amateur = 2,
        SemiPro = 3,
        Professional = 4
    }
}
