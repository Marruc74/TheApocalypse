using Assets.Scripts.Models.Generic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Generic
{
    [CreateAssetMenu(fileName = "ColorListData", menuName = "Data/Generic/ColorList")]
    [Serializable]
    public class ColorListData : ScriptableObject
    {
        public int id;
        public string colorListName;
        public List<ColorInfo> Colors;
    }
}
