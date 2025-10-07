using UnityEngine;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI incorrectText;

    public GameObject ball;
    public Player player;
    public Cup[] cups;

    private float resetTimer = 3f;
    private int correctCount = 0;
    private int incorrectCount = 0;
    private bool roundCounted = false;

    void Start()
    {
        roundCounted = false;
        infoText.text = "¡Elige la copa correcta!";
        UpdateScoreUI();
        StartCoroutine(ShuffleRoutine());
    }

    void Update()
    {
        if (player.picked && !roundCounted)
        {
            roundCounted = true;

            if (player.won)
            {
                correctCount++;
                infoText.text = "¡Ganaste!";
            }
            else
            {
                incorrectCount++;
                infoText.text = "Perdiste :( ¡Intenta de nuevo!";
            }

            UpdateScoreUI();
        }

        if (player.picked)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f)
            {
                ResetRound();
            }
        }
    }

    private void UpdateScoreUI()
    {
        correctText.text = "Aciertos: " + correctCount;
        incorrectText.text = "Fallos: " + incorrectCount;
    }

    private void ResetRound()
    {
        // Reset variables
        player.picked = false;
        player.canPick = false;
        player.won = false;

        resetTimer = 3f;
        roundCounted = false;

        // Quitar bola de todas las copas
        foreach (Cup cup in cups)
        {
            cup.ball = null;
        }

        // Iniciar nueva ronda
        infoText.text = "¡Elige la copa correcta!";
        StartCoroutine(ShuffleRoutine());
    }

    private IEnumerator ShuffleRoutine()
    {
        yield return new WaitForSeconds(1f);

        foreach (Cup cup in cups)
        {
            cup.MoveUp();
        }

        yield return new WaitForSeconds(0.5f);

        Cup targetCup = cups[Random.Range(0, cups.Length)];
        targetCup.ball = ball;
        ball.transform.position = new Vector3(
            targetCup.transform.position.x,
            ball.transform.position.y,
            targetCup.transform.position.z
        );

        yield return new WaitForSeconds(1.0f);

        foreach (Cup cup in cups)
        {
            cup.MoveDown();
        }

        yield return new WaitForSeconds(1.0f);

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

        player.canPick = true;
    }
}
