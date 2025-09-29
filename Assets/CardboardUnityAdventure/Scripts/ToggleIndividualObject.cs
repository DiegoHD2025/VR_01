using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleIndividualObject : MonoBehaviour
{
    public GameObject objectToToggle;

    public void SetObjectVisibility(bool state)
    {
        objectToToggle.SetActive(state);
    }
}

