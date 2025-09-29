using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject[] objectsToToggle;

    private bool isVisible = false;

    public void ToggleVisibility()
    {
        isVisible = !isVisible;

        foreach (var obj in objectsToToggle)
        {
            obj.SetActive(isVisible);
        }
    }
}

