using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Yarn.Unity;

public class MaterialValue : YarnStorageConnection, ISelectHandler
{
    public TMP_Text textDisplay;
    // private YarnStorageConnection yarnConnection;
    [SerializeField] private string matName;
    [SerializeField] private float origQuantity;
    [SerializeField] private bool origQuantitySet;

    void OnEnable() {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        origQuantitySet = false;
    }

    public void OnSelect(BaseEventData eventData) {}

    void Update() {
        variableStorage.TryGetValue(gameObject.name, out float floatVariable);
        UpdateQuantityText(floatVariable);
    }
    
    public string GetMatName() {
        return matName;
    }

    public void UpdateQuantityText(float quantity) {
        textDisplay.text = quantity.ToString();
    }

    public float GetOrigQuantity() {
        return origQuantity;
    }

    public void SetOrigQuantity() {
        origQuantity = GetFloatVariable(gameObject.name);
    }

    public void SetOrigStatus(bool status) {
        origQuantitySet = status;
    }

    public bool GetQuantitySetStatus() {
        return origQuantitySet;
    }
}
