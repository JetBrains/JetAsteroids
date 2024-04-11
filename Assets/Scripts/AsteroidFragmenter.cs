using System;
using UnityEngine;

public class AsteroidFragmenter : MonoBehaviour
{
    [SerializeField] private GameObject asteroidFragment;
    [SerializeField] private int minimumFragments = 2;
    [SerializeField] private int maximumFragments = 8;
    [SerializeField] private float minimumScale;

    public void Fragment()
    {
        if (transform.localScale.x <= minimumScale) return;

        var asteroidTransformPosition = transform.position;

        var numberOfFragments = UnityEngine.Random.Range(minimumFragments, maximumFragments);

        for (var i = 0; i < numberOfFragments; i++)
        {
            // Create fragment
            var fractureGameObject = Instantiate(asteroidFragment,new Vector3(asteroidTransformPosition.x, asteroidTransformPosition.y, asteroidTransformPosition.z), Quaternion.Euler(0, 0, 0));

            // Randomize direction
            fractureGameObject.transform.LookAt(
                new Vector3(UnityEngine.Random.Range(Screen.width * -1, Screen.width), UnityEngine.Random.Range(Screen.height * -1, Screen.height), 0));

            if (fractureGameObject.TryGetComponent<AsteroidController>(out var fractureAsteroidController))
            {
                fractureAsteroidController.damage = (int)Math.Max(10, fractureAsteroidController.damage / 2f);
            }

            var scale = transform.localScale.x / 2;
            fractureGameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}