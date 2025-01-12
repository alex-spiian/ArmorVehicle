Shader "Custom/WhiteFlashEffect"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _FlashIntensity ("Flash Intensity", Range(0,1)) = 0
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            float4 _Color;
            float _FlashIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float light = max(0, dot(normalize(i.normal), lightDir));
                
                fixed4 finalColor = _Color;
                finalColor = lerp(finalColor, fixed4(1,1,1,1), _FlashIntensity);
                finalColor.rgb *= light;
                
                return finalColor;
            }
            ENDCG
        }
    }
}