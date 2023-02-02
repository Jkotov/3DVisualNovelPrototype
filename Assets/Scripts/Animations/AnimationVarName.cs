using System;

namespace Animations
{
    [Serializable]
    public struct AnimatorVarName
    {
        public string name;
        [NonSerialized] public int cached;
    }
}