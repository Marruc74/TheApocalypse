using Assets.Scripts.Data.Character;
using Assets.Scripts.Data.Faction;
using Assets.Scripts.Data.Generic;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Enums.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers
{
    public class AssetManager : MonoBehaviour
    {
        public Dictionary<int, string> Regions = new Dictionary<int, string>();
        public Dictionary<int, string> Ethnicities = new Dictionary<int, string>();
        public Dictionary<int, string> Wearables = new Dictionary<int, string>();
        public Dictionary<int, string> Buildings = new Dictionary<int, string>();
        public Dictionary<int, string> SettlementTypes = new Dictionary<int, string>();
        public Dictionary<int, string> WorkClasses = new Dictionary<int, string>();
        public Dictionary<int, string> SkinColors = new Dictionary<int, string>();
        public Dictionary<int, string> HairColors = new Dictionary<int, string>();
        public Dictionary<int, string> WearableColors = new Dictionary<int, string>();
        public Dictionary<int, string> PaintAndTattoosColors = new Dictionary<int, string>();
        public Dictionary<int, string> WearableItems = new Dictionary<int, string>();
        public Dictionary<int, string> FlavorTexts = new Dictionary<int, string>();
        public Dictionary<int, string> Skills = new Dictionary<int, string>();

        public void Awake()
        {
            LoadRegions();
            LoadEthnicities();
            LoadWearables();
            LoadSettlementTypes();
            LoadWorkClasses();
            LoadSkills();
            SkinColors = LoadColors(Constants.SkinColorPath);
            HairColors = LoadColors(Constants.HairColorPath);
            WearableColors = LoadColors(Constants.WearableColorPath);
            PaintAndTattoosColors = LoadColors(Constants.PaintAndTattooColorPath);
        }

        public RegionData GetRegion(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.RegionPath, Regions[id])) as RegionData;
        }

        public EthnicityData GetEthnicity(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.EthnicityPath, Ethnicities[id])) as EthnicityData;
        }

        public WorkClassData GetWorkClass(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.WorkClassPath, WorkClasses[id])) as WorkClassData;
        }

        public WearableData GetWearables(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.WearablePath, Wearables[id])) as WearableData;
        }

        public FlavorTextsData GetFlavorTexts(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.FlavorTextsPath, FlavorTexts[id])) as FlavorTextsData;
        }

        public SkillData GetSkill(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.SkillsPath, Skills[id])) as SkillData;
        }

        public SettlementTypeData GetSettlementTypes(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.SettlementTypesPath, SettlementTypes[id])) as SettlementTypeData;
        }

        public ColorListData GetSkinColors(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.SkinColorPath, SkinColors[id])) as ColorListData;
        }

        public ColorListData GetHairColors(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.HairColorPath, HairColors[id])) as ColorListData;
        }

        public ColorListData GetWearablesColors(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.WearableColorPath, WearableColors[id])) as ColorListData;
        }

        public ColorListData GetPaintAndTattooColors(int id)
        {
            return Resources.Load(string.Format("{0}{1}", Constants.PaintAndTattooColorPath, PaintAndTattoosColors[id])) as ColorListData;
        }

        public IEnumerable<SettlementTypeData> GetAllSettlementTypes()
        {
            return Resources.LoadAll(Constants.SettlementTypesPath, typeof(SettlementTypeData)).Cast<SettlementTypeData>();
        }

        public IEnumerable<SkillData> GetAllSkills()
        {
            return Resources.LoadAll(Constants.SkillsPath, typeof(SkillData)).Cast<SkillData>();
        }

        public IEnumerable<RegionData> GetAllRegions()
        {
            return Resources.LoadAll(Constants.RegionPath, typeof(RegionData)).Cast<RegionData>();
        }

        public FlavorTextsData GetFlavorTextsData(Genders gender, FlavorTextTypes type)
        {
            var list = Resources.LoadAll(Constants.FlavorTextsPath, typeof(FlavorTextsData)).Cast<FlavorTextsData>();

            return list.FirstOrDefault(l => l.gender == gender && l.flavorTextType == type);
        }

        private void LoadRegions()
        {
            var datas = Resources.LoadAll(Constants.RegionPath, typeof(RegionData)).Cast<RegionData>();

            foreach (var data in datas)
            {
                Regions.Add(data.id, data.name);
            }
        }

        private void LoadSettlementTypes()
        {
            var datas = Resources.LoadAll(Constants.SettlementTypesPath, typeof(SettlementTypeData)).Cast<SettlementTypeData>();

            foreach (var data in datas)
            {
                SettlementTypes.Add(data.id, data.name);
            }
        }

        private void LoadEthnicities()
        {
            var datas = Resources.LoadAll(Constants.EthnicityPath, typeof(EthnicityData)).Cast<EthnicityData>();

            foreach (var data in datas)
            {
                Ethnicities.Add(data.id, data.name);
            }
        }

        private void LoadWorkClasses()
        {
            var datas = Resources.LoadAll(Constants.WorkClassPath, typeof(WorkClassData)).Cast<WorkClassData>();

            foreach (var data in datas)
            {
                WorkClasses.Add(data.id, data.name);
            }
        }

        private void LoadWearables()
        {
            var datas = Resources.LoadAll(Constants.WearablePath, typeof(WearableData)).Cast<WearableData>();

            foreach (var data in datas)
            {
                Wearables.Add(data.id, data.name);
            }
        }

        private void LoadFlavorTexts()
        {
            var datas = Resources.LoadAll(Constants.FlavorTextsPath, typeof(FlavorTextsData)).Cast<FlavorTextsData>();

            foreach (var data in datas)
            {
                FlavorTexts.Add(data.id, data.name);
            }
        }

        private void LoadSkills()
        {
            var datas = Resources.LoadAll(Constants.SkillsPath, typeof(SkillData)).Cast<SkillData>();

            foreach (var data in datas)
            {
                Skills.Add(data.id, data.name);
            }
        }

        private Dictionary<int, string> LoadColors(string path)
        {
            var datas = Resources.LoadAll(path, typeof(ColorListData)).Cast<ColorListData>();
            var list = new Dictionary<int, string>();
            foreach (var data in datas)
            {
                list.Add(data.id, data.name);
            }

            return list;
        }
    }
}
