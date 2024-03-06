Shader "Unlit/LineDrawing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // define a float property for the slope
        _Slope ("Slope", Range(0, 1)) = 0.0
        // define a float property for the offset
        _Offset ("Offset", Range(0, 1)) = 0.0
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Slope;
            float _Offset;

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
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            bool IsApproximatelyEqual(float a, float b)
            {
                float epsilon = 0.001f;
                return abs(a - b) < epsilon;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = 0;
                float x = i.uv.x;
                float y = i.uv.y;
                if(IsApproximatelyEqual(y, _Slope * x + _Offset))
                {
                    col = 1;
                }
                return col;
            }
            ENDCG
        }
    }
}
