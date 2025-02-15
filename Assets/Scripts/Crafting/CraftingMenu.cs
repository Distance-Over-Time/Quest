using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CraftingMenu : MonoBehaviour
{
    private bool isOn;
    [SerializeField] private PlayerInput playerControls;
    [SerializeField] private GameObject firstMat;

    void Start() {
        isOn = false;
        gameObject.SetActive(isOn);
    }

    public void ActivateCraftWindow() {
        isOn = !isOn;
        gameObject.SetActive(isOn);
        ToggleMovementControls(isOn);
        
        if (isOn == true) {
            StartCoroutine(SetFirstButton());
        }
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

    IEnumerator SetFirstButton() {
        // just let a girl load into existence, okay? jesus
        yield return null;
        EventSystem.current.SetSelectedGameObject(firstMat);
    }

}
