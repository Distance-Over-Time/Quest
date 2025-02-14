using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CraftingMenu : MonoBehaviour
{
    private bool isOn;
    [SerializeField] private PlayerInput playerControls;
    [SerializeField] private EventSystem optionsEvents;

    void Start() {
        isOn = false;
        gameObject.SetActive(isOn);
    }

    public void ActivateCraftWindow() {
        isOn = !isOn;
        gameObject.SetActive(isOn);
        optionsEvents.enabled = !isOn; // Need this to deal with making the options menu selectable
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
