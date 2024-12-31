using Assets.Scripts.Behaviours.Managers.Base;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.MainMenu
{
    public class ExitGamePopup : PopupManager
    {
        public void Yes()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit ();
            #endif
        }

        public void No()
        {
            HidePopup();
        }
    }
}
