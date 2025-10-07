using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< Updated upstream

public class GameController : MonoBehaviour {

	public Player player;
	public Ball ball;
	public TextMesh scoreText;

	private float gameOverTimer = 3f;

	// Use this for initialization
	void Start () {
	
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
=======
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour {

	public TextMeshProUGUI infoText;
	public GameObject ball;
	public Player player;
	public Cup[] cups;

	private float resetTimer = 3f;

	// Use this for initialization
	void Start () {
		infoText.text = "¡Elige la copa correcta!";

		StartCoroutine (ShuffleRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		if (player.picked) {
			if (player.won) {
				infoText.text = "Ganaste!";
			} else {
				infoText.text = "Perdiste :( Intenta de nuevo!";
			}

			resetTimer -= Time.deltaTime;
			if (resetTimer <= 0f) {
>>>>>>> Stashed changes
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			}
		}
	}
<<<<<<< Updated upstream
=======

	private IEnumerator ShuffleRoutine () {
		yield return new WaitForSeconds (1f);

		foreach (Cup cup in cups) {
			cup.MoveUp ();
		}

		yield return new WaitForSeconds (0.5f);

		Cup targetCup = cups[Random.Range(0, cups.Length)];
		targetCup.ball = ball;
		ball.transform.position = new Vector3 (
			targetCup.transform.position.x,
			ball.transform.position.y,
			targetCup.transform.position.z
		);

		yield return new WaitForSeconds (1.0f);

		foreach (Cup cup in cups) {
			cup.MoveDown ();
		}

		yield return new WaitForSeconds (1.0f);

		for (int i = 0; i < 5; i++) {
			Cup cup1 = cups[Random.Range(0, cups.Length)];
			Cup cup2 = cup1;

			while (cup2 == cup1) {
				cup2 = cups[Random.Range(0, cups.Length)];
			}

			Vector3 cup1Position = cup1.targetPosition;

			cup1.targetPosition = cup2.targetPosition;
			cup2.targetPosition = cup1Position;

			yield return new WaitForSeconds (0.75f);
		}

		player.canPick = true;
	}
>>>>>>> Stashed changes
}
