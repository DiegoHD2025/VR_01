using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    private Camera xrCamera;

    void Start()
    {
        // Obtenemos la cámara desde el CameraPointerManager
        xrCamera = CameraPointerManager.instance.gameObject.GetComponent<Camera>();
    }

    // Cargar escena del menú principal
    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu_principal");
    }

    // Cargar escena para reiniciar el juego
    public void Reiniciar()
    {
        SceneManager.LoadScene("Juego_principal_vr");
    }

    // Cargar escena de Game Over
    public void GameOver()
    {
        SceneManager.LoadScene("Menu_Game_Over");
    }

    // Evento de clic en UI para VR
    public void OnPointerClick()
    {
        PointerEventData pointerEvent = PlacePointer();

        // Ejecuta el evento de clic sobre el elemento UI seleccionado
        ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, pointerEvent, ExecuteEvents.pointerClickHandler);
    }

    // Crear un PointerEventData con la posición en pantalla donde se hizo clic
    private PointerEventData PlacePointer()
    {
        Vector3 screenPos = xrCamera.WorldToScreenPoint(CameraPointerManager.instance.hitPoint);
        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(screenPos.x, screenPos.y)
        };

        return pointer;
    }
}

