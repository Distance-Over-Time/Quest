using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemValue : MonoBehaviour
{
    [SerializeField] private string gameName;
    [SerializeField, TextArea(3, 5)] private string flavorDesc;
    [SerializeField] private Image image;

    public string GetGameName() {
        return gameName;
    }

    public string GetFlavorDesc() {
        return flavorDesc;
    }

    public Image GetKeyImage() {
        return image;
    }

}
