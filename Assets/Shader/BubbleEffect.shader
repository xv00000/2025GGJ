Shader "Custom/BubbleEffect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // 纹理
        _Color("Color", Color) = (1, 1, 1, 0.5) // 泡泡颜色和透明度
        _RefractionStrength("Refraction Strength", Range(0, 1)) = 0.1 // 折射强度
        _SpecularPower("Specular Power", Float) = 32.0 // 高光强度
        _FresnelPower("Fresnel Power", Float) = 2.0 // 菲涅尔效果强度
        _BubbleThickness("Bubble Thickness", Range(0, 1)) = 0.5 // 泡泡厚度
        _BubbleSpeed("Bubble Speed", Float) = 1.0 // 泡泡动态变化速度
        _BreathSpeed("Breath Speed", Float) = 1.0
        _BreathRange("Breath Range", Float) = 0.2
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 200

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha // 透明混合
                ZWrite Off // 禁用深度写入

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"
                #include "Lighting.cginc" // 引入光照计算库

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL; // 法线
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float3 worldNormal : TEXCOORD1; // 世界空间法线
                    float3 worldPos : TEXCOORD2; // 世界空间位置
                    float3 viewDir : TEXCOORD3; // 视线方向
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;
                float _RefractionStrength;
                float _SpecularPower;
                float _FresnelPower;
                float _BubbleThickness;
                float _BubbleSpeed;
                float _BreathSpeed;
                float _BreathRange;

                v2f vert(appdata v)
                {



                    v2f o;

                    // 计算呼吸效果因子
                    float breathFactor = sin(_Time.y * _BreathSpeed) * _BreathRange + 1.0;

                    // 应用呼吸效果到顶点位置（缩放）
                    v.vertex.xyz *= breathFactor;

                   
                    

                    // 将顶点位置转换到裁剪空间
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    // 传递 UV 坐标
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                    // 计算世界空间法线和位置
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                    // 计算视线方向
                    o.viewDir = normalize(_WorldSpaceCameraPos - o.worldPos);

                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 计算折射效果
                    float3 refractedDir = refract(-i.viewDir, normalize(i.worldNormal), _RefractionStrength);
                    float2 refractedUV = i.uv + refractedDir.xy * _BubbleThickness;

                    // 采样折射后的纹理
                    fixed4 refractedColor = tex2D(_MainTex, refractedUV);

                    // 计算高光效果
                    float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                    float3 halfwayDir = normalize(lightDir + i.viewDir);
                    float spec = pow(max(0, dot(normalize(i.worldNormal), halfwayDir)), _SpecularPower);

                    // 计算菲涅尔效果
                    float fresnel = 1.0 - max(0, dot(normalize(i.worldNormal), i.viewDir));
                    fresnel = pow(fresnel, _FresnelPower);

                    // 计算动态泡泡效果
                    float bubbleEffect = sin(_Time.y * _BubbleSpeed) * 0.5 + 0.5;

                    // 最终颜色
                    fixed4 col = refractedColor * _Color;
                    col.rgb += spec * bubbleEffect; // 添加高光和动态效果
                    col.a = _Color.a * fresnel; // 使用菲涅尔效果控制透明度

                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}