using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Reflection;
using SOHNE.Accessibility.Colorblindness;

public class ColorblindUI : MenuActivation
{
    private Colorblindness colorblindnessInstance;

    void Start() {
        colorblindnessInstance = Colorblindness.Instance;

        if (colorblindnessInstance == null) {
            Debug.LogError("Colorblindness instance not found");
        }

        // Ensure we start on default mode/no colorblind filter
        colorblindnessInstance.Change(0);

        gameObject.SetActive(false);
    }

    void OnEnable() {
        EventSystem.current.SetSelectedGameObject(selectFirst);
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
