using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController2 : MonoBehaviour
{
    public static GameController2 instance; // Instancia global del controlador

    // Referencias públicas
    public Camera gameCamera;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    [SerializeField] public GameObject menu;
    [SerializeField] TMP_Text puntaje; // Texto que muestra el puntaje actual
    [SerializeField] TMP_Text highscoreText; // NUEVO: Texto que muestra el highscore (en Practica06)

    // Configuraciones
    public float enemySpawningCooldown = 1f;
    public float enemySpawningDistance = 7f;
    public float shootingCooldown = 0.5f;

    // Variables internas
    private float enemySpawningTimer = 0;
    private float shootingTimer = 0;
    private int npuntaje = 0;

    private int highscore = 0; // NUEVO: almacenará el puntaje más alto

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        // Cargar highscore guardado
        highscore = PlayerPrefs.GetInt("Highscore", 0);

        // Mostrar highscore si estamos en la escena Practica06
        if (highscoreText != null)
        {
            highscoreText.text = "Highscore: " + highscore;
        }
    }

    void Update()
    {
        shootingTimer -= Time.deltaTime;
        enemySpawningTimer -= Time.deltaTime;

        // Spawneo de enemigos
        if (enemySpawningTimer <= 0f && !menu.activeSelf)
        {
            enemySpawningTimer = enemySpawningCooldown;
            GameObject enemyObject = Instantiate(enemyPrefab);

            float randomAngle = Random.Range(0f, Mathf.PI * 2f);
            enemyObject.transform.position = new Vector3(
                gameCamera.transform.position.x + Mathf.Cos(randomAngle) * enemySpawningDistance,
                0f,
                gameCamera.transform.position.z + Mathf.Sin(randomAngle) * enemySpawningDistance
            );

            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.direction = (gameCamera.transform.position - enemy.transform.position).normalized;
            enemy.transform.LookAt(gameCamera.transform.position);
        }

        // Disparo (raycast hacia adelante)
        RaycastHit hit;
        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Enemy") && shootingTimer <= 0f)
            {
                shootingTimer = shootingCooldown;

                GameObject bulletObject = Instantiate(bulletPrefab);
                bulletObject.transform.position = gameCamera.transform.position;

                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.direction = gameCamera.transform.forward;

                npuntaje += 100;
                puntaje.text = "Puntaje: " + npuntaje;

                // Actualizar highscore si es necesario
                if (npuntaje > highscore)
                {
                    highscore = npuntaje;
                    PlayerPrefs.SetInt("Highscore", highscore);
                    PlayerPrefs.Save();

                    if (highscoreText != null)
                        highscoreText.text = "Highscore: " + highscore;
                }
            }
        }
    }

    // Función para ir al menú principal (Practica07)
    public void menu_principal()
    {
        // Guardar el puntaje final para mostrarlo en la siguiente escena
        PlayerPrefs.SetInt("FinalScore", npuntaje);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Practica07");
    }

    // Método para actualizar el texto del puntaje final en Practica07
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Practica07")
        {
            // Buscar el texto de FinalScore en el canvas de Practica07
            TMP_Text finalScoreText = GameObject.Find("FinalScore")?.GetComponent<TMP_Text>();
            if (finalScoreText != null)
            {
                int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
                int bestScore = PlayerPrefs.GetInt("Highscore", 0);
                finalScoreText.text = "Final Score: " + finalScore + "\nHighscore: " + bestScore;
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
