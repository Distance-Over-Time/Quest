using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAcquiredActions : MonoBehaviour
{
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(false);
        // anim.Play("Base Layer.Popup", 0, 0f);
    }

    // void OnEnable() {
    //     anim.Play("Base Layer.Popup", 0, 0f);
    // }

    public void PlayPopupAnim() {
        gameObject.SetActive(true);
        Debug.Log(anim);
        anim.Play("Base Layer.Popup", 0, 0f);
    }
}
