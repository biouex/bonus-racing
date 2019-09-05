using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Helpers
{
    public class DefaultSerializerSettings
    {
        public static JsonSerializerSettings Instance
        {
            get
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });

                return settings;
            }
        }
    }
}