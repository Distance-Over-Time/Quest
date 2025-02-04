Shader "WaterColor/Base"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DiffuseColor("Diffuse Color", Color) = (1, 1, 1, 1)
        _Outline("Outline", Range(0, 1)) = 0.3
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            fixed4 _DiffuseColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct a2v {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;  // Fix for Metal: Pass world normal
                UNITY_FOG_COORDS(2)
            };

            v2f vert (a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);  // Compute world normal in vertex shader

                UNITY_TRANSFER_FOG(o, o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // ✅ Metal-compatible ambient lighting
                fixed3 ambient = ShadeSH9(float4(normalize(i.worldNormal), 1));  

                fixed3 texColor = lerp(tex2D(_MainTex, uv).rgb, ambient, 0.5);
                texColor = lerp(texColor, fixed3(1,1,1), 1 - tex2D(_MainTex, uv).a);

                float intensity = dot(texColor, float3(1, 1, 1));
                fixed3 diffuse = _DiffuseColor.rgb * smoothstep(0.35, 0.4, intensity) * texColor;

                UNITY_APPLY_FOG(i.fogCoord, diffuse);
                return fixed4(diffuse, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}