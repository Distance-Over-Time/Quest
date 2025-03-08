using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseUI : MenuActivation
{
    protected override void OnEnable() {
        base.OnEnable();
        // EventSystem.current.SetSelectedGameObject(selectFirst);
        selectFirst.Select();
    }

    public void ResumeGame() {
        ActivateWindow();
    }

    public void QuitGame() {

    }

    public void AccessCreditsMenu() {

    }
}
