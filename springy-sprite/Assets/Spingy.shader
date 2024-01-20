// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Spingy"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Threshold", Range (-5, 5)) = 0.5
        _Spring ("Position Offset", Vector) = (0, 0, 0, 0)
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
            float _Threshold;
            float2 _Spring;

            v2f vert (appdata v)
            {
                 if (v.vertex.x > _Threshold)
                {
                    // Adjust the x-coordinate as neede
                }
                // Calculate the center position in the object's local space
                
                
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
               
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                float3 center = mul(unity_ObjectToWorld, _WorldSpaceCameraPos).xyz;
                if (v.vertex.x > center.x && _Spring.x > 0)
                {
                    o.vertex.x += _Spring.x;
                    // Adjust the x-coordinate as neede
                }
                if (v.vertex.x < center.x && _Spring.x < 0)
                {
                    o.vertex.x += _Spring.x;
                    // Adjust the x-coordinate as neede
                }
                // v.vertex.y += _Spring.y;
                
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
