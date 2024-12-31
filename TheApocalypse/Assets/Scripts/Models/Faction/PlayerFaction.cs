using Assets.Scripts.Models.Settlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Faction
{
    [Serializable]
    public class PlayerFaction
    {
        public int RegionId { get; set; }

        public string Name { get; set; }

        public List<SettlementBase> Settlements { get; set; }

        public PlayerFaction()
        {
            Settlements = new List<SettlementBase>();
        }
    }
}
