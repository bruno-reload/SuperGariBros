Shader "Unlit/EnvironmentScrolling"{
    Properties{
        _Colour ("Color",color) = (1.0,1.0,0.0,1.0)
        _Texture ("Texture", 2d) = "white" {}
        _Factor ("Normal Factor", Range(1,100)) = 0.0
    }
    SubShader{
        pass{
            CGPROGRAM
            #pragma vertex vertexUpdate
            #pragma fragment fragmentUpdate

            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL;
            };
            struct v2f{
                float4 pos: SV_POSITION;
                float2 uv: TEXCOORD0;
            };

            float4 _Colour;
            sampler2D _Texture;
            float _Factor;

            v2f vertexUpdate(appdata IN){
                v2f OUT;
                IN.vertex.xyz = IN.vertex.xyz * _Factor;
                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;

                return OUT;
            }

            fixed4 fragmentUpdate(v2f IN): SV_TARGET{
                return tex2D(_Texture,IN.uv);
            }
            ENDCG
            Cull off
        }        
    }
}