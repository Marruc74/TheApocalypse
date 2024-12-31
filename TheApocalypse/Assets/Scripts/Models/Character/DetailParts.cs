using System;
using UnityEngine;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class DetailParts
    {
        public string Name { get; set; }

        public int? ColorListId { get; set; }
        public int? ColorInfoId { get; set; }

        [NonSerialized]
        public Texture2D Texture;
    }
}
