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

    void Start() {
        itemSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void CollectItem() {
        SetupPopup();
        IncrementFloatVariable(itemYarnName); // Increase quantity in crafting menu
        ChangeDiscoveredBool(itemDiscoverBoolName);
        Destroy(gameObject);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.waterCollected, this.transform.position);
    }
    
    public void SetupPopup() {
        playerItemIndicator.enabled = true;
        playerItemIndicator.sprite = itemSprite.sprite;
    }
}
