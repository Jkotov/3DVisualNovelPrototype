using Dialog;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class TalkAnimationChanger : MonoBehaviour
    {
        
        [SerializeField] private AnimatorVarName isTalking;
        [SerializeField] private Actor actor;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            isTalking.cached = Animator.StringToHash(isTalking.name);
            if (actor != null)
            {
                DialogManager.Instance.activeActorChanged.AddListener(OnActiveActorChange);
            }
        }

        private void OnActiveActorChange(Actor activeActor)
        {
            anim.SetBool(isTalking.cached, actor == activeActor); // slow but easy for typing
        }
    }
}