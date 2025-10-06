using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.EventSystems;
public class UIElementXR : MonoBehaviour
{
    public UnityEvent OnXRPointerEnter;
    public UnityEvent OnXRPointerExit;
    private Camera xRCamera;
    // Start is called before the first frame update
    void Start()
    {
        xRCamera = CameraPointerManager.instance.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    public void OnPointerClicXR()
    {
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerClickHandler);
    }

    public void OnPointerEnterXR()
    {
        GazeManager.Instance.SetUpGaze(1.5f);
        OnXRPointerEnter?.Invoke();

        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);

        // Dispara el clic después de 1.5 segundos si no lo manejas en otro lado
        Invoke("OnPointerClicXR", 1.5f);
    }

    public void OnPointerExitXR()
    {
        GazeManager.Instance.SetUpGaze(2.5f);
        OnXRPointerExit?.Invoke();

        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerUpHandler);
    }

    public PointerEventData PlacePointer()
    {
        Vector3 screePos = xRCamera.WorldToScreenPoint(CameraPointerManager.instance.hitPoint);
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(screePos.x, screePos.y);
        return pointer;
    }
}
