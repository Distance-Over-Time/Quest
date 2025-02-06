using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCollection : YarnStorageConnection
{
    // This name needs to match the '$matItem[N]'
    // ex. $matItem0
    public string itemYarnName;
    public string itemDiscoverBoolName;
    
    private SpriteRenderer itemSprite;
    [SerializeField] private SpriteRenderer playerItemIndicator;

    // Start is called before the first frame update
    void Start() {
        itemSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void CollectItem() {
        CollectItemPopup();
        IncrementFloatVariable(itemYarnName); // Increase quantity in crafting menu
        ChangeDiscoveredBool(itemDiscoverBoolName);
        // Destroy(gameObject);
    }
    
    public void CollectItemPopup() {
        playerItemIndicator.sprite = itemSprite.sprite;
        playerItemIndicator.enabled = true;
        // show item appearing above player head
        // item blinking?
    }
}
