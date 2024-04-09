using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFragmenter : MonoBehaviour
{
    public GameObject asteroidFragment;
    public int minimumFragments = 2;
    public int maximumFragments = 8;
    public float minimumScale;

    private Vector3 screenCenter;

    private void Start()
    {
        screenCenter = Camera.main!.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    }

    public void Fragment()
    {
        if (transform.localScale.x <= minimumScale) return;

        var asteroidTransformPosition = transform.position;

        var numberOfFragments = UnityEngine.Random.Range(minimumFragments, maximumFragments);

        for (var i = 0; i < numberOfFragments; i++)
        {
            // Create fragment
            var fractureGameObject = Instantiate(asteroidFragment, new Vector3(asteroidTransformPosition.x, asteroidTransformPosition.y, asteroidTransformPosition.z), Quaternion.Euler(0, 0, 0));
            fractureGameObject.transform.LookAt(screenCenter);

            var scale = transform.localScale.x / 2;
            fractureGameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}