using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            onTriggerEnter.Invoke();
        } 
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            onTriggerExit.Invoke();
        }
    }
}
