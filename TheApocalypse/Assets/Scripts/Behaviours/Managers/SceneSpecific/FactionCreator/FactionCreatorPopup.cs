using Assets.Scripts.Behaviours.Managers.Base;
using Assets.Scripts.Behaviours.Managers.SceneSpecific.FactionCreator.Ui;
using Assets.Scripts.Managers;
using Assets.Scripts.Models.Character;
using Assets.Scripts.Models.Faction;
using Assets.Scripts.Models.Save;
using Assets.Scripts.Models.Settlement;
using Assets.Scripts.Utils.Enums.Faction;
using Assets.Scripts.Utils.Enums.Game;
using Assets.Scripts.Utils.Guard;
using Assets.Scripts.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.FactionCreator
{
    public class FactionCreatorPopup : PopupManager
    {
        [Header("General")]
        public GameObject managers;
        public GameObject title;
        public GameObject continueButton;
        public GameObject imageWithTextPrefab;
        public List<GameObject> allSteps;        

        [Header("Region")]
        public GameObject regionList;

        [Header("Settlement")]
        public GameObject settlementTypesList;

        [Header("Character")]
        public GameObject characterList;        
        public GameObject displayCharacterPanel;

        [Header("Name")]
        public InputField factionName;
        public InputField settlementName;

        private CreateFactionSteps _currentStep;
        private PlayerFaction _playerFaction;

        public void Start()
        {
            HideAllSteps();

            _playerFaction = new PlayerFaction();
            _currentStep = CreateFactionSteps.Region;

            SetupStepRegion();
            SetupNewStep();
        }

        /// <summary>
        /// Move to the next step in faction creation.
        /// </summary>
        public void NextStep()
        {
            if (_currentStep == CreateFactionSteps.Name)
            {
                FinishCreation();
            }
            else
            {
                _currentStep = Enum.GetValues(typeof(CreateFactionSteps)).Cast<CreateFactionSteps>().First(cfs => (int)cfs > (int)_currentStep);

                HideAllSteps();

                SetupNewStep();

                switch (_currentStep)
                {
                    case CreateFactionSteps.SettlementType:
                        SetupStepSettlementTypes();
                        break;
                    case CreateFactionSteps.Characters:
                        SetupStepCharacters();
                        break;
                    case CreateFactionSteps.Name:
                        SetupStepName();
                        break;
                }
            }
        }

        /// <summary>
        /// Handle changes to the settlement name.
        /// </summary>
        public void ChangeSettlementName()
        {
            GuardVariable.ArgumentIsNull(_playerFaction.Settlements, "Settlements");
            GuardVariable.ArgumentIsEmptyList(_playerFaction.Settlements, "Settlements");
            GuardVariable.ArgumentIsNull(_playerFaction.Name, "Faction Name");

            _playerFaction.Settlements[0].Name = settlementName.text;

            if (_playerFaction.Name.Length >= 2 && _playerFaction.Settlements[0].Name.Length >= 2)
            {
                continueButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                continueButton.GetComponent<Button>().interactable = false;
            }
        }

        /// <summary>
        /// Handle changes to the faction name.
        /// </summary>
        public void ChangeFactionName()
        {
            GuardVariable.ArgumentIsNull(_playerFaction.Settlements, "Settlements");
            GuardVariable.ArgumentIsEmptyList(_playerFaction.Settlements, "Settlements");
            GuardVariable.ArgumentIsNull(_playerFaction.Settlements[0].Name, "Settlement Name");

            _playerFaction.Name = factionName.text;

            if (_playerFaction.Name.Length >= 2 && _playerFaction.Settlements[0].Name.Length >= 2)
            {
                continueButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                continueButton.GetComponent<Button>().interactable = false;
            }
        }

        /// <summary>
        /// Things to do on every new step.
        /// </summary>
        private void SetupNewStep()
        {            
            allSteps[(int)_currentStep].SetActive(true);

            continueButton.GetComponent<Button>().interactable = false;
        }

        /// <summary>
        /// Finish the creation, save it and move to the campaign.
        /// </summary>
        private void FinishCreation()
        {
            GuardVariable.ArgumentIsNull(_playerFaction, "_playerFaction");
            GuardVariable.ArgumentIsNull(_playerFaction.Settlements, "Settlements");
            GuardVariable.ArgumentIsEmptyList(_playerFaction.Settlements, "Settlements");
            GuardVariable.GameObjectNotHasScript<SavedGamesManager>(managers);
            
            GameManagerInstance.Instance.GameInfo.PlayerFaction = _playerFaction;

            var savedGameManager = managers.GetComponent<SavedGamesManager>();

            var savedGame = new SavedGame
            {
                GameInfo = GameManagerInstance.Instance.GameInfo
            };

            savedGameManager.Save(savedGame, GameManagerInstance.Instance.GameProperties.CurrentCampaign);

            SceneManager.LoadScene((int)Scenes.CampaignMain);
        }

        /// <summary>
        /// Setup the region step.
        /// </summary>
        private void SetupStepRegion()
        {
            GuardVariable.GameObjectNotHasScript<AssetManager>(gameObject);
            UiHelper.SetText(title, "Region");

            foreach (var region in GetComponent<AssetManager>().GetAllRegions())
            {
                var iconText = Instantiate(imageWithTextPrefab) as GameObject;

                GuardVariable.GameObjectNotHasChild(iconText, "Image");
                GuardVariable.GameObjectNotHasChild(iconText, "Text");

                iconText.transform.SetParent(regionList.transform, false);                
                iconText.transform.Find("Image").GetComponent<Image>().sprite = Sprite.Create(region.image, new Rect(0, 0, region.image.width, region.image.height), new Vector2(0.5f, 0.5f));
                UiHelper.SetText(iconText.transform.Find("Text").gameObject, region.regionName);
                iconText.GetComponent<Button>().onClick.AddListener(() => { SetRegion(region.id); });
            }
        }

        /// <summary>
        /// Setup the settlement type step.
        /// </summary>
        private void SetupStepSettlementTypes()
        {
            GuardVariable.GameObjectNotHasScript<AssetManager>(gameObject);
            UiHelper.SetText(title, "Settlement Type");

            foreach (var settlementTypes in GetComponent<AssetManager>().GetAllSettlementTypes())
            {
                var iconText = Instantiate(imageWithTextPrefab) as GameObject;
                iconText.transform.SetParent(settlementTypesList.transform, false);

                GuardVariable.GameObjectNotHasChild(iconText, "Image");
                GuardVariable.GameObjectNotHasChild(iconText, "Text");

                iconText.transform.Find("Image").GetComponent<Image>().sprite = Sprite.Create(settlementTypes.image, new Rect(0, 0, settlementTypes.image.width, settlementTypes.image.height), new Vector2(0.5f, 0.5f));
                UiHelper.SetText(iconText.transform.Find("Text").gameObject, settlementTypes.settlementTypeName);
                iconText.GetComponent<Button>().onClick.AddListener(() => { SetSettlementType(settlementTypes.id); });
            }
        }

        /// <summary>
        /// Setup the character step
        /// </summary>
        private void SetupStepCharacters()
        {
            GuardVariable.GameObjectNotHasScript<AssetManager>(gameObject);
            GuardVariable.ArgumentIsNull(_playerFaction, "_playerFaction");
            GuardVariable.ArgumentIsNull(_playerFaction.Settlements, "Settlements");
            GuardVariable.ArgumentIsEmptyList(_playerFaction.Settlements, "Settlements");
            GuardVariable.ArgumentIsNull(_playerFaction.Settlements[0].Characters, "Settlements");

            UiHelper.SetText(title, "Characters");

            var characterManager = new CharacterManager();
            var assetManager = GetComponent<AssetManager>();
            var region = assetManager.GetRegion(_playerFaction.RegionId);

            GuardVariable.ArgumentIsNull(region, "region");

            continueButton.GetComponent<Button>().interactable = true;

            var list = new List<Character>();

            for (int loop = 0; loop < 20; loop++)
            {
                var character = characterManager.CreateCharacter(region, assetManager, true, loop + 1);
                list.Add(character);

                var iconText = Instantiate(imageWithTextPrefab) as GameObject;
                iconText.transform.SetParent(characterList.transform, false);

                GuardVariable.GameObjectNotHasChild(iconText, "Image");
                GuardVariable.GameObjectNotHasChild(iconText, "Text");

                iconText.transform.Find("Image").GetComponent<Image>().sprite = character.Portrait.GetFullImage(character.WearableItems);
                UiHelper.SetText(iconText.transform.Find("Text").gameObject, character.Name.Nickname);
                iconText.GetComponent<Button>().onClick.AddListener(() => { DisplayCharacter(character); });
            }

            _playerFaction.Settlements[0].Characters = characterManager.SetupRelations(list);
        }

        /// <summary>
        /// Setup the name step.
        /// </summary>
        private void SetupStepName()
        {
            _playerFaction.Name = string.Empty;
            _playerFaction.Settlements[0].Name = string.Empty;
        }

        /// <summary>
        /// Open the display and show a specific character.
        /// </summary>
        /// <param name="character">The character to display</param>
        private void DisplayCharacter(Character character)
        {
            GuardParameter.ArgumentIsNull(character, "character");

            displayCharacterPanel.SetActive(true);
            displayCharacterPanel.GetComponent<DisplayCharacter>().ChangeCharacter(character);
        }

        /// <summary>
        /// Set the region clicked on.
        /// </summary>
        /// <param name="id">The id of the region</param>
        private void SetRegion(int id)
        {
            GuardParameter.IntIsZeroOrLess(id, "id");

            _playerFaction.RegionId = id;
            continueButton.GetComponent<Button>().interactable = true;
        }

        /// <summary>
        /// Set the settlement type clicked on.
        /// </summary>
        /// <param name="id">The id of the settlement type</param>
        private void SetSettlementType(int id)
        {
            GuardParameter.IntIsZeroOrLess(id, "id");

            _playerFaction.Settlements.Add(new SettlementBase
            {
                SettlementTypeId = id
            });

            continueButton.GetComponent<Button>().interactable = true;
        }

        /// <summary>
        /// Hide all the steps.
        /// </summary>
        private void HideAllSteps()
        {
            foreach (var step in allSteps)
            {
                step.SetActive(false);
            }

            displayCharacterPanel.SetActive(false);
        }

        /// <summary>
        /// Check all the required properties of the scripts are set.
        /// </summary>
        private void GuardProperties()
        {
            GuardProperty.ArgumentIsNull(managers, "managers");
            GuardProperty.ArgumentIsNull(title, "title");
            GuardProperty.ArgumentIsNull(continueButton, "continueButton");
            GuardVariable.GameObjectNotHasScript<Button>(continueButton);
            GuardProperty.ArgumentIsEmptyList(allSteps, "allSteps");
            GuardProperty.ArgumentIsNull(regionList, "regionList");
            GuardProperty.ArgumentIsNull(settlementTypesList, "settlementTypesList");
            GuardProperty.ArgumentIsNull(characterList, "characterList");
            GuardProperty.ArgumentIsNull(imageWithTextPrefab, "imageWithTextPrefab");
            GuardProperty.ArgumentIsNull(displayCharacterPanel, "displayCharacterPanel");
            GuardProperty.ArgumentIsNull(factionName, "factionName");
            GuardProperty.ArgumentIsNull(settlementName, "settlementName");
        }
    }
}
