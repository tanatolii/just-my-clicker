Shader "Custom/2DDepthShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DepthColor ("Depth Color", Color) = (0.5, 0.5, 0.5, 1)
        _DepthFactor ("Depth Factor", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float depth : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _DepthColor;
            float _DepthFactor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.depth = v.vertex.z * _DepthFactor; // Глубина из Z-координаты
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = lerp(col.rgb, _DepthColor.rgb, i.depth); // Затемнение по глубине
                return col;
            }
            ENDCG
        }
    }
}