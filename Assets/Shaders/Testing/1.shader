Shader "Custom/NoiseEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseAmount ("Noise Amount", Range(0, 1)) = 0.1
        _NoiseSpeed ("Noise Speed", Float) = 1.0
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Стандартный режим прозрачности
        ZWrite Off
        
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
            };

            sampler2D _MainTex;
            float _NoiseAmount;
            float _NoiseSpeed;
            float4 _Color;

            // Простая функция шума (на основе синуса и времени)
            float simpleNoise(float2 uv, float time)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453 * time);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Генерируем шум
                float noise = simpleNoise(i.uv, _Time.y * _NoiseSpeed);
                noise = noise * 2.0 - 1.0; // Преобразуем в диапазон [-1, 1]
                
                // Применяем шум к цвету
                col.rgb += noise * _NoiseAmount;
                
                col *= _Color;
                return col;
            }
            ENDCG
        }
    }
}