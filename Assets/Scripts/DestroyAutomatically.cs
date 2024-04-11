using UnityEngine;

public class DestroyAutomatically : MonoBehaviour
{
    [SerializeField] private float destroyAfter;

    private void Start() => Destroy(gameObject, destroyAfter);
}