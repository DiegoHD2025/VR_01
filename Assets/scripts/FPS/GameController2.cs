using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController2 : MonoBehaviour
{
    public Camera gameCamera;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;

    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text puntaje;

    public float enemySpawningCooldown = 1f;
    public float enemySpawningDistance = 7f;
    public float shootingCooldown = 0.5f;

    private float enemySpawningTimer = 0;
    private float shootingTimer = 0;
    private int npuntaje = 0;

    void Start()
    {
    }

    void Update()
    {
        shootingTimer -= Time.deltaTime;
        enemySpawningTimer -= Time.deltaTime;

        if (enemySpawningTimer <= 0f && !menu.activeSelf)
        {
            enemySpawningTimer = enemySpawningCooldown;

            GameObject enemyObject = Instantiate(enemyPrefab);

            float randomAngle = Random.Range(0, Mathf.PI * 2);
            enemyObject.transform.position = new Vector3(
                gameCamera.transform.position.x + Mathf.Cos(randomAngle) * enemySpawningDistance,
                0,
                gameCamera.transform.position.z + Mathf.Sin(randomAngle) * enemySpawningDistance
            );

            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.direction = (gameCamera.transform.position - enemy.transform.position).normalized;
            enemy.transform.LookAt(Vector3.zero);
        }

        RaycastHit hit;

        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy" && shootingTimer <= 0f)
            {
                shootingTimer = shootingCooldown;

                GameObject bulletObject = Instantiate(bulletPrefab);
                bulletObject.transform.position = gameCamera.transform.position;

                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.direction = gameCamera.transform.forward;

                npuntaje += 100;
                puntaje.text = "Puntaje: " + npuntaje;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            menu_principal();
        }
    }

    void menu_principal()
    {
        SceneManager.LoadScene("Menu_principal");
    }
}

