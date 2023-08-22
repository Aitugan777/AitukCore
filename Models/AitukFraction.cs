using AitukCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Models
{
    internal class AitukFraction : IAitukFraction
    {
        public int Id { get; set; }
        public uint Balance { get; set; }
    }
}
