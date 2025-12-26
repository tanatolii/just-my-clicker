Shader "Custom/InvertColor" {
    Properties {
        _TintColor ("Tint Color", Color) = (1,1,1,1)
    }
    SubShader {
        Tags { "Queue"="Overlay+100" "IgnoreProjector"="True" } // Рендерится поверх всего
        Pass {
            Blend OneMinusDstColor OneMinusSrcColor // Ключевая строка для инверсии
            ZWrite Off
            ZTest Always // Игнорирует буфер глубины
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            float4 _TintColor;
            
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert (appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag (v2f i) : SV_Target {
                return _TintColor; // Для полной инверсии используйте белый цвет (1,1,1,1)
            }
            ENDCG
        }
    }
}