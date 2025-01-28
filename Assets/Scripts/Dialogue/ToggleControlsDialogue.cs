using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class ToggleControlsDialogue : MonoBehaviour
{
    [SerializeField] private PlayerInput playerControls;
    [SerializeField] private PlayerInput inventoryControls;

    public void DisableControls() {
        playerControls.DeactivateInput();
        inventoryControls.DeactivateInput();
    }

    public void EnableControls() {
        playerControls.ActivateInput();
        inventoryControls.ActivateInput();
    }
}
