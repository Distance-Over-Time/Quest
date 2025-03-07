using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CraftingUI : MenuActivation {
    private InputActionMap craftUIMap;

    void Awake() {
        base.Awake();
        craftUIMap = GetInputManager().Crafting;
    }

    protected override void OnEnable() {
        craftUIMap.Enable();
    }

    protected override void OnDisable() {
        craftUIMap.Disable();
    }
}
