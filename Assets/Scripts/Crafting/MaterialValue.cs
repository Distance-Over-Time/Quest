using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Yarn.Unity;

public class MaterialValue : YarnStorageConnection, ISelectHandler
{
    public TMP_Text textDisplay;
    [SerializeField] private string matName;
    [SerializeField] private float origQuantity;
    [SerializeField] private bool origQuantitySet;
    private bool inInventory;

    public string matSoundName;

    void Start() {
        // --- SAI (START)
    }

    void OnEnable() {
        // For actual crafting
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        origQuantitySet = false;
    }

    public void OnSelect(BaseEventData eventData) {}

    void Update() {
        variableStorage.TryGetValue(gameObject.name, out float floatVariable);
        // For updating the quantity of this item
        UpdateQuantityText(floatVariable);
        // Checking to see if the player actually has this item
        // For lighting up key item images in recipe list
        UpdateInInventoryStatus(floatVariable);
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

    public bool IsMatInInventory() {
        return inInventory;
    }
    
    private void UpdateInInventoryStatus(float quantity) {
        if (quantity > 0) {
            inInventory = true;
        }
        else {
            inInventory = false;
        }
    }

    private void PlayIngredientSound() {
        // --- SAI
        switch (matName) {
            case "0":
                // select_water

                break;
            case "1":
                // select_chamomile

                break;
            case "2":
                // select_oats

                break;
            case "3":
                // select_wolfsbane

                break;
            case "4":
                // select_honey

                break;
            case "5":
                // select_beeswax

                break;
            case "6":
                // select_tomato

                break;
            case "7":
                // select_lemon

                break;
            default:
                Debug.Log("Something went wrong in PlayIngredientSound() (MaterialValue.cs) -- check matName");
                break;
        }
        return;
    }
}
