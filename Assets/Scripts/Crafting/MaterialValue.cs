using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Yarn.Unity;

public class MaterialValue : MonoBehaviour, ISelectHandler
{
    public TMP_Text textDisplay;
    private VariableStorageBehaviour variableStorage;
    [SerializeField] private string matName;

    void Awake() {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
    }

    public void OnSelect(BaseEventData eventData) {
        // Debug.Log(this.gameObject.name + " was selected");
    }

    void FixedUpdate() {
        variableStorage.TryGetValue(gameObject.name, out float floatVariable);
        UpdateQuantityText(floatVariable);
    }
    
    public string GetMatName() {
        return matName;
    }

    public void UpdateQuantityText(float quantity) {
        textDisplay.text = quantity.ToString();
    }
}
