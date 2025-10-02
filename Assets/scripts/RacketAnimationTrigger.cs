using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketAnimationTrigger : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Asegúrate de que el objeto tiene un Animator
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Asegúrate de que la pelota tiene el tag "Ball"
        {
            animator.SetTrigger("Hit"); // Usa un trigger llamado "Hit" en tu Animator
        }
    }
}

