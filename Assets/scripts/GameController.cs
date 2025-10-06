using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public Player player;
    public Ball ball;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    public GameObject gameOverCanvas; // 🟢 Nuevo: Canvas con los botones de Game Over

    private bool isGameOver = false;

    void Start()
    {
        // Mostrar el highscore almacenado
        int highscore = PlayerPrefs.GetInt("scoreHigh", 0);
        highscoreText.text = "Highscore: " + highscore;
        Debug.Log("Highscore inicial: " + highscore);

        // Asegúrate de ocultar el canvas al principio
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Solo verificar game over una vez
        if (!isGameOver && ball.transform.position.z < player.transform.position.z)
        {
            isGameOver = true;
            HandleGameOver();
        }

        if (!isGameOver)
        {
            scoreText.text = "Score: " + ball.score;
        }
    }

    void HandleGameOver()
    {
        scoreText.text = "Game over!\nYour final score: " + ball.score;

        // Guardar el highscore si es necesario
        int highscore = PlayerPrefs.GetInt("scoreHigh", 0);
        if (ball.score > highscore)
        {
            PlayerPrefs.SetInt("scoreHigh", ball.score);
            PlayerPrefs.Save();
            highscoreText.text = "Highscore: " + ball.score;
        }

        // Mostrar el menú de Game Over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    // Estas funciones las llamas desde los botones con Gaze
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Practica03"); // Cambia "MainMenu" al nombre real de tu escena del menú
    }
}
