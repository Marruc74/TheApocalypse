using Assets.Scripts.Models.Character;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Guard;
using Assets.Scripts.Utils.Helpers;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.FactionCreator.Ui
{
    /// <summary>
    /// Display info about a specific character
    /// </summary>
    [RequireComponent(typeof(AssetManager))]
    public class DisplayCharacter : MonoBehaviour
    {
        public GameObject managers;

        public GameObject title;

        [Header("Stats")]
        public GameObject gender;
        public GameObject ethnicity;
        public GameObject workClass;

        /// <summary>
        /// Updates all the fields with the new character stats
        /// </summary>
        /// <param name="character">The new character</param>
        public void ChangeCharacter(Character character)
        {
            GuardProperties();
            GuardCharacter(character);
            GuardVariable.GameObjectNotHasScript<AssetManager>(managers);

            var assetManager = managers.GetComponent<AssetManager>();
            
            UiHelper.SetText(title, character.Name.Nickname);
            UiHelper.SetText(gender, character.Gender.ToString());
            UiHelper.SetText(ethnicity, assetManager.GetEthnicity(character.Ethnicity).etnicityName);
            UiHelper.SetText(workClass, assetManager.GetWorkClass(character.WorkClass).workClassName);
        }

        /// <summary>
        /// Make sure the character has all the required field needed.
        /// </summary>
        /// <param name="character">The character to check.</param>
        private void GuardCharacter(Character character)
        {
            GuardParameter.ArgumentIsNull(character, "character");
            GuardVariable.ArgumentIsNull(character.Name, "Name");
            GuardVariable.TextIsEmpty(character.Name.Nickname, "Nickname");            
            GuardVariable.IntIsZeroOrLess(character.Ethnicity, "Ethnicity");
            GuardVariable.IntIsZeroOrLess(character.WorkClass, "WorkClass");
        }

        /// <summary>
        /// Check all the required properties of the scripts are set.
        /// </summary>
        private void GuardProperties()
        {
            GuardProperty.ArgumentIsNull(managers, "managers");
            GuardProperty.ArgumentIsNull(title, "title");
            GuardProperty.ArgumentIsNull(gender, "gender");
            GuardProperty.ArgumentIsNull(ethnicity, "ethnicity");
            GuardProperty.ArgumentIsNull(workClass, "workClass");
        }
    }
}
