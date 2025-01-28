using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingMenu : MonoBehaviour
{
    private bool isOn;
    [SerializeField] private PlayerInput playerControls;

    void Awake() {
        isOn = false;
        gameObject.SetActive(isOn);
    }

    public void ActivateCraftWindow() {
        isOn = !isOn;
        gameObject.SetActive(isOn);
        ToggleMovementControls(isOn);
    }

    public bool GetActiveStatus() {
        return isOn;
    }

    public void ToggleMovementControls(bool isOn) {
        if (isOn) {
            playerControls.DeactivateInput();
        }
        else {
            playerControls.ActivateInput();
        }
    }
}
