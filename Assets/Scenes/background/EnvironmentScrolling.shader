Shader "Unlit/EnvironmentScrolling"{
        Properties{
                _TextureIn ("Texture Input", 2d) = "white" {}
                _TextureBuff ("Texture Buff", 2d) = "white" {}
                _SplitScreem ("Split Screem", Range(0,2)) = 0.0
                _Ligth ("Ligth Factor", Range(0,1)) = 0
                _Speed ("Speed Factory", Float) = 0
                _Sinterpolation ("INterpolation Faactory", Range(0,1)) = 0 
                
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
                        };
                        struct v2f{
                                float4 pos: SV_POSITION;
                                float2 uv: TEXCOORD0;
                        };
                        
                        float _Sinterpolation;
                        sampler2D _TextureIn;
                        sampler2D _TextureBuff;
                        float _SplitScreem;
                        float _Ligth;
                        float _Speed;
                        float countSplit;
                        float cont = 0;

                        v2f vertexUpdate(appdata IN){
                                v2f OUT;
                                OUT.pos = UnityObjectToClipPos(IN.vertex);
                                OUT.uv = IN.uv;
                                return OUT;
                        }

                        fixed4 fragmentUpdate(v2f IN): SV_TARGET{
                                float4 texBuff;
                                float4 texIn;
                                float4 text;
                                float2 inPos;
                                float2 buffPos;

                                inPos = float2(IN.uv.x + _Time.y * _Speed,IN.uv.y);
                                buffPos = float2(IN.uv.x + _Time.y * _Speed,IN.uv.y);

                                IN.uv.x +=  1 - _SplitScreem;

                                if(IN.uv.x  <= 1){
                                        texIn = tex2D(_TextureIn,inPos);
                                }
                                else{
                                        texBuff = tex2D(_TextureBuff,buffPos);
                                }

                                text = lerp(texIn,float4(0,0,0,0),pow(IN.uv.x,2)) - 
                                lerp(texBuff,float4(0,0,0,0),pow(IN.uv.x,2));

                                text.xyz *= _Ligth;

                                return text;
                        }
                        ENDCG
                        Cull off
                } 

        }
}