using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using SOHNE.Accessibility.Colorblindness;

/* 
    (In order of the buttons on the UI)
    
    MODE                INT
    -----------------------
    Normal               0
    Achromatopsia        7
    Achromatomaly        8
    Deuteranopia         3
    Deuteranomaly        4
    Protanopia           1
    Protanomaly          2
    Tritanopia           5
    Tritanomaly          6
*/


public class ColorblindUI : MenuActivation {
    private Colorblindness colorblindnessInstance;

    protected override void Start() {
        base.Start();
        colorblindnessInstance = Colorblindness.Instance;

        if (colorblindnessInstance == null) {
            Debug.LogError("Colorblindness instance not found");
        }

        // Ensure we start on default mode/no colorblind filter
        SetColorblindMode(0);

        gameObject.SetActive(false);
    }

    protected override void OnEnable() {
        base.OnEnable();
        selectFirst.Select();
        pauseUIMap.FindAction("Activate")?.Disable();
    }

    public void SetColorblindMode(int modeIndex) {
        if (colorblindnessInstance != null) {
            colorblindnessInstance.Change(modeIndex);
        }
        else {
            Debug.LogError("Colorblindness instance is null.");
        }
    }
}
