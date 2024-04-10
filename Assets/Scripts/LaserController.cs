using UnityEngine;

public class LaserController : MonoBehaviour
{
    public int damage = 25;
    public float speed;

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