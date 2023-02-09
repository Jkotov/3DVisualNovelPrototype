using System;
using UnityEngine;

namespace Dialog
{
    [Serializable]
    public abstract class DialogBlockCondition
    {
        public abstract bool Completed { get; }
    }
}