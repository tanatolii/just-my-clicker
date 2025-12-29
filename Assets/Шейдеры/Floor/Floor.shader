Shader "Unlit/Floor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tilling ("Tilling", Float) = 1.0
        _Tilling2 ("Tilling", Float) = 1.0
        _Dissolve ("Dissolve", Range(0, 1)) = 0.5
        _MaskTex ("MaskTex", 2D) = "white" {}
    }
    SubShader
    {
        Tags { 
        "Queue"="Transparent" 
        "RenderType"="Transparent"
        "IgnoreProjector"="True"
        "PreviewType"="Plane"
    }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Tilling;
            float _Tilling2;
            float _Dissolve;
            sampler2D _MaskTex;

            struct appdata
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
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mask = tex2D(_MaskTex, i.uv);
                mask.rgb = 1 - mask.rgb;
                clip(mask - _Dissolve);
                i.uv.x *= _Tilling2;
                i.uv = frac(i.uv * _Tilling);
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = lerp(col, mask, 0.5);
                return col;
            }
            ENDCG
        }
    }
}
