using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnBecameInvisible() => Destroy(gameObject);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) return;

        if (other.gameObject.TryGetComponent<HealthController>(out var healthController))
        {
            healthController.DealDamage(damage);
        }

        Destroy(gameObject);
    }
}