using System.Text.Json.Serialization;

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
