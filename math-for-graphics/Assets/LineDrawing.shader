Shader "Unlit/LineDrawing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // define a vector2 property for point and point b
        [ShowAsVector2] _PointA("Point a", Vector) = (0, 0, 0, 0)
        [ShowAsVector2] _PointB("Point b", Vector) = (0, 0, 0, 0)
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
            float2 _PointA;
            float2 _PointB;
            
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

            bool IsApproximatelyEqual(float a, float b, float epsilon = 0.001f)
            {
                return abs(a - b) < epsilon;
            }

            v2f vert (appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = 0;
                float x = i.uv.x;
                float y = i.uv.y;
                
                if(IsApproximatelyEqual(_PointA.y, y, .01f) && IsApproximatelyEqual(_PointA.x, x, .01f))
                    col.x = 1;
                
                if(IsApproximatelyEqual(_PointB.y, y, .01f) && IsApproximatelyEqual(_PointB.x, x, .01f))
                    col.x = 1;

                if (IsApproximatelyEqual(_PointA.x - _PointB.x, 0))
                {
                    return 1;
                }
                
                const float m = (_PointA.y - _PointB.y) / (_PointA.x - _PointB.x);
                const float b = _PointB.y - m * _PointB.x;


                if(IsApproximatelyEqual(y, m * x + b))
                    col.yz = 1;

                return col;
            }
            ENDCG
        }
    }
}
