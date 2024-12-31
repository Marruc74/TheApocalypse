using Assets.Scripts.Utils.Enums.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Start()
        {
            InitGameProperties();
        }

        public void ResumeCampaign()
        {
            var name = GameManagerInstance.Instance.GameProperties.CurrentCampaign;

            if (!string.IsNullOrEmpty(name))
            {
                var savedGameManager = GetComponent<SavedGamesManager>();
                var savedGame = savedGameManager.Load(name);

                GameManagerInstance.Instance.GameInfo = savedGame.GameInfo;
                SceneManager.LoadScene((int)Scenes.CampaignMain);
            }
        }

        public void NewCampaign()
        {
            var manager = GetComponent<NewCampaignPopup>();

            manager.ShowPopup();
        }

        public void ExitGame()
        {
            var manager = GetComponent<ExitGamePopup>();

            manager.ShowPopup();
        }

        private void InitGameProperties()
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
        }
    }
}
