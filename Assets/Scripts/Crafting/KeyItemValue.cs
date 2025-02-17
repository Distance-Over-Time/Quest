using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemValue : MonoBehaviour
{
    [SerializeField] private string gameName;
    [SerializeField, TextArea(3, 5)] private string flavorDesc;
    [SerializeField] private Image image;
    [SerializeField] private string craftYarnVariableName;

    public string GetGameName() {
        return gameName;
    }

    public string GetFlavorDesc() {
        return flavorDesc;
    }

    public Image GetKeyImage() {
        return image;
    }

    // This is for updating the yarn variable that each NPC checks for progressing the story
    public string GetYarnCraftingVariable() {
        return craftYarnVariableName;
    }

}
