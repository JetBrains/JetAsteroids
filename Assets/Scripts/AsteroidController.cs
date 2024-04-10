using System;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public int damage;
    public float damageCooldown = 0.5f;

    private float currentTime;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 100f);
    }

    private void OnBecameInvisible() => Destroy(gameObject);

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (currentTime <= 0f)
        {
            other.gameObject.GetComponent<HealthController>()?.DealDamage(damage);
            currentTime = damageCooldown;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        currentTime = 0f;
    }
}