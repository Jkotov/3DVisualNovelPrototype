using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Bookmark : MonoBehaviour
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }
    }
}