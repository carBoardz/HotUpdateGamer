Shader "Custom/text3"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color Tint", Color) = (1,1,1,1)
        _Gloss ("Gloss", Range(8, 256)) = 20
        _Specular ("Specular", Color) = (1,1,1,1)
        _ScrollSpeedX ("Scroll Speed X", Float) = 0
        _ScrollSpeedY ("Scroll Speed Y", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode"="ForwardBase" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _Gloss;
            fixed4 _Specular;
            float _ScrollSpeedX;
            float _ScrollSpeedY;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // 基础 UV 变换（支持 Tiling/Offset）
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 1. 添加 UV 滚动动画
                float2 uv_scrolled = i.uv + float2(_ScrollSpeedX, _ScrollSpeedY) * _Time.y;
                fixed4 albedo = tex2D(_MainTex, uv_scrolled) * _Color;

                // 2. Blinn-Phong 光照
                float3 normal = normalize(i.worldNormal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfDir = normalize(lightDir + viewDir);

                // 漫反射
                float ndotl = max(0, dot(normal, lightDir));
                fixed3 diffuse = albedo.rgb * ndotl * _LightColor0.rgb;

                // 高光
                float ndoth = max(0, dot(normal, halfDir));
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(ndoth, _Gloss);

                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb * albedo.rgb;

                fixed3 finalColor = ambient + diffuse + specular;
                return fixed4(finalColor, 1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}