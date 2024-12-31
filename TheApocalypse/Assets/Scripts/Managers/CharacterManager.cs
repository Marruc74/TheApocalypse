using Assets.Scripts.Behaviours.Managers;
using Assets.Scripts.Data.Character;
using Assets.Scripts.Data.Faction;
using Assets.Scripts.Data.Item;
using Assets.Scripts.Models.Character;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Enums.Characters;
using Assets.Scripts.Utils.Enums.Items;
using Assets.Scripts.Utils.Guard;
using Assets.Scripts.Utils.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CharacterManager
    {
        private AssetManager _assetManager;
        private RandomHelper _randomHelper;
        private List<Character> _characters;

        public CharacterManager()
        {
            _randomHelper = new RandomHelper();
        }

        /// <summary>
        /// Create a random character based on a specific region.
        /// </summary>
        /// <param name="region">The region the character is based on.</param>
        /// <param name="assetManager">The region the character is based on.</param>
        /// <param name="includeClothes">If clothes should be included in the creation.</param>
        /// <param name="nextId">The next id for the character.</param>
        /// <returns>A random character.</returns>
        public Character CreateCharacter(RegionData region, AssetManager assetManager, bool includeClothes, int nextId)
        {
            GuardParameter.ArgumentIsNull(region, "region");
            GuardParameter.ArgumentIsNull(assetManager, "assetManager");

            _assetManager = assetManager;

            var ethnicity = _randomHelper.GetRandomDataByChance(region.ethnicities, region.mainEthnicity);
            var workClass = _randomHelper.GetRandomDataByChance(region.workClasses, region.mainWorkClass);
            GuardVariable.ArgumentIsNull(ethnicity, "ethnicity");
            GuardVariable.ArgumentIsNull(workClass, "workClass");

            var character = new Character
            {
                Id = nextId,
                Ethnicity = ethnicity.id,
                Gender = GetGender(),
                WorkClass = workClass.id,               
            };
            
            character.Name = GetName(character);
            character.Strength = GetStat(character, workClass, Stats.Strength);
            character.Coordination = GetStat(character, workClass, Stats.Coordination);
            character.Mind = GetStat(character, workClass, Stats.Mind);
            character.Charisma = GetStat(character, workClass, Stats.Charisma);
            character.Skills = GetSkills(character, workClass);
            character.Portrait = GetPortrait(character, ethnicity, workClass, includeClothes);
            character.FlavorTexts.AddRange(GetFlavorTexts(character));
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(character.Gender, "gender");
            GuardVariable.ArgumentIsNull(character.Name, "name");
            GuardVariable.ArgumentIsNull(character.Name.Nickname, "nickname");

            return character;
        }

        public List<Character> SetupRelations(List<Character> characters)
        {
            _characters = characters;

            foreach (var character in _characters)
            {
                if (Random.Range(1, 101) < 60)
                {
                    AddMutualRelation(character, characters, 8, 11);
                }

                if (Random.Range(1, 101) < 30)
                {
                    AddMutualRelation(character, characters, 0, 2);
                }

                var otherRelations = Random.Range(0, 4);

                for (int i = 0; i < otherRelations; i++)
                {
                    AddRelation(character, characters, 0, 5);
                }

                otherRelations = Random.Range(0, 4);

                for (int i = 0; i < otherRelations; i++)
                {
                    AddRelation(character, characters, 6, 11);
                }
            }

            return _characters;
        }

        private void AddMutualRelation(Character character, List<Character> characters, int minRelation, int maxRelation)
        {
            var listPossibleRelations = characters.Where(c => c.Id != character.Id && !c.Relations.Any(r => r.CharacterId == character.Id) && !character.Relations.Any(r => r.CharacterId == c.Id)).ToList();

            if (listPossibleRelations.Count != 0)
            {
                var otherCharacter = listPossibleRelations.FirstOrDefault(c => c.Id == Random.Range(0, listPossibleRelations.Count));
                if (otherCharacter != null)
                {
                    character.Relations.Add(new Relation { CharacterId = otherCharacter.Id, Value = Random.Range(minRelation, maxRelation) });
                    otherCharacter.Relations.Add(new Relation { CharacterId = character.Id, Value = Random.Range(minRelation, maxRelation) });
                }
            }
        }

        private void AddRelation(Character character, List<Character> characters, int minRelation, int maxRelation)
        {
            var listPossibleRelations = characters.Where(c => c.Id != character.Id && !character.Relations.Any(r => r.CharacterId == c.Id)).ToList();

            if (listPossibleRelations.Count != 0)
            {
                var otherCharacter = listPossibleRelations.FirstOrDefault(c => c.Id == Random.Range(0, listPossibleRelations.Count));
                if (otherCharacter != null)
                {
                    character.Relations.Add(new Relation { CharacterId = otherCharacter.Id, Value = Random.Range(minRelation, maxRelation) });
                }
            }
        }

        private Genders GetGender()
        {
            return Random.Range(1, 101) < 50 ? Genders.Male : Genders.Female;
        }

        private Name GetName(Character character)
        {
            GuardParameter.ArgumentIsNull(character.Gender, "gender");

            var name = new Name();

            var random = Random.Range(1, 101);  
            
            if (!_assetManager.GetWorkClass(character.WorkClass).hasTitle)
            {
                random = Random.Range(11, 101);
            }

            if (random < 5)
            {
                var title = GetNamePart(character.Gender, "Title");
                var givenName = GetNamePart(character.Gender, "Name");

                name.Nickname = string.Format("{0} {1}", title, givenName);
            }
            else if (random < 10)
            {
                var title = GetNamePart(character.Gender, "Title");
                var adjective = GetNamePart(character.Gender, "Adjective");
                var givenName = GetNamePart(character.Gender, "Name");

                name.Nickname = string.Format("{0} {1} {2}", title, adjective, givenName);
            }
            else if (random < 80)
            {
                var adjective = GetNamePart(character.Gender, "Adjective");
                var givenName = GetNamePart(character.Gender, "Name");

                name.Nickname = string.Format("{0} {1}", adjective, givenName);
            }
            else
            {
                var mixName = GetMixName(character.Gender);

                name.Nickname = string.Format("{0}", mixName);
            }

            GuardVariable.ArgumentIsNull(name, "name");
            GuardVariable.TextIsEmpty(name.Nickname, "nickname");

            return name;
        }

        private string GetNamePart(Genders gender, string section)
        {
            GuardParameter.ArgumentIsNull(gender, "gender");

            var fileName = string.Format("{0}/{1}_{2}", Constants.NamePartsPath, section, gender.ToString());
            return GetNamePartFromResource(fileName);
        }

        private string GetMixName(Genders gender)
        {
            GuardParameter.ArgumentIsNull(gender, "gender");

            var pre = string.Format("{0}/Pre_{1}", Constants.NamePartsPath, gender.ToString());
            var post = string.Format("{0}/Post_{1}", Constants.NamePartsPath, gender.ToString());

            GuardVariable.ArgumentIsNull(pre, "pre");
            GuardVariable.ArgumentIsNull(post, "post");

            return string.Format("{0}{1}", GetNamePartFromResource(pre), GetNamePartFromResource(post));
        }

        private string GetNamePartFromResource(string file)
        {
            GuardParameter.ArgumentIsNull(file, "file");

            TextAsset asset = Resources.Load(file) as TextAsset;

            GuardVariable.ArgumentIsNull(asset, "asset");

            var lines = asset.text.Split("\n"[0]);
            var random = Random.Range(0, lines.Length - 1);
            var text = lines[random];

            GuardVariable.ArgumentIsNull(text, "text");

            return text;
        }

        private Stat GetStat(Character character, WorkClassData workClassData, Stats stat)
        {
            var statValue = 0;
           
            if (character.Gender == Genders.Female && stat == Stats.Strength)
            {
                statValue = Random.Range(2, 8);
            }
            else if(character.Gender == Genders.Male && stat == Stats.Coordination)
            {
                statValue = Random.Range(2, 8);
            }
            else
            {
                statValue = Random.Range(2, 9);
            }

            if (stat == workClassData.mainStat)
            {
                statValue = statValue + 2;
            }

            return new Stat { Value = statValue };
        }

        private Portrait GetPortrait(Character character, EthnicityData ethnicityData, WorkClassData workClassData, bool includeClothes)
        {
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(ethnicityData, "ethnicityData");
            GuardVariable.ArgumentIsNull(workClassData, "workClassData");

            var portrait = new Portrait(_assetManager, _assetManager.GetEthnicity(character.Ethnicity))
            {
                Gender = character.Gender
            };

            GetPortraitColors(ethnicityData, portrait);

            GetPortraitBasicFeatures(character, ethnicityData, portrait);

            GetAppearanceDetails(character, portrait, workClassData);

            if (includeClothes)
            {
                GetWearables(character, portrait, workClassData);
            }

            return portrait;
        }

        private void GetAppearanceDetails(Character character, Portrait portrait, WorkClassData workClassData)
        {
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(portrait, "portrait");
            GuardVariable.ArgumentIsNull(workClassData, "workClassData");

            var wearables = _assetManager.GetWearables((int)character.Gender);

            GetAppearanceDetailsFromList(character, portrait, wearables.paintAndTattoos, WearableItemTypes.PaintAndTattoo, workClassData.paintAndTattooChance);

            GetAppearanceDetailsFromList(character, portrait, wearables.wounds, WearableItemTypes.Wounds, workClassData.woundChance);
        }

        private void GetWearables(Character character, Portrait portrait, WorkClassData workClassData)
        {
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(portrait, "portrait");
            GuardVariable.ArgumentIsNull(workClassData, "workClassData");

            var wearables = _assetManager.GetWearables((int)character.Gender);

            GuardVariable.ArgumentIsNull(wearables, "wearables");

            GetWearablesFromList(character, wearables.clothes, WearableItemTypes.Clothes);

            GetWearablesFromList(character, wearables.shoulderPads, WearableItemTypes.ShoulderPads, workClassData.shoulderPadsChance);

            GetWearablesFromList(character, wearables.masks, WearableItemTypes.Mask, workClassData.maskChance);

            GetWearablesFromList(character, wearables.helmets, WearableItemTypes.Helmet, workClassData.helmetChance);

            GetWearablesFromList(character, wearables.armors, WearableItemTypes.Armor, workClassData.armorChance);
        }

        private void GetWearablesFromList(Character character, List<WearableItemData> list, WearableItemTypes wearableItemType, int chance = 100)
        {
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(list, "list");
            GuardVariable.ArgumentIsEmptyList(list, "list");

            if (Random.Range(1, 101) <= chance)
            {
                var wearable = _randomHelper.GetRandomFromList(list);
                if (wearable != null)
                {
                    var item = new WearableItems
                    {
                        ItemId = wearable.id,
                        ItemType = wearableItemType
                    };

                    if (!wearable.alwaysKeepColor)
                    {
                        item.ColorListId = wearable.colorList.id;
                        item.ColorInfoId = _randomHelper.GetRandomFromList(wearable.colorList.Colors).id;
                    }

                    character.WearableItems.Add(item);
                }
            }
        }

        private void GetAppearanceDetailsFromList(Character character, Portrait portrait, List<AppearanceDetailsData> list, WearableItemTypes wearableItemType, int chance = 100, int chanceForSecond = 30)
        {
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(portrait, "portrait");
            GuardVariable.ArgumentIsNull(list, "list");
            GuardVariable.ArgumentIsEmptyList(list, "list");

            if (Random.Range(1, 101) <= chance)
            {
                var detail = AddAppearanceDetails(character, portrait, list, wearableItemType);
                if (detail != null)
                {
                    portrait.Wounds.Add(detail);
                }

                if (Random.Range(1, 101) < chanceForSecond)
                {
                    detail = AddAppearanceDetails(character, portrait, list, wearableItemType);
                    if (detail != null)
                    {
                        portrait.Wounds.Add(detail);
                    }
                }
            }
        }

        private DetailParts AddAppearanceDetails(Character character, Portrait portrait, List<AppearanceDetailsData> list, WearableItemTypes type)
        {
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(portrait, "portrait");
            GuardVariable.ArgumentIsNull(list, "list");
            GuardVariable.ArgumentIsEmptyList(list, "list");

            var detail = _randomHelper.GetRandomFromList(list);
            if (detail != null)
            {
                var newDetail = new DetailParts
                {
                    Name = GetResourceName(detail.texture.ToString()),
                };

                if (!detail.alwaysKeepColor)
                {
                    newDetail.ColorListId = detail.colorList.id;
                    newDetail.ColorInfoId = _randomHelper.GetRandomFromList(detail.colorList.Colors).id;
                }

                if (detail.flavorTexts.Count > 0)
                {
                    character.FlavorTexts.Add(new FlavorText {
                        FlavorTextType = FlavorTextTypes.Wound,
                        Text = _randomHelper.GetRandomFromList(detail.flavorTexts) });
                }

                character.WearableItems.Add(new WearableItems { ItemId = detail.id, ItemType = type });

                return newDetail;
            }

            return null;
        }

       

        private void GetPortraitBasicFeatures(Character character, EthnicityData ethnicityData, Portrait portrait)
        {
            GuardVariable.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(ethnicityData, "ethnicityData");
            GuardVariable.ArgumentIsNull(portrait, "portrait");

            portrait.FaceShape = GetResourceName(_randomHelper.GetRandomFromList(character.Gender == Genders.Female ? ethnicityData.faceShapesFemale : ethnicityData.faceShapesMale).ToString());
            portrait.Nose = GetResourceName(_randomHelper.GetRandomFromList(character.Gender == Genders.Female ? ethnicityData.nosePartsFemale : ethnicityData.nosePartsMale).ToString());
            portrait.Eyes = GetResourceName(_randomHelper.GetRandomFromList(character.Gender == Genders.Female ? ethnicityData.eyesPartsFemale : ethnicityData.eyesPartsMale).ToString());
            portrait.Pupils = GetResourceName(_randomHelper.GetRandomFromList(character.Gender == Genders.Female ? ethnicityData.pupilsPartsFemale : ethnicityData.pupilsPartsMale).ToString());
            portrait.Mouth = GetResourceName(_randomHelper.GetRandomFromList(character.Gender == Genders.Female ? ethnicityData.mouthPartsFemale : ethnicityData.mouthPartsMale).ToString());
            portrait.EyeBrows = GetResourceName(_randomHelper.GetRandomFromList(character.Gender == Genders.Female ? ethnicityData.eyeBrowsFemale : ethnicityData.eyeBrowsMale).ToString());

            var hair = _randomHelper.GetRandomFromList(character.Gender == Genders.Female ? ethnicityData.hairFemale : ethnicityData.hairMale,
                character.Gender == Genders.Female ? ethnicityData.femaleBaldChance : ethnicityData.maleBaldChance);

            if (hair != null)
            {
                portrait.Hair = GetResourceName(hair.ToString());
            }

            if (character.Gender == Genders.Male)
            {
                var faceHair = _randomHelper.GetRandomFromList(ethnicityData.facialHairMale, ethnicityData.facialHairChance);

                if (faceHair != null)
                {
                    portrait.FaceHair = GetResourceName(faceHair.ToString());
                }
            }
        }

        private void GetPortraitColors(EthnicityData ethnicityData, Portrait portrait)
        {
            GuardVariable.ArgumentIsNull(ethnicityData, "ethnicityData");
            GuardVariable.ArgumentIsNull(portrait, "portrait");

            portrait.HairColorInfoId = _randomHelper.GetRandomFromList(ethnicityData.hairColorList.Colors).id;
            portrait.SkinColorInfoId = _randomHelper.GetRandomFromList(ethnicityData.skinColorList.Colors).id;
            portrait.PupilsColorInfoId = _randomHelper.GetRandomFromList(ethnicityData.pupilsColorList.Colors).id;
        }

        private string GetResourceName(string text)
        {
            GuardVariable.ArgumentIsNull(text, "text");

            return text.Substring(0, text.ToString().IndexOf(" "));
        }    
        
        private List<FlavorText> GetFlavorTexts(Character character)
        {
            var randomFlavorTextsNumber = Random.Range(0, 4);
            var list = new List<FlavorText>();

            if (randomFlavorTextsNumber > 0)
            {
                for (int i = 0; i < randomFlavorTextsNumber; i++)
                {
                    var flavorTextType = (FlavorTextTypes)Random.Range(0, System.Enum.GetValues(typeof(FlavorTextTypes)).Length);
                    var flavorTextsData = _assetManager.GetFlavorTextsData(character.Gender, flavorTextType);
                    if (flavorTextsData != null)
                    {
                        var flavorText = _randomHelper.GetRandomFromList(flavorTextsData.flavorTexts);
                        list.Add(new FlavorText { FlavorTextType = flavorTextType, Text = flavorText });
                    }
                }
            }

            return list;
        }

        private List<Skill> GetSkills(Character character, WorkClassData workClassData)
        {
            var list = new List<Skill>();

            var skillsData = _assetManager.GetAllSkills();

            foreach (var skillData in skillsData)
            {
                var skill = new Skill
                {
                    SkillId = skillData.id
                };

                list.Add(skill);
            }

            list = GetSkillValues(character, workClassData, list);

            return list;
        }

        private List<Skill> GetSkillValues(Character character, WorkClassData workClassData, List<Skill> list)
        {
            var skillPoints = 10;

            var mainSkill = list.FirstOrDefault(s => s.SkillId == workClassData.mainSkill.id);

            if (mainSkill != null)
            {
                var random = Random.Range(1, 3);
                skillPoints = skillPoints - random;
                mainSkill.Value = mainSkill.Value + random;
            }

            while (skillPoints > 0)
            {
                var skill = _randomHelper.GetRandomFromList(list);
                skill.Value++;
                skillPoints--;
            }

            return list;
        }
    }
}