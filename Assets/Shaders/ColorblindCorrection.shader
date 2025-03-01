Shader "Hidden/ColorblindCorrection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mode ("Colorblind Mode", Float) = 0 // 0 = Normal, 1 = Protanopia, 2 = Deuteranopia, 3 = Tritanopia
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Mode; // Colorblind mode

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Colorblind Simulation Matrices
            static const float3x3 PROTANOPIA = float3x3(
                0.567, 0.433, 0.000,
                0.558, 0.442, 0.000,
                0.000, 0.242, 0.758
            );

            static const float3x3 DEUTERANOPIA = float3x3(
                0.625, 0.375, 0.000,
                0.700, 0.300, 0.000,
                0.000, 0.300, 0.700
            );

            static const float3x3 TRITANOPIA = float3x3(
                0.950, 0.050, 0.000,
                0.000, 0.433, 0.567,
                0.000, 0.475, 0.525
            );

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                if (_Mode == 1)
                {
                    col.rgb = mul(PROTANOPIA, col.rgb);
                }
                else if (_Mode == 2)
                {
                    col.rgb = mul(DEUTERANOPIA, col.rgb);
                }
                else if (_Mode == 3)
                {
                    col.rgb = mul(TRITANOPIA, col.rgb);
                }

                return col;
            }
            ENDHLSL
        }
    }
}