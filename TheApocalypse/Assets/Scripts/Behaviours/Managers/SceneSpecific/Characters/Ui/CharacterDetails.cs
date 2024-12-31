using Assets.Scripts.Models.Character;
using Assets.Scripts.Utils.Extensions;
using Assets.Scripts.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.Characters.Ui
{
    public class CharacterDetails : MonoBehaviour
    {
        public GameObject listItemPrefab;

        public GameObject characterName;
        public GameObject portrait;
        public GameObject gender;
        public GameObject ethnicity;
        public GameObject workClass;
        public GameObject strength;
        public GameObject coordination;
        public GameObject mind;
        public GameObject charisma;
        public GameObject skills;
        public GameObject relations;
        public GameObject flavorText;


        public void ChangeCharacter(Character character, AssetManager assetManager)
        {            
            UiHelper.SetText(characterName, character.Name.Nickname);
            portrait.GetComponent<Image>().sprite = character.Portrait.GetFullImage(character.WearableItems);
            UiHelper.SetText(gender, character.Gender.ToString());
            UiHelper.SetText(ethnicity, assetManager.GetEthnicity(character.Ethnicity).etnicityName);
            UiHelper.SetText(workClass, assetManager.GetWorkClass(character.WorkClass).workClassName);
            UiHelper.SetText(strength, character.Strength.Value.ToString());
            UiHelper.SetText(coordination, character.Coordination.Value.ToString());
            UiHelper.SetText(mind, character.Mind.Value.ToString());
            UiHelper.SetText(charisma, character.Charisma.Value.ToString());

            skills.transform.ClearChildren();
            relations.transform.ClearChildren();

            foreach (var skill in character.Skills)
            {
                var listItem = Instantiate(listItemPrefab) as GameObject;
                listItem.transform.SetParent(skills.transform, false);

                UiHelper.SetText(listItem.transform.Find("ListItem").gameObject, assetManager.GetSkill(skill.SkillId).skillName.ToString());
                UiHelper.SetText(listItem.transform.Find("ListItemValue").gameObject, skill.Value.ToString());
            }

            foreach (var relation in character.Relations)
            {
                var listItem = Instantiate(listItemPrefab) as GameObject;
                listItem.transform.SetParent(relations.transform, false);

                UiHelper.SetText(listItem.transform.Find("ListItem").gameObject, GameManagerInstance.Instance.GameInfo.PlayerFaction.Settlements[0].Characters.FirstOrDefault(c => c.Id == relation.CharacterId).Name.Nickname);
                UiHelper.SetText(listItem.transform.Find("ListItemValue").gameObject, relation.Value.ToString());
            }

            var flavorTextsCombined = new StringBuilder();
            foreach (var flavorText in character.FlavorTexts)
            {
                flavorTextsCombined.Append(flavorText.Text);
                flavorTextsCombined.Append(" ");
            }
            UiHelper.SetText(flavorText, flavorTextsCombined.ToString());
        }
    }
}
