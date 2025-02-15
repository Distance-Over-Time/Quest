using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionSwap : MonoBehaviour
{
    [SerializeField] private GameObject firstMat;

    public void SetCraftingWindowFirstSelected() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(firstMat);
        }
    }


}
