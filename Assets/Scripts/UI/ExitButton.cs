using UnityEngine;

namespace UI
{
    public class ExitButton : MonoBehaviour
    {
        public void Quit()
        {
#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }
}