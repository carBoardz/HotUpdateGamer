Shader "Custom/text3_Normal"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NormalTex ("Normal Map", 2D) = "bump" {}
        _BumpScale ("Bump Scale", Range(0,2)) = 1
        _Color ("Color Tint", Color) = (1,1,1,1)
        _Gloss ("Gloss", Range(8,256)) = 20
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
            sampler2D _NormalTex;
            float4 _MainTex_ST;
            float _BumpScale;
            fixed4 _Color;
            float _Gloss;
            fixed4 _Specular;
            float _ScrollSpeedX;
            float _ScrollSpeedY;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                float3 worldTangent : TEXCOORD3;
                float3 worldBinormal : TEXCOORD4;
            };

            v2f vert (appdata_tan v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                o.worldBinormal = cross(o.worldNormal, o.worldTangent) * v.tangent.w;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 滚动 UV
                float2 uv_scrolled = i.uv + float2(_ScrollSpeedX, _ScrollSpeedY) * _Time.y;

                // 采样法线贴图并解码（切线空间）
                fixed4 packedNormal = tex2D(_NormalTex, uv_scrolled);
                float3 tangentNormal = UnpackNormal(packedNormal);
                tangentNormal.xy *= _BumpScale;
                tangentNormal.z = sqrt(1 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));

                // 构建 TBN 矩阵，将法线从切线空间转到世界空间
                float3 N = normalize(i.worldNormal);
                float3 T = normalize(i.worldTangent);
                float3 B = normalize(i.worldBinormal);
                float3x3 TBN = float3x3(T, B, N);
                float3 worldNormal = normalize(mul(tangentNormal, TBN));

                // 光照计算（同前）
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfDir = normalize(lightDir + viewDir);

                fixed4 albedo = tex2D(_MainTex, uv_scrolled) * _Color;

                float ndotl = max(0, dot(worldNormal, lightDir));
                fixed3 diffuse = albedo.rgb * ndotl * _LightColor0.rgb;

                float ndoth = max(0, dot(worldNormal, halfDir));
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