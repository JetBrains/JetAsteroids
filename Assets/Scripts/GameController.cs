using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject scoreValue;
    public GameObject gameOverLabel;

    private int score;

    private float currentTime;

    private void Start()
    {
        gameOverLabel.SetActive(false);
    }

    private void Update()
    {
        if (gameOverLabel.activeInHierarchy)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void IncrementScore()
    {
        score = score + 10;
        scoreValue.GetComponent<TMP_Text>().text = score.ToString();
    }

    public void GameOver()
    {
        gameOverLabel.SetActive(true);

        currentTime = 10f;
    }
}