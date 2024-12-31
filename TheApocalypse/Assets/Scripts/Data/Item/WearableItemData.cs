using Assets.Scripts.Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Scripts.Data.Item
{
    [CreateAssetMenu(fileName = "WearableItemData", menuName = "Data/Item/WearableItem")]
    public class WearableItemData : ScriptableObject
    {
        public int id;
        public string itemName;

        public Texture2D texture;

        public bool isMetal;

        public bool alwaysKeepColor;

        public bool hideFace;

        public bool hideHair;

        public ColorListData colorList;
    }
}
