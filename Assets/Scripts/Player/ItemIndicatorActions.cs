using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndicatorActions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerItemIndicator;
    [SerializeField] private Animator anim;

    void Start() {
        playerItemIndicator.enabled = false;
    }
  
    public void CollectItemPopup() {
        // enabling and disabling of SpriteRenderer is handled by the Animation clip
        anim.Play("Base Layer.Popup", 0, 0f);
    }
}
