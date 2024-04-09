using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health = 100;
    public int currentHealth;
    public GameObject explosion;
    public AudioClip explosionAudioClip;

    private AudioSource audioSource;

    private bool isExploding = false;

    private void Start()
    {
        currentHealth = health;
        audioSource = GetComponent<AudioSource>();
    }

    public void DealDamage(int damage)
    {
        currentHealth = Math.Max(0, currentHealth - damage);

        if (currentHealth <= 0 && !isExploding)
        {
            isExploding = true;

            var explosionGameObject = Instantiate(explosion, transform.position,
                Quaternion.Euler(0, 0, 0));

            if (audioSource != null)
            {
                audioSource.PlayOneShot(explosionAudioClip);
            }

            Destroy(gameObject, 0.5f);
            Destroy(explosionGameObject, 1f);

            if (!gameObject.CompareTag("Player"))
            {
                GameObject.FindWithTag("GameController")?.GetComponent<GameController>()?.IncrementScore();
            }
        }
    }
}