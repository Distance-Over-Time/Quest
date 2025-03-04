using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorblindBlitPass : ScriptableRenderPass
{
    private Material colorblindMaterial;
    private RenderTargetIdentifier source;
    private RTHandle tempTexture;
    public int mode = 0;

    public ColorblindBlitPass(Material material)
    {
        this.colorblindMaterial = material;
        tempTexture = RTHandles.Alloc("_TempColorblindTex", name: "_TempColorblindTex"); // Updated
    }

    public void SetSource(RenderTargetIdentifier source)
    {
        this.source = source;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (colorblindMaterial == null) return;

        CommandBuffer cmd = CommandBufferPool.Get("Colorblind Effect");
        cmd.SetGlobalFloat("_Mode", mode);
        cmd.Blit(source, tempTexture, colorblindMaterial);
        cmd.Blit(tempTexture, source);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
