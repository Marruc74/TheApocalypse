using Assets.Scripts.Behaviours.Managers.SceneSpecific.Characters.Ui;
using Assets.Scripts.Models.Character;
using Assets.Scripts.Utils.Enums.Game;
using Assets.Scripts.Utils.Guard;
using Assets.Scripts.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.Characters
{
    public class CharacterManager : MonoBehaviour
    {
        public GameObject listItemPrefab;

        [Header("Character")]
        public GameObject characterList;
        public GameObject displayCharacterPanel;

        void Start()
        {
            var savedGameManager = GetComponent<SavedGamesManager>();

            if (savedGameManager.GamePropertiesExists())
            {
                savedGameManager.LoadGameProperties();
            }
            else
            {
                savedGameManager.SaveGameProperties();
            }

            var name = GameManagerInstance.Instance.GameProperties.CurrentCampaign;

            if (!string.IsNullOrEmpty(name))
            {
                var savedGame = savedGameManager.Load(name);

                GameManagerInstance.Instance.GameInfo = savedGame.GameInfo;
            }

            ListCharacters();
        }

        public void ListCharacters()
        {
            var characters = GameManagerInstance.Instance.GameInfo.PlayerFaction.Settlements[0].Characters;
            var assetManager = GetComponent<AssetManager>();
            foreach (var character in characters)
            {
                var listItem = Instantiate(listItemPrefab) as GameObject;
                listItem.transform.SetParent(characterList.transform, false);

                GuardVariable.GameObjectNotHasChild(listItem, "Image");
                GuardVariable.GameObjectNotHasChild(listItem, "Text");
                character.Portrait.Setup(GetComponent<AssetManager>(), character.Ethnicity);
                listItem.GetComponent<CharacterListItem>().SetupCharacter(character, assetManager);
                listItem.GetComponent<Button>().onClick.AddListener(() => { DisplayCharacter(character); });
            }
        }

        private void DisplayCharacter(Character character)
        {
            GuardParameter.ArgumentIsNull(character, "character");

            displayCharacterPanel.SetActive(true);
            displayCharacterPanel.GetComponent<CharacterDetails>().ChangeCharacter(character, GetComponent<AssetManager>());
        }

        public void CloseDisplayCharacter()
        {
            displayCharacterPanel.SetActive(false);
        }
    }
}
