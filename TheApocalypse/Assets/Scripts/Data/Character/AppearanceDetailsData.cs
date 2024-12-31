using Assets.Scripts.Data.Generic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Character
{
    [CreateAssetMenu(fileName = "AppearanceDetailsData", menuName = "Data/Character/AppearanceDetails")]
    public class AppearanceDetailsData : ScriptableObject
    {
        public int id;
        public string appearanceDetailName;
        public Texture2D texture;
        public bool alwaysKeepColor;
        public ColorListData colorList;

        public List<string> flavorTexts;
    }
}
