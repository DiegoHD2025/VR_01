using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

	public Player player;
	public Ball ball;
	public TextMeshProUGUI scoreText;

	private float gameOverTimer = 3f;

	// Use this for initialization
	void Start () {
        Debug.Log(PlayerPrefs.GetInt("scoreHigh"));
    }
	
	// Update is called once per frame
	void Update () {
		bool isGameOver = ball.transform.position.z < player.transform.position.z;

		if (isGameOver == false) {
			scoreText.text = "Score: " + ball.score;
		} else {
			scoreText.text = "Game over!\nYour final score: " + ball.score;

			gameOverTimer -= Time.deltaTime;
			if (gameOverTimer <= 0f) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			}
		}
	}
}
