using System.Collections.Generic;

namespace BonusRacing.DataAccess
{
    public class DatabaseCollections
    {
        private static IDictionary<Collections, string> collections = new Dictionary<Collections, string>
        {
            { Collections.Users , "users" },
            { Collections.Rounds, "rounds" },
            { Collections.ArchivalRounds, "archivalRounds" },
            { Collections.EncodedGameData, "gameData" },
            { Collections.LogRequest, "logRequests" },
            { Collections.Settings, "settings" }
        };

        public static string Get(Collections collection)
        {
            return collections[collection];
        }
    }

    public enum Collections
    {
        Users,
        Rounds,
        ArchivalRounds,
        EncodedGameData,
        LogRequest,
        Settings
    }
}
