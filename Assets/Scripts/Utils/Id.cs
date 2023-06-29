using UnityEngine;

namespace Utils
{
    public class Id : MonoBehaviour
    {
        public string Guid => guid;
        [HideInInspector] [SerializeField] protected string guid;
    
        public void GenerateGuid()
        {
            guid = System.Guid.NewGuid().ToString();
        }
    }
}
