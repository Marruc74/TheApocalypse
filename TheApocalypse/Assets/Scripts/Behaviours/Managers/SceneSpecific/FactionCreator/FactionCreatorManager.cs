using Assets.Scripts.Behaviours.Managers.Activity;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.FactionCreator
{
    /// <summary>
    /// Main handler for the faction creator scene.
    /// </summary>
    [RequireComponent(typeof(InputManager))]
    public class FactionCreatorManager : MonoBehaviour
    {
        public void Start()
        {
            var manager = GetComponent<FactionCreatorPopup>();

            manager.ShowPopup();
        }
    }
}
