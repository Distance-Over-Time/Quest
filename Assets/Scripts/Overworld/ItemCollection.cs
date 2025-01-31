using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : CraftingActions
{
    // This name needs to match the '$matItem[N]'
    // ex. $matItem0
    public string itemType;

    
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTrigger() {
        CollectItemPopup();
    }

    public void CollectItem() {
        CollectItemPopup();
        IncrementFloatVariable(itemType);
    }
    
    public void CollectItemPopup() {
        // show item appearing above player head
        // item blinking?
    }
}
