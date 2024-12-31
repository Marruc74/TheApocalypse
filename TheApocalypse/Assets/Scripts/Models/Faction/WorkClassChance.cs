using Assets.Scripts.Data.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models.Faction
{
    [Serializable]
    public class WorkClassChance
    {
        public WorkClassData data;
        public int chance;
    }
}
