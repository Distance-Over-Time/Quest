using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class CustomDialogueCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    [SerializeField] private GameObject popup;
    [SerializeField] private Image[] itemImages;
    [SerializeField] private Animator anim;

    public void Awake() {
        dialogueRunner.AddCommandHandler<string>(
            "show_popup", ShowDialogueItemPopup 
        );
        dialogueRunner.AddCommandHandler<string>(
            "hide_popup", HideDialogueItemPopup 
        );
    }

    private void ShowDialogueItemPopup(string name) {
        if (FindMatImage(name) != null) {
            popup.SetActive(true);
            popup.GetComponent<Image>().sprite = FindMatImage(name).sprite;
            Debug.Log("Showing dialogue popup");
        }
        else {
            Debug.Log("Unable to show the item popup for dialogue -- let Ryan know there's a bug");
        }
    }

    private void HideDialogueItemPopup(string name = "") {
        StartCoroutine(ClosePopupAfterAnimation());
        Debug.Log("Hide dialogue popup");
    }

    private IEnumerator ClosePopupAfterAnimation() {
        anim.Play("Base Layer.Popup Exit", 0, 0f);    
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        popup.SetActive(false);
    }

    private Image FindMatImage(string name) {
        switch (name) {
            // Materials
            case "water":
                return itemImages[0];
            case "chamomile":
                return itemImages[1];
            case "oats":
                return itemImages[2];
            case "wolfsbane":
                return itemImages[3];
            case "honey":
                return itemImages[4];
            case "beeswax":
                return itemImages[5];
            case "tomato":
                return itemImages[6];
            case "lemon":
                return itemImages[7];
            // Recipes
            case "tea":
                return itemImages[8];
            case "grog":
                return itemImages[9];
            case "porridge":
                return itemImages[10];
            case "salve":
                return itemImages[11];
            case "potion":
                return itemImages[12];
            default:
                Debug.Log("An unrecognized name has been entered for custom Yarn function, so this isn't going to work -- better ask Ryan what happened");
                return null;
        }
    }
}
