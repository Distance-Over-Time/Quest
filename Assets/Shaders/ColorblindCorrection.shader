// Shader "Hidden/ColorblindCorrection"
// {
//     HLSLINCLUDE
//     #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

//     TEXTURE2D(_MainTex);
//     SAMPLER(sampler_MainTex);
//     float _Mode; // 0 = Normal, 1 = Protanopia, 2 = Deuteranopia, 3 = Tritanopia

//     // Colorblind simulation matrices
//     static const float3x3 PROTANOPIA = float3x3(
//         0.567, 0.433, 0.000,
//         0.558, 0.442, 0.000,
//         0.000, 0.242, 0.758
//     );

//     static const float3x3 DEUTERANOPIA = float3x3(
//         0.625, 0.375, 0.000,
//         0.700, 0.300, 0.000,
//         0.000, 0.300, 0.700
//     );

//     static const float3x3 TRITANOPIA = float3x3(
//         0.950, 0.050, 0.000,
//         0.000, 0.433, 0.567,
//         0.000, 0.475, 0.525
//     );

//     struct Attributes
//     {
//         float4 positionHCS : POSITION;
//         float2 uv : TEXCOORD0;
//     };

//     struct Varyings
//     {
//         float2 uv : TEXCOORD0;
//         float4 positionHCS : SV_POSITION;
//     };

//     Varyings Vert(Attributes v)
//     {
//         Varyings o;
//         o.positionHCS = TransformObjectToHClip(v.positionHCS.xyz);
//         o.uv = v.uv;
//         return o;
//     }

//     half4 Frag(Varyings i) : SV_Target
//     {
//         half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

//         if (_Mode == 1)
//         {
//             col.rgb = mul(PROTANOPIA, col.rgb);
//         }
//         else if (_Mode == 2)
//         {
//             col.rgb = mul(DEUTERANOPIA, col.rgb);
//         }
//         else if (_Mode == 3)
//         {
//             col.rgb = mul(TRITANOPIA, col.rgb);
//         }

//         return col;
//     }

//     ENDHLSL

//     SubShader
//     {
//         Tags { "RenderType"="Opaque" }
//         Pass
//         {
//             Name "ColorblindCorrection"
//             HLSLPROGRAM
//             #pragma vertex Vert
//             #pragma fragment Frag
//             ENDHLSL
//         }
//     }
// }

Shader "Hidden/ColorblindCorrection"
{
    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

    TEXTURE2D(_MainTex);
    SAMPLER(sampler_MainTex);
    half _Mode; // 0 = Normal, 1 = Protanopia, 2 = Deuteranopia, 3 = Tritanopia

    static const half3x3 COLORBLIND_MATRICES[3] = {
        half3x3(
            0.567, 0.433, 0.000,
            0.558, 0.442, 0.000,
            0.000, 0.242, 0.758
        ),
        half3x3(
            0.625, 0.375, 0.000,
            0.700, 0.300, 0.000,
            0.000, 0.300, 0.700
        ),
        half3x3(
            0.950, 0.050, 0.000,
            0.000, 0.433, 0.567,
            0.000, 0.475, 0.525
        )
    };

    struct Attributes
    {
        float4 positionHCS : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct Varyings
    {
        float2 uv : TEXCOORD0;
        float4 positionHCS : SV_POSITION;
    };

    Varyings Vert(Attributes v)
    {
        Varyings o;
        o.positionHCS = TransformObjectToHClip(v.positionHCS.xyz);
        o.uv = v.uv;
        return o;
    }

    half4 Frag(Varyings i) : SV_Target
    {
        half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

        if (_Mode == 0)
            return col;

        int modeIndex = clamp(int(_Mode) - 1, 0, 2);
        col.rgb = mul(COLORBLIND_MATRICES[modeIndex], col.rgb);

        return col;
    }

    ENDHLSL

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Pass
        {
            Name "ColorblindCorrection"
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            ENDHLSL
        }
    }
}
