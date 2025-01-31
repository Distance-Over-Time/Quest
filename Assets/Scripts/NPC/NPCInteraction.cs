using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NPCInteraction : MonoBehaviour
{
    public SpriteRenderer interactIcon;
    private bool isIconShowing;
    private PlayerInput npcAction;

    void Start()
    {
        npcAction = gameObject.GetComponent<PlayerInput>();
        npcAction.DeactivateInput();
        HideIcon();
    }

    public void ShowIcon() {
        isIconShowing = true;
        interactIcon.enabled = isIconShowing;
    }

    public void HideIcon() {
        isIconShowing = false;
        interactIcon.enabled = isIconShowing;
    }
        
    public void PlayerInRange() {
        ShowIcon();
        npcAction.ActivateInput();
    }

    public void PlayerOutOfRange() {
        HideIcon();
        npcAction.DeactivateInput();
    }

    public bool GetIcon() {
        return isIconShowing;
    }
}
