using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.GameData
{
    public class GameDataReceivingModel
    {
        public int session_id { get; set; }
        public long card_num { get; set; }
        public int player_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string idencard_number { get; set; }
        public int mach_id { get; set; }
        public string update_date { get; set; }
        public double earned_points { get; set; }
    }
}