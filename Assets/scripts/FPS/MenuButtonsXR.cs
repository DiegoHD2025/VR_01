using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsXR : MonoBehaviour
{
    // Reinicia el juego (vuelve a Practica06 y reinicia el puntaje)
    public void ReiniciarJuego()
    {
        // Reiniciar puntaje actual
        PlayerPrefs.SetInt("FinalScore", 0);
        SceneManager.LoadScene("Practica06");
    }

    // Sale del juego (o detiene la ejecución en el editor)
    public void SalirJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
