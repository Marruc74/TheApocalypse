using Assets.Scripts.Utils.Guard;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers.Base
{
    public class PopupManager : MonoBehaviour
    {
        /// <summary>
        /// The popup UI game object.
        /// </summary>
        public GameObject popup;

        void Awake()
        {
            GuardProperties();

            popup.SetActive(false);
        }

        /// <summary>
        /// Hide the popup and return to game.
        /// </summary>
        public void HidePopup()
        {
            GuardProperties();

            GameManagerInstance.Instance.UnpauseGame();
            popup.SetActive(false);
        }

        /// <summary>
        /// Show the popup.
        /// </summary>
        public void ShowPopup()
        {
            GuardProperties();

            GameManagerInstance.Instance.PauseGame();

            var popups = GameObject.FindGameObjectsWithTag("Popup");

            foreach (var popup in popups.Where(p => p.activeInHierarchy))
            {
                popup.SetActive(false);
            }

            popup.SetActive(true);
        }

        /// <summary>
        /// Check all the required properties of the scripts are set.
        /// </summary>
        private void GuardProperties()
        {
            GuardProperty.ArgumentIsNull(popup, "popup");
        }
    }
}
