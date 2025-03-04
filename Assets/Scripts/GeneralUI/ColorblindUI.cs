using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Reflection;

public class ColorblindUI : MenuActivation
{
    void Start() {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(selectFirst);
    }

    public void ChangeMode(int mode)
    {
        // TODO: change for SOHNE colorblindness
        Debug.Log(mode);
    }
}