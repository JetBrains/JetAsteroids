using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject[] cannons;
    [SerializeField] private GameObject thruster;
    [SerializeField] private float cooldown = 1f;

    [SerializeField] private AudioClip thrustAudioClip;
    [SerializeField] private AudioClip fireAudioClip;

    private Rigidbody rigidBody;
    private int currentCannon = 0;

    private AudioManager audioManager;
    private ParticleSystem thrusterParticles;

    private float time = 0f;

    private Collider[] colliders;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        colliders = GetComponents<Collider>();
        rigidBody = GetComponent<Rigidbody>();

        audioManager = GameObject.FindGameObjectWithTag("AudioManager")?.GetComponent<AudioManager>();

        thrusterParticles = thruster.GetComponent<ParticleSystem>();
        thrusterParticles.Stop();
    }

    private void Update()
    {
        // Laser
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            // Make laser originate from alternate canon on each shot
            var laserOriginTransform = transform;
            if (cannons.Length > 0)
            {
                laserOriginTransform = cannons[currentCannon++].transform;

                if (currentCannon >= cannons.Length)
                {
                    currentCannon = 0;
                }
            }

            Instantiate(laser, laserOriginTransform
                .TransformPoint(Vector3.forward * 2), transform.rotation);
            time = cooldown;

            audioManager?.PlaySfx(fireAudioClip);
        }

        // Player movements
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            audioManager?.PlaySfx(thrustAudioClip, true);

            rigidBody.AddForce(transform.forward * (movementSpeed * Time.deltaTime));

            if (!thrusterParticles.isPlaying)
            {
                thrusterParticles.Play();
            }
        }
        else
        {
            thrusterParticles.Stop();
            audioManager?.StopSfx(thrustAudioClip);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(transform.forward * (-movementSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * (rotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        }

        // Move through screen borders
        if (!IsPlayerVisible())
        {
            var playerTransformPosition = transform.position;
            var screenCenter = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            transform.position = new Vector3(screenCenter.x - playerTransformPosition.x, screenCenter.y - playerTransformPosition.y, 0);
        }
    }

    private bool IsPlayerVisible()
    {
        var collider = colliders.First(it => it.isTrigger);

        var planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, collider.bounds);
    }

    private void OnDestroy()
    {
        audioManager?.StopSfx(thrustAudioClip);
        GameObject.FindWithTag("GameController")?.GetComponent<GameController>()?.GameOver();
    }
}