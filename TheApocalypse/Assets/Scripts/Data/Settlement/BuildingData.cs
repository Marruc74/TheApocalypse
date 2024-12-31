using System;
using UnityEngine;

namespace Assets.Scripts.Data.Settlement
{
    [CreateAssetMenu(fileName = "SettlementData", menuName = "Data/Settlement/Building")]
    [Serializable]
    public class BuildingData : ScriptableObject
    {
        public int id;
        public string buildingName;
        public int sizeX;
        public int sizeZ;
        public GameObject Prefab;
    }
}
