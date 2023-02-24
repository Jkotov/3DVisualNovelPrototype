using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class ActorWindow : MonoBehaviour
    {
        public Actor Actor
        {
            set
            {
                actorName.text = value.actorName;
                image.sprite = value.sprite;
            }
        }

        [SerializeField] private TextMeshProUGUI actorName;
        [SerializeField] private Image image;
    }
}