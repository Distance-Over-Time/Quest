using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn.Unity;

public class CraftingActions : YarnStorageConnection
{
    [SerializeField] private int potSize = 4;
    [SerializeField] private string[] pot;
    [SerializeField] private int potFillCount = 0;
    [SerializeField] private GameObject potRow;
    [SerializeField] private CraftingSolutions solutions;
    private GameObject selectedMat;
    [SerializeField] private CraftingMenu menuStatus;
    [SerializeField] private GameObject[] materialObjs;

    void Start() {
        pot = new string[potSize];
        materialObjs = new GameObject[potSize];
    }

    // Using this to find the currently selected material item in the crafting UI
    void FixedUpdate() {
        if (menuStatus.GetActiveStatus()) {
            selectedMat = EventSystem.current.currentSelectedGameObject;
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
        if (GetFloatVariable(selectedMat.name) <= 0) {
            // maybe play a rejected noise here?
            Debug.Log("can't add -- not enough of " + selectedMat.name);
            return;
        }

        for (int i = 0; i < potSize; i++) {
            if (pot[i] == null) {
                // change image
                Sprite selectedSprite = selectedMat.GetComponent<Image>().sprite;
                potRow.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = selectedSprite;

                // The name of the in-game material (i.e. 'carrot')
                pot[i] = selectedMat.GetComponent<MaterialValue>().GetMatName();

                materialObjs[i] = selectedMat;

                Debug.Log("added " + selectedMat.name); // TODO: remove

                // Hold onto quanity in current state incase of failure/cancel, run only as necessary
                MaterialValue matValue = GetMaterialValue(selectedMat);
                Debug.Log(matValue);
                if (!matValue.GetQuantitySetStatus()) {
                    matValue.SetOrigQuantity();
                    matValue.SetOrigStatus(true);
                }

                potFillCount++;
                DecrementFloatVariable(materialObjs[i].name);
                return;
            }
        }
    }

    private MaterialValue GetMaterialValue(GameObject obj) {
        return obj.GetComponent<MaterialValue>();
    }

    public void Craft() {
        if (potFillCount != potSize) {
            // maybe play a rejected noise here?
            Debug.Log("not enough items in pot");
            return;
        }

        // We can just sort instead of generating permutations
        string[] sortedPot = (string[])pot.Clone();
        Array.Sort(sortedPot);
        
        foreach (KeyValuePair<string, string[]> kvp in solutions.GetRecipes()) {
            string[] sortedKvp = (string[])kvp.Value.Clone();
            Array.Sort(sortedKvp); // or just sort this in CraftingSolutions.cs
            if (sortedKvp.SequenceEqual(sortedPot)) {
                CraftingSuccess(kvp.Key);
                return;
            }
        }
        Debug.Log("no match");
        CraftingFailure();
    }

    // private void ResetQuantityToPrevState() {
    //     for (int i = 0; i < potSize; i++) {
    //         MaterialValue matValue = GetMaterialValue(yarnPot[i]);
    //         variableStorage.SetValue(yarnPot[i], yarnPot[i].GetOrigQuantity());
    //     }
    // }

    private void CraftingSuccess(string midName) {
        // SubtractMaterialsForCrafting();
        IncrementFloatVariable(midName);
        ClearPots();
    }

    private void CraftingFailure() {
        // some kind of failure notifcation
        Debug.Log("crafting failed -- not a valid solution"); // TODO: remove
        // ResetQuantityToPrevState();
        ClearPots(true);
    }

    // This function handles variables stored in Yarn
    // private void SubtractMaterialsForCrafting() {
    //     for (int i = 0; i < potSize; i++) {
    //         DecrementFloatVariable(yarnPot[i]);
    //         Debug.Log("Subtracted 1 from " + yarnPot[i] + ", aka " + pot[i]); // TODO: remove
    //     }
    // }

    public void ClearPots(bool failure = false) {
        for (int i = 0; i < potSize; i++) {
            // Clear images for pot row
            GameObject currPotItem = potRow.transform.GetChild(i).gameObject; 
            currPotItem.GetComponent<Image>().sprite = null;

            // Reset all pot items to their previous state
            MaterialValue matValue = GetMaterialValue(materialObjs[i]);
            matValue.SetOrigStatus(false);
            if (failure) {
                variableStorage.SetValue(materialObjs[i].name, matValue.GetOrigQuantity());
            }
        }

        Array.Clear(pot, 0, potSize);
        Array.Clear(materialObjs, 0, potSize);
        potFillCount = 0;
    }
}
