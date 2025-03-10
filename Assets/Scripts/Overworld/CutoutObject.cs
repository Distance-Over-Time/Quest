using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private LayerMask wallMask;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = GetComponent<Camera>();
    }

    private void Update() {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i) {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int n = 0; n < materials.Length; ++n) {
                materials[n].SetVector("_CutoutPos", cutoutPos);
                materials[n].SetFloat("_CutoutSize", 0.1f);
                materials[n].SetFloat("_FalloffSize", 0.05f);
            }
        }
    }
}
