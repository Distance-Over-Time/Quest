using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn.Unity;

public class CraftingActions : MonoBehaviour
{
    private VariableStorageBehaviour variableStorage;
    [SerializeField] private int potSize = 4;
    [SerializeField] private string[] pot;
    [SerializeField] private string[] yarnPot;
    [SerializeField] private int potFillCount = 0;
    [SerializeField] private GameObject potRow;
    [SerializeField] private CraftingSolutions solutions;
    private GameObject selectedMat;
    [SerializeField] private CraftingMenu menuStatus;

    void Awake() {
        pot = new string[potSize];
        yarnPot = new string[potSize];

        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        if (variableStorage == null)
        {
            Debug.LogError("No InMemoryVariableStorage found");
            return;
        }
    }

    // Using this to find the currently selected material item in the crafting UI
    void FixedUpdate() {
        if (menuStatus.GetActiveStatus()) {
            selectedMat = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void IncrementFloatVariable(string variableName) {
        if (variableStorage.TryGetValue(variableName, out float floatVariable)) {
            floatVariable += 1;
            variableStorage.SetValue(variableName, floatVariable);
        }
        else {
            Debug.LogWarning(variableName + " does not exist in variable storage -- check Yarn node for declaration");
        }
    }

    public void DecrementFloatVariable(string variableName) {
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

    // for getting the values of the in-game pot, not the pot related to Yarn variable storage
    public string[] GetCurrPot() {
        return pot;
    }

    public void AddToPot() {
        if (potFillCount == potSize) {
            // maybe play a rejected noise here?
            Debug.Log("pot too full");  // TODO: remove
            return;
        }
        for (int i = 0; i < potSize; i++) {
            if (pot[i] == null) {
                // change image
                Sprite selectedSprite = selectedMat.GetComponent<Image>().sprite;
                potRow.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = selectedSprite;

                // The name of the in-game material (i.e. 'carrot')
                pot[i] = selectedMat.GetComponent<MaterialValue>().GetMatName();

                // The name of the GameObject that corresponds to a Yarn variable (i.e. '$matItem0')
                yarnPot[i] = selectedMat.name;

                Debug.Log("added " + selectedMat.name); // TODO: remove

                potFillCount++;
                return;
            }
        }
    }

    public void Craft() {
        if (potFillCount != potSize) {
            // maybe play a rejected noise here?
            Debug.Log("not enough items in pot");
            return;
        }

        // We can just sort instead of generating permutations
        Array.Sort(pot);
        
        foreach (KeyValuePair<string, string[]> kvp in solutions.GetRecipes()) {
            Array.Sort(kvp.Value); // or just sort this in CraftingSolutions.cs
            for (int i = 0; i < kvp.Value.Length; i++) {
                if (EqualityComparer<string>.Default.Equals(pot[i], kvp.Value[i])) {
                    CraftingSuccess(kvp.Key);
                    return;
                }
            }
        }
        Debug.Log("no match");
        CraftingFailure();
    }

    private void CraftingSuccess(string midName) {
        SubtractMaterialsForCrafting();
        IncrementFloatVariable(midName);
        ClearPots();
    }

    private void CraftingFailure() {
        // some kind of failure notifcation
        Debug.Log("crafting failed -- not a valid solution"); // TODO: remove
        ClearPots();
    }

    // This function handles variables stored in Yarn
    private void SubtractMaterialsForCrafting() {
        for (int i = 0; i < potSize; i++) {
            DecrementFloatVariable(yarnPot[i]);
            Debug.Log("Subtracted 1 from " + yarnPot[i] + ", aka " + pot[i]); // TODO: remove
        }
    }

    public void ClearPots() {
        Array.Clear(pot, 0, potSize);
        Array.Clear(yarnPot, 0, potSize);

        // Clear images for pot row
        for (int i = 0; i < potSize; i++) {
            potRow.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
        }
    }
}
