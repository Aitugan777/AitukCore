using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Interfaces
{
    public interface IAitukPlayer
    {
        CSteamID CSteamID { get; set; }

        int FractionId { get; set; }

        int FractionLVL { get; set; }

        string Language { get; set; }

    }
}
