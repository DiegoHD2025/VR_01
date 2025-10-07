using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float ballProximity = 4f;

    public bool canPick = false;
    public bool picked = false;
    public bool won = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canPick)
        {
            if (Input.GetKeyDown("space"))
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    Cup cup = hit.transform.GetComponent<Cup>();
                    if (cup != null)
                    {
                        canPick = false;
                        picked = true;
                        won = (cup.ball != null);
                        cup.MoveUp();
                    }

                    // Este bloque parece innecesario en el contexto de "cup picking"
                    // Pero si también quieres detectar si golpeaste una bola directamente:
                    Ball ball = hit.transform.GetComponent<Ball>();
                    if (ball != null)
                    {
                        if (ball.transform.position.z - transform.position.z < ballProximity && ball.direction.z < 0)
                        {
                            ball.OnPlayerHit();
                        }
                    }
                }
            }
        }
    }
}