using Assets.Scripts.Models.Character;
using Assets.Scripts.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.Characters.Ui
{
    public class CharacterListItem : MonoBehaviour
    {
        public Image portrait;

        public GameObject characterName;
        public GameObject gender;
        public GameObject ethnicity;
        public GameObject workClass;
        public GameObject strength;
        public GameObject coordination;
        public GameObject mind;
        public GameObject charisma;

        private Character _character;

        public void SetupCharacter(Character character, AssetManager assetManager)
        {
            _character = character;            

            portrait.GetComponent<Image>().sprite = _character.Portrait.GetFullImage(_character.WearableItems);
            UiHelper.SetText(characterName, _character.Name.Nickname);
            UiHelper.SetText(gender, _character.Gender.ToString());
            UiHelper.SetText(ethnicity, assetManager.GetEthnicity(_character.Ethnicity).etnicityName);
            UiHelper.SetText(workClass, assetManager.GetWorkClass(_character.WorkClass).workClassName);
            UiHelper.SetText(strength, _character.Strength.Value.ToString());
            UiHelper.SetText(coordination, _character.Coordination.Value.ToString());
            UiHelper.SetText(mind, _character.Mind.Value.ToString());
            UiHelper.SetText(charisma, _character.Charisma.Value.ToString());
        }
    }
}
