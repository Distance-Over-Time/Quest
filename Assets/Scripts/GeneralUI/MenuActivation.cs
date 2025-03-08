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
    public string menuType;

    private InputManager inputManager;
    protected InputActionMap pauseUIMap;

    protected virtual void Awake() {
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

    protected virtual void Start() {
        isOn = false;
        gameObject.SetActive(isOn);
    }

    public void ActivateWindow() {
        isOn = !isOn;
        ToggleWindow(isOn);
        ToggleMovementControls(isOn);
        
        if (isOn == true) {
            StartCoroutine(SetFirstButton());
        }
    }

    public void ToggleWindow(bool isOn) {
        gameObject.SetActive(isOn);
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

    protected IEnumerator SetFirstButton() {
        yield return null;
        selectFirst.Select();
    }

    public void GoToMenu(GameObject targetMenu) {
        targetMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
