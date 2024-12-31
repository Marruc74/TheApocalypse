using Assets.Scripts.Behaviours.Managers;
using Assets.Scripts.Data.Character;
using Assets.Scripts.Data.Faction;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace Assets.Scripts.Managers
{
    public class FactionManager
    {
        public void CreateFaction(RegionData region, AssetManager assetManager)
        {
            var characterManager = new CharacterManager();

            for (int x = 0; x < 200; x++)
            {
                var charater = characterManager.CreateCharacter(region, assetManager, true, x +1);

                var ethnicityDatas = Resources.LoadAll("DataAssets/Characters/Ethnicities", typeof(EthnicityData));
                var ethnicity = string.Empty;

                foreach (EthnicityData ethnicityData in ethnicityDatas)
                {
                    if (ethnicityData.id == charater.Ethnicity)
                    {
                        ethnicity = ethnicityData.etnicityName;
                    }
                }
                Debug.Log(string.Format("{0} - {1} - {2}", charater.Gender, ethnicity, charater.Name.Nickname));
            }
        }
    }
}
