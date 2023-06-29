using System.Linq;
using Utils;

namespace Destroyables
{
    public class Destroyable : Id
    {
        private void Awake()
        {
            if (DestroyableObjectsManager.Instance.DestroyedObjects.Contains(Guid))
                Destroy(gameObject);
        }
    }
}