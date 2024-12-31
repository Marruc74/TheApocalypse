using Assets.Scripts.Data.Item;
using Assets.Scripts.Models.Generic;
using Assets.Scripts.Utils.Enums.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class WearableItems
    {
        public int ItemId { get; set; }
        public WearableItemTypes ItemType { get; set; }

        public int ColorListId { get; set; }
        public int ColorInfoId { get; set; }

        [NonSerialized]
        public Texture2D Texture;
    }
}
