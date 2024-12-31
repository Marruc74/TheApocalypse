using Assets.Scripts.Behaviours.Managers.Base;
using Assets.Scripts.Models.Save;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Enums.Game;
using Assets.Scripts.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.MainMenu
{
    public class NewCampaignPopup : PopupManager
    {
        public InputField campaignName;
        public GameObject error;

        public void Start()
        {
            UiHelper.SetText(error, "");
        }

        public void StartCampaign()
        {
            var name = campaignName.text;
            var savedGameManager = GetComponent<SavedGamesManager>();

            if (string.IsNullOrEmpty(name))
            {
                UiHelper.SetText(error, "Enter a name for the campaign!");
            }
            else if (savedGameManager.FileExists(name))
            {
                UiHelper.SetText(error, "There is already a campaign with that name!");
            }
            else
            {
                var savedGame = new SavedGame
                {
                    GameInfo = GameManagerInstance.Instance.GameInfo
                };
                savedGameManager.Save(savedGame, name);

                GameManagerInstance.Instance.GameProperties.CurrentCampaign = name;
                savedGameManager.SaveGameProperties();

                SceneManager.LoadScene((int)Scenes.FactionCreator);
            }
        }

        public void Cancel()
        {
            UiHelper.SetText(error, "");
            HidePopup();
        }
    }
}
