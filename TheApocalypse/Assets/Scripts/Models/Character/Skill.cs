using System;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class Skill
    {
        public int SkillId { get; set; }
        public int Value { get; set; }
        public int Adjustment { get; set; }
        public int Experience { get; set; }
    }
}
