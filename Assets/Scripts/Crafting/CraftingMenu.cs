using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    private bool isOn;

    void Awake() {
        isOn = false;
        gameObject.SetActive(isOn);
    }

    public void ActivateCraftWindow() {
        isOn = !isOn;
        gameObject.SetActive(isOn);
    }
}
