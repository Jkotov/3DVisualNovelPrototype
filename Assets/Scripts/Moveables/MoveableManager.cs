using System.Collections.Generic;
using UnityEngine;

namespace Moveables
{
    public class MoveableManager
    {
        public static MoveableManager Instance { get; } = new MoveableManager();
        public bool canUpdatePosition = true;
        public IReadOnlyDictionary<string, PositionRotation> Positions
        {
            get
            {
                UpdateAllPositions();
                return positions;
            }
        }
        private Dictionary<string, PositionRotation> positions = new Dictionary<string, PositionRotation>();

        public void UpdateAllPositions()
        {
            var moveables = GameObject.FindObjectsOfType<Moveable>();
            foreach (var moveable in moveables)
            {
                UpdatePosition(moveable);
            }
        }
    
        public void UpdatePosition(Moveable moveable)
        {
            if (canUpdatePosition == false)
                return;
            if (positions.ContainsKey(moveable.Guid))
            {
                positions[moveable.Guid] = new PositionRotation(moveable.transform.position, moveable.transform.rotation);
            }
            else
            {
                positions.Add(moveable.Guid, new PositionRotation(moveable.transform.position, moveable.transform.rotation));
            }
        }
    
        public PositionRotation GetPosition(Moveable moveable)
        {
            return positions.ContainsKey(moveable.Guid) ? positions[moveable.Guid] : new PositionRotation(moveable.transform.position, moveable.transform.rotation);
        }

        public void Load(Dictionary<string, PositionRotation> dictionary)
        {
            positions = dictionary;
        }
    
        static MoveableManager()
        {
        }
        private MoveableManager()
        {
        }

    }
}
