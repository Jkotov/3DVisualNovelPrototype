using System;
using UnityEngine;

namespace Moveables
{
    public class Moveable : Id
    {
        private void Awake()
        {
            if (TryGetComponent(out CharacterController controller))
                controller.enabled = false;
            
            var posRot =  MoveableManager.Instance.GetPosition(this);
            transform.position = posRot.pos;
            transform.rotation = posRot.rot;
            
            if (controller != null)
                controller.enabled = true;
        }

        private void OnDestroy()
        {
            MoveableManager.Instance.UpdatePosition(this);
        }
    }
}
