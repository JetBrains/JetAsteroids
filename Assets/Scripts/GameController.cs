using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject scoreValue;
    public GameObject gameOverLabel;
    private int score;

    private void Start()
    {
        gameOverLabel.SetActive(false);
    }

    public void IncrementScore()
    {
        score = score + 10;
        scoreValue.GetComponent<TMP_Text>().text = score.ToString();
    }

    public void GameOver()
    {
        gameOverLabel.SetActive(true);
    }
}