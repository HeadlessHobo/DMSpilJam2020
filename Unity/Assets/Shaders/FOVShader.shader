Shader "Shaders/FOVShader"{
Properties{
    _Color ("Tint", Color) = (0, 0, 0, 1)
    _MainTex ("Texture", 2D) = "white" {}
    _ConeAngle ("ConeAngle", float) = 30
}

SubShader{
    Tags{ 
        "RenderType"="Transparent" 
        "Queue"="Transparent"
    }

    Blend SrcAlpha OneMinusSrcAlpha

        ZWrite off
        Cull off

    Pass{

        CGPROGRAM

#include "UnityCG.cginc"

#pragma vertex vert
#pragma fragment frag

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float _ConeAngle;

        fixed4 _Color;

            struct appdata{
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
            fixed4 color : COLOR;
        };

        struct v2f{
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
            fixed4 color : COLOR;
        };

        v2f vert(appdata v){
            v2f o;
            o.position = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            o.color = v.color;
            float4 normalizedVertex = normalize(v.vertex);
            float angle = degrees(dot(float2(0.5, 0), float2(0, 0.5)));
            if(angle < _ConeAngle){
                o.color = 0;
            }        


            return o;
        }

        fixed4 frag(v2f i) : SV_TARGET{
            fixed4 col = tex2D(_MainTex, i.uv);
            col *= _Color;
            col *= i.color;
            return i.color;
        }

        ENDCG
    }
}
}