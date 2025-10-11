using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonHandler : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Regresando a la escena Practica08...");
        SceneManager.LoadScene("Practica08");
    }
}

