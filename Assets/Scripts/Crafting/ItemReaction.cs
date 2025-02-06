using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ItemReaction : YarnStorageConnection
{
    [SerializeField] private string discoveryName;
    // [SerializeField] private Material mat;
    [SerializeField] private Material grayscaleMat;
    [SerializeField] private Material defaultMat;
    private Image image;
    private bool found;

    void Start() {
        // need to make yarn boolean for discoverability for each material
        found = false;

        // The material will set the sprite to grayscale at default
        image = GetComponent<Image>();
        defaultMat = Canvas.GetDefaultCanvasMaterial();
        SetColor(found);
    }

    // Update is called once per frame
    void OnEnable()
    {
        if (found == false) {
            CheckIfDiscovered();
        }
    }

    private void CheckIfDiscovered() {
        if (variableStorage.TryGetValue(discoveryName, out bool discovered)) {
            if (discovered) {
                SetColor(discovered);
                found = true;
            }
        }
    }

    protected void SetColor(bool enable) {
        // True is default, which is color
        image.material = enable ? defaultMat : grayscaleMat; 
    }
}
