using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuActivation : MonoBehaviour
{
    public string menuType;
    private bool isOn;
    [SerializeField] private PlayerInput playerControls;
    [SerializeField] protected GameObject selectFirst;

    void Start() {
        isOn = false;
        gameObject.SetActive(isOn);
    }

    public void ActivateWindow() {
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

    public void ToggleMenuControls(bool isOn) {
        switch (menuType) {
            case "pause":
                break;
            case "craft":
                break;
            case "accessibility":
                break;
            default:
                break;
        }
    }

    protected IEnumerator SetFirstButton() {
        yield return null;
        EventSystem.current.SetSelectedGameObject(selectFirst);
    }

    public void GoToMenu(GameObject targetMenu) {
        targetMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
