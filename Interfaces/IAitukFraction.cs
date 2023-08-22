using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Interfaces
{
    public interface IAitukFraction
    {
        int Id { get; set; }

        uint Balance { get; set; }
    }
}
