using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad = "Practica04"; // Reemplaza con el nombre de tu escena del juego

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit pressed - Application.Quit() called");
    }
}

