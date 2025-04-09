Shader "Custom/Bubble"
{
    Properties
    {
        _BubbleColor ("Bubble Color", Color) = (1,1,0,1)
        _BubbleRadius ("Bubble Radius", Range(0,0.5)) = 0.2
        _BubbleHardness ("Bubble Hardness", Range(1,20)) = 8
        _BubbleOffset ("Bubble Offset", Vector) = (0,0,0,0)
        _BubbleCenter ("Bubble Center", Vector) = (0.5,0.5,0,0)
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

            #include "UnityCG.cginc"

            fixed4 _BubbleColor;
            float _BubbleRadius;
            float _BubbleHardness;
            float4 _BubbleOffset;
            float4 _BubbleCenter;  // Nuova proprietà per regolare il centro

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 bg = fixed4(0, 1, 0, 1);

                // Usa _BubbleCenter per definire il centro della bolla
                float2 bubbleCenter = _BubbleCenter.xy ;
                //float2 bubbleCenter = _BubbleCenter.xy + _BubbleOffset.xy;

                float dist = distance(uv, bubbleCenter);
                float t = smoothstep(_BubbleRadius, _BubbleRadius - (1.0 / _BubbleHardness), dist);
                float bubbleMask = 1.0 - t;
                fixed4 col = lerp(bg, _BubbleColor, bubbleMask);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Color"
}