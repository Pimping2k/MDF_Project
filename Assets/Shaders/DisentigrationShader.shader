Shader "Unlit/DisentigrationShader"
{
     Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _SplitLine ("Split Line", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _SplitLine;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                if (i.uv.x < _SplitLine)
                    return tex2D(_MainTex, i.uv);
                else
                    return float4(0, 0, 0, 0); // Прозрачная часть
            }
            ENDCG
        }
    }
}
