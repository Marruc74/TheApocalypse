using Assets.Scripts.Utils.Enums.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Character
{
    [CreateAssetMenu(fileName = "FlavorTextsData", menuName = "Data/Character/FlavorTexts")]
    public class FlavorTextsData : ScriptableObject
    {
        public int id;
        public string flavorTextsName;
        public Genders gender;
        public FlavorTextTypes flavorTextType;
        public List<string> flavorTexts;
    }
}
