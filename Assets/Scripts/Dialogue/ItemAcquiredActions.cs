using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemAcquiredActions : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject buttonObj;
    [SerializeField] private TMP_Text gameName;
    [SerializeField] private TMP_Text flavorDesc;
    [SerializeField] private Image image;

    void OnEnable() {
        if (gameObject.activeInHierarchy) {
            EventSystem.current.SetSelectedGameObject(buttonObj);
        }
    }

    public void PlayPopupAnim(GameObject keyItem) {
        gameObject.SetActive(true);
        SetUpPopup(keyItem);
        anim.Play("Base Layer.Popup Enter", 0, 0f);
    }

    public void ExitPopup() {
        StartCoroutine(ClosePopupAfterAnimation());
    }

    private IEnumerator ClosePopupAfterAnimation() {
        anim.Play("Base Layer.Popup Exit", 0, 0f);    
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    private void SetUpPopup(GameObject keyItem) {
        KeyItemValue value = keyItem.GetComponent<KeyItemValue>();

        gameName.text = value.GetGameName();
        flavorDesc.text = value.GetFlavorDesc();
        image.sprite = value.GetKeyImage().sprite;
    }
}
