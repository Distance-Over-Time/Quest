using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuActivation : MonoBehaviour {
    private bool isOn;
    [SerializeField] private PlayerInput playerControls;
    [SerializeField] protected Selectable selectFirst;

    private InputManager inputManager;
    private InputActionMap pauseUIMap;

    protected void Awake() {
        inputManager = new InputManager();
        pauseUIMap = GetInputManager().UI;
    }
    
    protected virtual void OnDisable() {
        pauseUIMap.Disable();
    }

    protected virtual void OnEnable() {
        pauseUIMap.Enable();
    }

    protected InputManager GetInputManager() {
        return inputManager;
    }

    protected void Start() {
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

    // public void ToggleMenuControls(bool isOn) {
    //     if (isOn) {
    //     // Note: Making this a switch in case we decide to revamp the menus in the future
    //         switch (menuType) {
    //             case "pause":
    //                 // deactivate craft menu input
    //                 break;
    //             case "craft":
    //                 // deactive pause menu input
    //                 break;
    //             default:
    //                 Debug.Log("No 'menuType' string was provided in this MenuActivation component -- no action taken");
    //                 break;
    //         }
    //     }
    // }

    protected IEnumerator SetFirstButton() {
        yield return null;
        // EventSystem.current.SetSelectedGameObject(selectFirst);
        selectFirst.Select();
    }

    public void GoToMenu(GameObject targetMenu) {
        targetMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
