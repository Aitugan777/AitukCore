using AitukCore.Interfaces;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Models
{
    internal class AitukPlayer : IAitukPlayer
    {
        public AitukPlayer(CSteamID steamID, int fractionId, int fractionLVL, string lang)
        {
            this.CSteamID = steamID;
            this.FractionId = fractionId;
            this.FractionLVL = fractionLVL;
            this.Language = lang;
        }

        public CSteamID CSteamID { get; set; } 

        public int FractionId { get; set; }

        public int FractionLVL { get; set; }

        public string Language { get; set; }
    }
}
