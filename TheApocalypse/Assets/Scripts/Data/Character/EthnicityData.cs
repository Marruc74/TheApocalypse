using Assets.Scripts.Data.Generic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Character
{
    [CreateAssetMenu(fileName = "EthnicityData", menuName = "Data/Character/Ethnicity")]
    [Serializable]
    public class EthnicityData : ScriptableObject
    {
        public int id;
        public string etnicityName;

        [Header("Portrait")]

        public ColorListData skinColorList;

        public ColorListData hairColorList;

        public ColorListData pupilsColorList;

        public List<Texture2D> faceShapesMale;
        public List<Texture2D> faceShapesFemale;

        public List<Texture2D> nosePartsMale;
        public List<Texture2D> nosePartsFemale;

        public List<Texture2D> mouthPartsMale;
        public List<Texture2D> mouthPartsFemale;

        public List<Texture2D> eyesPartsMale;
        public List<Texture2D> eyesPartsFemale;

        public List<Texture2D> pupilsPartsMale;
        public List<Texture2D> pupilsPartsFemale;

        public List<Texture2D> eyeBrowsMale;
        public List<Texture2D> eyeBrowsFemale;

        public List<Texture2D> hairMale;
        public List<Texture2D> hairFemale;

        public List<Texture2D> facialHairMale;

        public int maleBaldChance;
        public int femaleBaldChance;
        public int facialHairChance;

    }
}
