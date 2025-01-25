using UnityEngine;
using Yarn.Unity;

public class CraftingActions : MonoBehaviour
{
    private VariableStorageBehaviour variableStorage;
    // private GameObject matToUpdate;

    void Awake() {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        if (variableStorage == null)
        {
            Debug.LogError("No InMemoryVariableStorage found");
            return;
        }
    }

    public void IncrementFloatVariable(string variableName) {
        if (variableStorage.TryGetValue(variableName, out float floatVariable)) {
            floatVariable += 1;
            variableStorage.SetValue(variableName, floatVariable);
            // UpdateMatQuantity(variableName, floatVariable);
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
        }
    }

    public void DecrementFloatVariable(string variableName) {
        if (variableStorage.TryGetValue(variableName, out float floatVariable)) {
            floatVariable -= 1;
            variableStorage.SetValue(variableName, floatVariable);
            if (floatVariable < 0) {
                variableStorage.SetValue(variableName, floatVariable = 0);
            }
            // UpdateMatQuantity(variableName, floatVariable);
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
        }
    }

    public void GainKeyItem(string variableName) {
        if (variableStorage.TryGetValue(variableName, out bool boolVariable)) {
            variableStorage.SetValue(variableName, boolVariable = true);
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
        }
    }

    // public void UpdateMatQuantity(string variableName, float quantity) {
    //     matToUpdate = GameObject.Find(variableName);
    //     matToUpdate.GetComponent<MaterialValue>().UpdateQuantityText(quantity);
    // }
    
}
