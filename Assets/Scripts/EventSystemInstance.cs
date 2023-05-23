using UnityEngine;

public class EventSystemInstance : MonoBehaviour
{
    private static EventSystemInstance instance;
        
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform);
        }
    }
}
