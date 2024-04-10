using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject scoreValue;
    public GameObject gameOverLabel;
    public Slider healthSlider;
    public Canvas healthCanvas;
    public GameObject player;

    private int score;
    private HealthController playerHealthController;
    private float currentTime;

    private void Start()
    {
        gameOverLabel.SetActive(false);

        playerHealthController = player.GetComponent<HealthController>();
        healthSlider.maxValue = playerHealthController.health;
        healthSlider.value = playerHealthController.health;
    }

    private void Update()
    {
        if (!gameOverLabel.activeInHierarchy)
        {
            healthSlider.value = playerHealthController.currentHealth;
        }
        else
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
        healthCanvas.gameObject.SetActive(false);

        currentTime = 10f;
    }
}