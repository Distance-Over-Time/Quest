using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnStorageConnection : MonoBehaviour
{
    protected VariableStorageBehaviour variableStorage;

    void Awake() {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        
        if (variableStorage == null) {
            Debug.LogError("No InMemoryVariableStorage found");
            return;
        }

        DontDestroyOnLoad(variableStorage);
    }

    public VariableStorageBehaviour GetYarnVarStorage() {
        return variableStorage;
    }

    public float GetFloatVariable(string variableName) {
        Debug.Log(variableStorage + " (from GetFloatVariable)");
        if (variableStorage.TryGetValue(variableName, out float floatVariable)) {
            return floatVariable;
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
            return -1;
        }
    }

    public void IncrementFloatVariable(string variableName) {
        Debug.Log(variableStorage + " (from Increment)");
        if (variableStorage.TryGetValue(variableName, out float floatVariable)) {
            floatVariable += 1;
            variableStorage.SetValue(variableName, floatVariable);
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
        }
    }

    public void DecrementFloatVariable(string variableName) {
        Debug.Log(variableStorage + " (from Decrement)");
        if (variableStorage.TryGetValue(variableName, out float floatVariable)) {
            if (floatVariable < 1) {
                // Not enough material
                // Play error noise here?
                return;
            }
            floatVariable -= 1;
            variableStorage.SetValue(variableName, floatVariable);
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
        }
    }

    public void GainFinalItem(string variableName) {
        if (variableStorage.TryGetValue(variableName, out bool boolVariable)) {
            variableStorage.SetValue(variableName, boolVariable = true);
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
        }
    }
}
