using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class Stat
    {
        public int Value { get; set; }
        public int Adjustment { get; set; }        
    }
}
