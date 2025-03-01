using UnityEngine;

[ExecuteInEditMode]
public class ColorblindEffect : MonoBehaviour
{
    public Material colorblindMaterial;
    [Range(0, 3)] public int mode = 0; // 0 = Normal, 1 = Protanopia, 2 = Deuteranopia, 3 = Tritanopia

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (colorblindMaterial != null)
        {
            colorblindMaterial.SetFloat("_Mode", mode);
            Graphics.Blit(source, destination, colorblindMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
