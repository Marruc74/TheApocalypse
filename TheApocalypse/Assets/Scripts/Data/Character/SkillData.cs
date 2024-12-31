using Assets.Scripts.Utils.Enums.Characters;
using UnityEngine;

namespace Assets.Scripts.Data.Character
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Data/Character/Skill")]
    public class SkillData : ScriptableObject
    {
        public int id;
        public string skillName;
        public Stats baseStat; 
    }
}
