using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed;
    public float movementSpeed;
    public GameObject laser;
    public GameObject[] cannons;
    public GameObject thruster;
    public float cooldown = 1f;

    public AudioClip thrustAudioClip;
    public AudioClip fireAudioClip;

    private Rigidbody rigidBody;
    private int currentCannon = 0;
    private AudioSource audioSource;
    private ParticleSystem thrusterParticles;

    private float time = 0f;

    private Collider[] colliders;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        colliders = GetComponents<Collider>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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

            audioSource.PlayOneShot(fireAudioClip);
        }

        // Player movements
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (!audioSource.isPlaying || audioSource.clip == fireAudioClip)
            {
                audioSource.loop = true;
                audioSource.clip = thrustAudioClip;
                audioSource.Play();
            }

            rigidBody.AddForce(transform.forward * (movementSpeed * Time.deltaTime));

            if (!thrusterParticles.isPlaying)
            {
                thrusterParticles.Play();
            }
        }
        else
        {
            thrusterParticles.Stop();
            if (audioSource.clip == thrustAudioClip)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }
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
            var screenCenter = _camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            transform.position = new Vector3(screenCenter.x - playerTransformPosition.x, screenCenter.y - playerTransformPosition.y, 0);
        }
    }

    private bool IsPlayerVisible()
    {
        var collider = colliders.First(it => it.isTrigger);

        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        return GeometryUtility.TestPlanesAABB(planes, collider.bounds);
    }

    private void OnDestroy()
    {
        GameObject.FindWithTag("GameController")?.GetComponent<GameController>()?.GameOver();
    }
}