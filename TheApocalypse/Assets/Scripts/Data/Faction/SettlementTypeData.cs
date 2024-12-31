using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data.Faction
{
    [CreateAssetMenu(fileName = "SettlementTypeData", menuName = "Data/Faction/SettlementType")]
    [Serializable]
    public class SettlementTypeData : ScriptableObject
    {
        public int id;
        public string settlementTypeName;

        public Texture2D image;

        public int xSize;
        public int zSize;

        public GameObject floor;
    }
}
