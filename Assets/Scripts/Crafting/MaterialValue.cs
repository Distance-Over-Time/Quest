using UnityEngine;
using TMPro;
using Yarn.Unity;

public class MaterialValue : MonoBehaviour
{
    public TMP_Text textDisplay;
    private VariableStorageBehaviour variableStorage;

    void Awake() {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
    }

    void FixedUpdate() {
        variableStorage.TryGetValue(gameObject.name, out float floatVariable);
        UpdateQuantityText(floatVariable);
    }
    
    public void UpdateQuantityText(float quantity) {
        textDisplay.text = quantity.ToString();
    }
}
