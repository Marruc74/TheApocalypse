using Assets.Scripts.Models.Faction;
using Assets.Scripts.Models.Settlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// The information about the current game that will be saved.
    /// </summary>
    [Serializable]
    public class GameInfo
    {
        public PlayerFaction PlayerFaction { get; set; }        
        
        public GameInfo()
        {
            PlayerFaction = new PlayerFaction();
        }
    }
}
