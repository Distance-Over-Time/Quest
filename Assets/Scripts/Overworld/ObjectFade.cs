using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    public bool doFade = false;
    public float fadeSpeed = 5f;  // Speed of fading
    public float fadeAmount = 0.2f; // Target transparency
    private float[] originalOpacities;
    private List<Material> mats;

    void Start() {
        Renderer rend = GetComponent<Renderer>();
        if (rend == null) {
            Debug.LogError("ObjectFade: No Renderer found on " + gameObject.name);
            return;
        }

        mats = rend.materials.ToList();
        originalOpacities = new float[mats.Count];

        for (int i = 0; i < mats.Count; i++) {
            originalOpacities[i] = mats[i].color.a; 
        }
    }

    void Update()
    {
        if (doFade) {
            Fade();
        }
        else {
            ResetFade();
        }
    }

    private void Fade() {
        foreach (Material mat in mats) {
            Color currentColor = mat.color;
            float newAlpha = Mathf.Lerp(currentColor.a, fadeAmount, 1 - Mathf.Exp(-fadeSpeed * Time.deltaTime));

            mat.SetColor("_Color", new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha));
        }
    }

    private void ResetFade() {
        for (int i = 0; i < mats.Count; i++) {
            Color currentColor = mats[i].color;
            float newAlpha = Mathf.Lerp(currentColor.a, originalOpacities[i], 1 - Mathf.Exp(-fadeSpeed * Time.deltaTime));

            mats[i].SetColor("_Color", new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha));
        }
    }
}
