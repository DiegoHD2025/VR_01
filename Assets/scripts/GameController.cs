using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{

    public TextMeshProUGUI infoText;
    public TextMeshProUGUI correctCountText; // Texto para aciertos
    public TextMeshProUGUI failCountText;    // Texto para fallos

    public GameObject ball;
    public Player player;
    public Cup[] cups;

    private float resetTimer = 3f;

    private int correctCount = 0;
    private int failCount = 0;
    private bool resultRegistered = false;

    // Use this for initialization
    void Start()
    {
        correctCount = PlayerPrefs.GetInt("CorrectCount", 0);
        failCount = PlayerPrefs.GetInt("FailCount", 0);

        infoText.text = "Elige la copa correcta!";
        UpdateScoreUI();
        resultRegistered = false;
        resetTimer = 3f;
        StartCoroutine(ShuffleRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.picked && !resultRegistered)
        {
            if (player.won)
            {
                infoText.text = "Ganaste!";
                correctCount++;
            }
            else
            {
                infoText.text = "Perdiste :( intentalo de nuevo!";
                failCount++;
            }

            UpdateScoreUI();
            resultRegistered = true; // Marca que ya se registró el resultado
        }

        if (resultRegistered)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }


    void UpdateScoreUI()
    {
        correctCountText.text = "Aciertos: " + correctCount;
        failCountText.text = "Fallos: " + failCount;

        PlayerPrefs.SetInt("CorrectCount", correctCount);
        PlayerPrefs.SetInt("FailCount", failCount);
        PlayerPrefs.Save();
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

            Vector3 cup1Position = cup1.targetPosition;

            cup1.targetPosition = cup2.targetPosition;
            cup2.targetPosition = cup1Position;

            yield return new WaitForSeconds(0.75f);
        }

        player.canPick = true;
    }
}
