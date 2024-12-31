using Assets.Scripts.Utils.Enums.Characters;
using System;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class FlavorText
    {
        public FlavorTextTypes FlavorTextType { get; set; }
        public string Text { get; set; }
    }
}
