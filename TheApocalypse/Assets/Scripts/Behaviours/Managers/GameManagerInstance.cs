using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers
{
    /// <summary>
    /// Holds an instance of the GameManager.
    /// </summary>
    public class GameManagerInstance : MonoBehaviour
    {
        /// <summary>
        /// An instance of the GameManager.
        /// </summary>
        public static GameManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                var manager = new GameObject { name = "GameManager" };

                Instance = manager.AddComponent<GameManager>();
            }

            Destroy(gameObject);
        }
    }
}
