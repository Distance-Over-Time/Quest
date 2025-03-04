using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorblindAccessibility : MonoBehaviour
{
    [SerializeField] private Material colorblindMaterial;
    private ColorblindBlitPass colorblindPass;
    private ScriptableRenderer scriptableRenderer;

    void Start()
    {
        // Load the shader dynamically
        Shader shader = Shader.Find("Hidden/ColorblindCorrection");
        if (shader != null)
        {
            Debug.Log("Shader Loaded: " + shader.name);
            colorblindMaterial = new Material(shader);
            Debug.Log("Colorblind material created!");
        }
        else
        {
            Debug.LogError("Colorblind shader not found!");
            return;
        }

        // Create and register the Blit Pass
        colorblindPass = new ColorblindBlitPass(colorblindMaterial);

        var urpAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        var renderer = urpAsset?.scriptableRenderer as UniversalRenderer;

        if (renderer != null)
        {
            Debug.Log("Found URP Renderer. Adding Colorblind Effect Pass...");
            colorblindPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
            renderer.EnqueuePass(colorblindPass);
        }
        else
        {
            Debug.LogError("No valid URP Renderer found.");
        }
    }

    public void SetColorblindMode(int mode)
    {
        if (colorblindPass != null)
        {
            colorblindPass.mode = mode;
            Debug.Log("Colorblind mode set to: " + mode);
        }
    }
}