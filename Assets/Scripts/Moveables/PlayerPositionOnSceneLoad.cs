using UnityEngine;

namespace Moveables
{
    public class PlayerPositionOnSceneLoad
    {
        public static PlayerPositionOnSceneLoad Instance { get; } = new PlayerPositionOnSceneLoad();
        public Vector3 pos;
        public string objectId;

        static PlayerPositionOnSceneLoad()
        {
        }

        private PlayerPositionOnSceneLoad()
        {
        }
    }
}
