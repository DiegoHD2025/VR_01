using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public GameObject ball;
    public Player player;
    public Cup[] cups;

    private float resetTimer = 3f;

    void Start()
    {
        infoText.text = "¡Elige la copa correcta!";
        StartCoroutine(ShuffleRoutine());
    }

    void Update()
    {
        if (player.picked)
        {
            if (player.won)
            {
                infoText.text = "¡Ganaste!";
            }
            else
            {
                infoText.text = "Perdiste :( ¡Intenta de nuevo!";
            }

            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private IEnumerator ShuffleRoutine()
    {
        yield return new WaitForSeconds(1f);

        // Levanta todas las copas
        foreach (Cup cup in cups)
        {
            cup.MoveUp();
        }

        yield return new WaitForSeconds(0.5f);

        // Escoge una copa al azar para esconder la bola
        Cup targetCup = cups[Random.Range(0, cups.Length)];
        targetCup.ball = ball;
        ball.transform.position = new Vector3(
            targetCup.transform.position.x,
            ball.transform.position.y,
            targetCup.transform.position.z
        );

        yield return new WaitForSeconds(1.0f);

        // Baja todas las copas
        foreach (Cup cup in cups)
        {
            cup.MoveDown();
        }

        yield return new WaitForSeconds(1.0f);

        // Mezcla las copas 5 veces
        for (int i = 0; i < 5; i++)
        {
            Cup cup1 = cups[Random.Range(0, cups.Length)];
            Cup cup2 = cup1;

            while (cup2 == cup1)
            {
                cup2 = cups[Random.Range(0, cups.Length)];
            }

            Vector3 cup1Pos = cup1.targetPosition;
            cup1.targetPosition = cup2.targetPosition;
            cup2.targetPosition = cup1Pos;

            yield return new WaitForSeconds(0.75f);
        }

        // El jugador ahora puede elegir una copa
        player.canPick = true;
    }
}
