using UnityEngine;

public class DestroyAutomatically : MonoBehaviour
{
    public float destroyAfter;

    private void Start() => Destroy(gameObject, destroyAfter);
}