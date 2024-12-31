using Assets.Scripts.Data.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data.Character
{
    [CreateAssetMenu(fileName = "WearableData", menuName = "Data/Character/Wearable")]
    public class WearableData : ScriptableObject
    {
        public int id;        

        [Header("Portrait")]
        public List<WearableItemData> clothes;

        public List<WearableItemData> armors;

        public List<WearableItemData> shoulderPads;

        public List<WearableItemData> masks;

        public List<WearableItemData> helmets;

        public List<AppearanceDetailsData> paintAndTattoos;

        public List<AppearanceDetailsData> wounds;
    }
}
