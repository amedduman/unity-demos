// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/springy"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Spring ("Position Offset", Vector) = (0, 0, 0, 0)
        _RestLength ("Rest Length", Float) = 1
        _SpringConstant ("Spring Constant", Float) = 1
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _Spring;
            float _RestLength;
            float _SpringConstant;

            v2f vert (appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                float3 center = mul(unity_ObjectToWorld, _WorldSpaceCameraPos).xyz;

                float2 springForce = o.vertex - center;
                const float mag = length(springForce.xy);
                const float k = min(_RestLength, mag - _RestLength);
                float2 springFnorm = normalize(springForce);
                springForce = springFnorm * (-1 * k * _SpringConstant);
                o.vertex.x += springForce.x;
                o.vertex.y += springForce.y;

                // float3 center = mul(unity_ObjectToWorld, _WorldSpaceCameraPos).xyz;
                // if (o.vertex.x > center.x && _Spring.x > 0)
                // {
                //     o.vertex.x += _Spring.x;
                // }
                // if (o.vertex.x < center.x && _Spring.x < 0)
                // {
                //     o.vertex.x += _Spring.x;
                // }
                // if (o.vertex.y > center.y && _Spring.y > 0)
                // {
                //     o.vertex.y += _Spring.y;
                // }
                // if (o.vertex.y < center.y && _Spring.y < 0)
                // {
                //     o.vertex.y += _Spring.y;
                // }

                o.vertex.x += _Spring.x;
                // o.vertex.y += _Spring.y;
                
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
