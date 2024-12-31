using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class Relation
    {
        public int CharacterId { get; set; }
        public int Value { get; set; }        
    }
}
