using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class NavigateOptions : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;

    void Start() {
        eventSystem.SetSelectedGameObject(GameObject.Find("Option View"));
        Debug.Log("selected");
    }
}
