using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MenuActivation
{
    void OnEnable() {
        EventSystem.current.SetSelectedGameObject(selectFirst);
    }

    public void ResumeGame() {
        ActivateWindow();
    }

    public void QuitGame() {

    }

    public void AccessCreditsMenu() {

    }
}
