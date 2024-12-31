using Assets.Scripts.Utils.Enums.Characters;
using UnityEngine;

namespace Assets.Scripts.Data.Character
{
    [CreateAssetMenu(fileName = "WorkClassData", menuName = "Data/Character/WorkClass")]
    public class WorkClassData : ScriptableObject
    {
        public int id;
        public string workClassName;
        public bool hasTitle;

        [Header("Chances")]
        public int armorChance;
        public int shoulderPadsChance;
        public int maskChance;
        public int helmetChance;
        public int paintAndTattooChance;
        public int woundChance;

        [Header("Bonuses")]
        public Stats mainStat;
        public SkillData mainSkill;
    }
}
