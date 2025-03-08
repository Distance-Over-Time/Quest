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
    [SerializeField] private MenuActivation menuStatus;
    [SerializeField] private GameObject[] materialObjs;
    [SerializeField] private GameObject craftedPopup;

    private GameObject selectedMat;
    private string emptySlot;

    void Start() {
        emptySlot = solutions.GetGameNullValue();
        
        pot = new string[potSize];
        Array.Fill(pot, emptySlot);
        
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
            ClearCraftingPot();
            return;
        }

        for (int i = 0; i < potSize; i++) {
            if (pot[i] == emptySlot) {
                // change image
                Sprite selectedSprite = selectedMat.GetComponent<Image>().sprite;
                potRow.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = selectedSprite;

                // The name of the in-game material (i.e. 'carrot')
                pot[i] = selectedMat.GetComponent<MaterialValue>().GetMatName();

                materialObjs[i] = selectedMat;

                // Hold onto quanity in current state incase of failure/cancel, run only as necessary
                MaterialValue matValue = GetMaterialValue(selectedMat);
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
        // --- SAI
        

        // We can just sort instead of generating permutations
        string[] sortedPot = (string[])pot.Clone();
        Array.Sort(sortedPot);
        
        foreach (KeyValuePair<string, string[]> kvp in solutions.GetRecipes()) {
            string[] sortedKvp = (string[])kvp.Value.Clone();
            Array.Sort(sortedKvp);
            if (sortedKvp.SequenceEqual(sortedPot)) {
                CraftingSuccess(kvp.Key);
                return;
            }
        }
        Debug.Log("no match");
        CraftingFailure();
    }

    private void CraftingSuccess(string recipeName) {
        GameObject craftedRecipe = GameObject.Find(recipeName);
        craftedRecipe.transform.GetChild(1).GetComponent<KeyItemReaction>().SetCraftedStatus(true);

        craftedPopup.GetComponent<ItemAcquiredActions>().PlayPopupAnim(craftedRecipe);

        ClearAllPots();
    }

    private void CraftingFailure() {
        // TODO: some kind of failure notifcation

        // --- SAI

        ClearAllPots(true);
    }

    public void ClearAllPots(bool failure = false) {
        for (int i = 0; i < potSize; i++) {
            // End if the attempt did not utilize the full pot
            if (!materialObjs[i]) {
                ClearCraftingPot();
                return;
            }

            // Clear image for pot slot
            GameObject currPotItem = potRow.transform.GetChild(i).gameObject; 
            currPotItem.GetComponent<Image>().sprite = null;

            // Reset all pot items to their previous state
            MaterialValue matValue = GetMaterialValue(materialObjs[i]);
            matValue.SetOrigStatus(false);
            if (failure) {
                variableStorage.SetValue(materialObjs[i].name, matValue.GetOrigQuantity());
            }
        }

        ClearCraftingPot();
        Array.Clear(materialObjs, 0, potSize);
        potFillCount = 0;
    }

    public void ClearCraftingPot() {
        pot = new string[potSize];
        Array.Fill(pot, emptySlot);
    }
}
