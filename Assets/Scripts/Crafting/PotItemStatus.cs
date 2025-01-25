using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PotItemStatus : MonoBehaviour
{
    public bool isFilled;
    private Image thisImage;

    void Start()
    {
        isFilled = false;
        thisImage = gameObject.GetComponent<Image>();
    }

    public bool CheckIfFilled() {
        return isFilled;
    }

    public void MakeEmpty() {
        // turn off image
        isFilled = false;
    }

    public void MakeFull() {
        // turn on image
        isFilled = true;
    }
}
