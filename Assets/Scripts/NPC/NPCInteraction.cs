using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactIcon;
    private bool isIconShowing;
    private PlayerInput npcAction;

    void Awake() {
        interactIcon.SetActive(false);
        isIconShowing = false;
    }

    void Start()
    {
        npcAction = gameObject.GetComponent<PlayerInput>();
        npcAction.DeactivateInput();
    }

    public void ShowIcon() {
        interactIcon.SetActive(true);
        isIconShowing = true;
    }

    public void HideIcon() {
        interactIcon.SetActive(false);
        isIconShowing = false;
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
