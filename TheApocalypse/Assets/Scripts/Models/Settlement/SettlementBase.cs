using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models.Settlement
{
    [Serializable]
    public class SettlementBase
    {
        public int SettlementTypeId { get; set; }
        public Square[,] Grid { get; set; }
        public List<Character.Character> Characters { get; set; }
        public string Name { get; set; }

        public SettlementBase()
        {
            Characters = new List<Character.Character>();
        }
    }
}
