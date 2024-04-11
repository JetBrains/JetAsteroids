using UnityEngine;

public class KeepAliveBetweenScenes : MonoBehaviour
{
    private static KeepAliveBetweenScenes _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}