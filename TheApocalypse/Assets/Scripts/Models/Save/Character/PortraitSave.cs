using Assets.Scripts.Utils.Enums.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Save.Character
{
    [Serializable]
    public class PortraitSave
    {
        public string FaceShape { get; set; }

        public string Nose { get; set; }

        public string Mouth { get; set; }

        public string Eyes { get; set; }

        public string EyeBrows { get; set; }

        public string Clothes { get; set; }

        public string Shoulders { get; set; }

        public string Paint { get; set; }

        public string Mask { get; set; }

        public string Hair { get; set; }

        public string FaceHair { get; set; }

        public Genders Gender { get; set; }
    }
}
