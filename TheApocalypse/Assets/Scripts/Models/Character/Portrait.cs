using Assets.Scripts.Behaviours.Managers;
using Assets.Scripts.Data.Character;
using Assets.Scripts.Models.Generic;
using Assets.Scripts.Models.Save.Character;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Enums.Characters;
using Assets.Scripts.Utils.Enums.Items;
using Assets.Scripts.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Models.Character
{
    [Serializable]
    public class Portrait
    {
        public string FaceShape { get; set; }

        public string Nose { get; set; }

        public string Mouth { get; set; }

        public string Eyes { get; set; }

        public string EyeBrows { get; set; }

        public string Pupils { get; set; }

        //   public string Clothes { get; set; }

        //public string Shoulders { get; set; }

        //public string Paint { get; set; }

        //public string Mask { get; set; }

        public string Hair { get; set; }

        public string FaceHair { get; set; }

        public List<DetailParts> PaintAndTattoos { get; set; }

        public List<DetailParts> Wounds { get; set; }

        public Genders Gender { get; set; }

        public int HairColorInfoId { get; set; }

        public int SkinColorInfoId { get; set; }

        public int PupilsColorInfoId { get; set; }

        //   public ColorInfo ClothesColor { get; set; }

        //public ColorInfo MaskColor { get; set; }

        //public ColorInfo ShoulderColor { get; set; }

        //public ColorInfo PaintColor { get; set; }

        [NonSerialized]
        private Texture2D _faceShape;

        [NonSerialized]
        private Texture2D _nose;

        [NonSerialized]
        private Texture2D _mouth;

        [NonSerialized]
        private Texture2D _eyes;

        [NonSerialized]
        private Texture2D _eyeBrows;

        [NonSerialized]
        private Texture2D _pupils;

        [NonSerialized]
        private Texture2D _hair;

        [NonSerialized]
        private Texture2D _faceHair;

        [NonSerialized]
        private AssetManager _assetManager;

        [NonSerialized]
        private EthnicityData _ethnicityData;

        [NonSerialized]
        private Sprite _portrait;

        public Portrait(AssetManager assetManager, EthnicityData ethnicityData)
        {
            _assetManager = assetManager;
            _ethnicityData = ethnicityData;
            PaintAndTattoos = new List<DetailParts>();
            Wounds = new List<DetailParts>();
        }

        public void Setup(AssetManager assetManager, int ethnicity)
        {
            _assetManager = assetManager;
            _ethnicityData = assetManager.GetEthnicity(ethnicity);
        }

        /// <summary>
        /// Mix all the face parts into one image.
        /// </summary>
        /// <returns>The complete portrait.</returns>
        public Sprite GetFullImage(List<WearableItems> wearableItems)
        {
            if (_portrait == null)
            {
                if (_faceShape == null)
                {
                    LoadTextures(wearableItems);
                }

                var skinColor = GetColorInfo(_ethnicityData.skinColorList.Colors, SkinColorInfoId);
                var hairColor = GetColorInfo(_ethnicityData.hairColorList.Colors, HairColorInfoId);

                var tempImage = new Texture2D(128, 128);
                tempImage = ImageHelper.AlphaBlend(tempImage, _faceShape, skinColor);

                foreach (var paintAndTattoo in PaintAndTattoos)
                {
                    var paintAndTattooColor = _assetManager.GetPaintAndTattooColors((int)paintAndTattoo.ColorListId).Colors.FirstOrDefault(cd => cd.id == paintAndTattoo.ColorInfoId);
                    tempImage = ImageHelper.AlphaBlend(tempImage, paintAndTattoo.Texture, paintAndTattooColor ?? null);
                }

                tempImage = ImageHelper.AlphaBlend(tempImage, _nose, new ColorInfo { color = ChangeColorBrightness(skinColor.color, -0.3f) });
                tempImage = ImageHelper.AlphaBlend(tempImage, _mouth);
                tempImage = ImageHelper.AlphaBlend(tempImage, _eyes);

                foreach (var wound in Wounds)
                {
                    tempImage = ImageHelper.AlphaBlend(tempImage, wound.Texture);
                }

                tempImage = ImageHelper.AlphaBlend(tempImage, _pupils, GetColorInfo(_ethnicityData.pupilsColorList.Colors, PupilsColorInfoId));
                tempImage = ImageHelper.AlphaBlend(tempImage, _eyeBrows, hairColor);
                var clothes = wearableItems.FirstOrDefault(w => w.ItemType == WearableItemTypes.Clothes);
                if (clothes != null)
                {
                    var clothesColor = _assetManager.GetWearablesColors(clothes.ColorListId).Colors.FirstOrDefault(cd => cd.id == clothes.ColorInfoId);
                    tempImage = ImageHelper.AlphaBlend(tempImage, clothes.Texture, clothesColor ?? null);
                }

                var armor = wearableItems.FirstOrDefault(w => w.ItemType == WearableItemTypes.Armor);
                if (armor != null)
                {
                    var armorColor = _assetManager.GetWearablesColors(armor.ColorListId).Colors.FirstOrDefault(cd => cd.id == armor.ColorInfoId);
                    tempImage = ImageHelper.AlphaBlend(tempImage, armor.Texture, armorColor ?? null);
                }
                var shoulderPads = wearableItems.FirstOrDefault(w => w.ItemType == WearableItemTypes.ShoulderPads);
                if (shoulderPads != null)
                {
                    var shoulderPadsColor = _assetManager.GetWearablesColors(shoulderPads.ColorListId).Colors.FirstOrDefault(cd => cd.id == shoulderPads.ColorInfoId);
                    tempImage = ImageHelper.AlphaBlend(tempImage, shoulderPads.Texture, shoulderPadsColor ?? null);
                }
                tempImage = ImageHelper.AlphaBlend(tempImage, _faceHair, hairColor);
                var mask = wearableItems.FirstOrDefault(w => w.ItemType == WearableItemTypes.Mask);
                if (mask != null)
                {
                    var maskColor = _assetManager.GetWearablesColors(mask.ColorListId).Colors.FirstOrDefault(cd => cd.id == mask.ColorInfoId);
                    tempImage = ImageHelper.AlphaBlend(tempImage, mask.Texture, maskColor ?? null);
                }

                var helmet = wearableItems.FirstOrDefault(w => w.ItemType == WearableItemTypes.Helmet);
                if (helmet != null)
                {
                    var helmetColor = _assetManager.GetWearablesColors(helmet.ColorListId).Colors.FirstOrDefault(cd => cd.id == helmet.ColorInfoId);
                    tempImage = ImageHelper.AlphaBlend(tempImage, helmet.Texture, helmetColor ?? null);
                }

                if (helmet == null)
                {
                    tempImage = ImageHelper.AlphaBlend(tempImage, _hair, hairColor);
                }
                var rec = new Rect(0, 0, tempImage.width, tempImage.height);
                _portrait = Sprite.Create(tempImage, rec, new Vector2(0.5f, 0.5f), 100);
            }

            return _portrait;
        }

        private ColorInfo GetColorInfo(List<ColorInfo> list, int id)
        {
            return list.FirstOrDefault(l => l.id == id);
        }

        public PortraitSave CreateSaveModel()
        {
            return new PortraitSave
            {
               // Clothes = Clothes,
               // Shoulders = Shoulders,
                EyeBrows = EyeBrows,
                Eyes = Eyes,
                FaceHair = FaceHair,
                FaceShape = FaceShape,
                Gender = Gender,
                Hair = Hair,
                Mouth = Mouth,
                Nose = Nose
            };
        }

        public void CreatePortrait(PortraitSave portraitSave)
        {
           // Clothes = portraitSave.Clothes;
            //Shoulders = portraitSave.Shoulders;
            EyeBrows = portraitSave.EyeBrows;
            Eyes = portraitSave.Eyes;
            FaceHair = portraitSave.FaceHair;
            FaceShape = portraitSave.FaceShape;
            Gender = portraitSave.Gender;
            Hair = portraitSave.Hair;
            Mouth = portraitSave.Mouth;
            Nose = portraitSave.Nose;
        }

        private Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.r;
            float green = (float)color.g;
            float blue = (float)color.b;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return new Color(red, green, blue, color.a);
        }

        private void LoadTextures(List<WearableItems> wearableItems)
        {
            _faceShape = Resources.Load(string.Format("{0}/{1}/Shape/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), FaceShape)) as Texture2D;
            _nose = Resources.Load(string.Format("{0}/{1}/Nose/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), Nose)) as Texture2D;
            _eyes = Resources.Load(string.Format("{0}/{1}/Eyes/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), Eyes)) as Texture2D;
            _pupils = Resources.Load(string.Format("{0}/{1}/Pupils/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), Pupils)) as Texture2D;
            _mouth = Resources.Load(string.Format("{0}/{1}/Mouth/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), Mouth)) as Texture2D;
            _eyeBrows = Resources.Load(string.Format("{0}/{1}/EyeBrows/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), EyeBrows)) as Texture2D;
            if (!string.IsNullOrEmpty(Hair))
            {
                _hair = Resources.Load(string.Format("{0}/{1}/Hair/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), Hair)) as Texture2D;
            }
            if (!string.IsNullOrEmpty(FaceHair))
            {
                _faceHair = Resources.Load(string.Format("{0}/{1}/FaceHair/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), FaceHair)) as Texture2D;
            }
         
            foreach (var paintAndTattoo in PaintAndTattoos)
            {
                paintAndTattoo.Texture = Resources.Load(string.Format("{0}/{1}/Paint/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), paintAndTattoo.Name)) as Texture2D;
            }

            foreach (var wound in Wounds)
            {
                wound.Texture = Resources.Load(string.Format("{0}/{1}/Wounds/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), wound.Name)) as Texture2D;
            }

            foreach (var wearable in wearableItems)
            {
                if (wearable.ItemType == WearableItemTypes.Clothes)
                {
                    wearable.Texture = Resources.Load(string.Format("{0}/{1}/Clothes/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), wearable.ItemId)) as Texture2D;
                }

                if (wearable.ItemType == WearableItemTypes.ShoulderPads)
                {
                    wearable.Texture = Resources.Load(string.Format("{0}/{1}/ShoulderPads/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), wearable.ItemId)) as Texture2D;
                }

                if (wearable.ItemType == WearableItemTypes.Mask)
                {
                    wearable.Texture = Resources.Load(string.Format("{0}/{1}/Mask/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), wearable.ItemId)) as Texture2D;
                }

                if (wearable.ItemType == WearableItemTypes.Armor)
                {
                    wearable.Texture = Resources.Load(string.Format("{0}/{1}/Armor/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), wearable.ItemId)) as Texture2D;
                }

                if (wearable.ItemType == WearableItemTypes.Helmet)
                {
                    wearable.Texture = Resources.Load(string.Format("{0}/{1}/Helmets/{2}", Constants.ResourceImagesPortraitPartsPath, Enum.GetName(typeof(Genders), Gender), wearable.ItemId)) as Texture2D;
                }
            }
        }
    }
}
