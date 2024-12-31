using Assets.Scripts.Data.Character;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Faction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data.Faction
{
    [CreateAssetMenu(fileName = "RegionData", menuName = "Data/Faction/Region")]
    [Serializable]
    public class RegionData : ScriptableObject
    {
        public int id;
        public string regionName;

        public EthnicityData mainEthnicity;
        public WorkClassData mainWorkClass;

        public List<EthnicityChance> ethnicities;

        public List<WorkClassChance> workClasses;

        public Texture2D image;
    }
}
