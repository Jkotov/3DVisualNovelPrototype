using System;
using UnityEngine;

namespace Moveables
{
    [Serializable]
    public struct PositionRotation
    { 
        [SerializeField] public Vector3 pos;
        [SerializeField] public Quaternion rot;

        public PositionRotation(Vector3 pos, Quaternion rot)
        {
            this.pos = pos;
            this.rot = rot;
        }
    }
}
