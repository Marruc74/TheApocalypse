using Assets.Scripts.Utils.Enums.Characters;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class Character
    {
        public int Id { get; set; }

        public int Ethnicity { get; set; }

        public Genders Gender { get; set; }

        public Name Name { get; set; }

        public Stat Strength { get; set; }

        public Stat Coordination { get; set; }

        public Stat Mind { get; set; }

        public Stat Charisma { get; set; }

        public Portrait Portrait { get; set; }

        public int WorkClass { get; set; }

        public List<WearableItems> WearableItems { get; set; }

        public List<Skill> Skills { get; set; }

        public List<FlavorText> FlavorTexts { get; set; }

        public List<Relation> Relations { get; set; }

        public Character()
        {
            WearableItems = new List<WearableItems>();
            FlavorTexts = new List<FlavorText>();
            Skills = new List<Skill>();
            Relations = new List<Relation>();
        }
    }
}
