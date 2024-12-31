using Assets.Scripts.Models;
using Assets.Scripts.Models.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers
{
    /// <summary>
    /// The main information about the current game. It is stored between the scenes and always available.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The information about the current game that will be saved.
        /// </summary>
        public GameInfo GameInfo { get; set; }

        public GameProperties GameProperties { get; set; }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            GameInfo = new GameInfo();
            GameProperties = new GameProperties();
        }

        /// <summary>
        /// Pauses the game.        
        /// </summary>
        public void PauseGame()
        {
            //TODO: Add a way to allow cameramovement.
            Time.timeScale = 0;
        }

        /// <summary>
        /// Unpauses the game.
        /// </summary>
        public void UnpauseGame()
        {
            Time.timeScale = 1;
        }
    }
}
