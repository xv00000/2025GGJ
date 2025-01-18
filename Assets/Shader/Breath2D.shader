Shader "Custom/Breath2D"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1) // 添加颜色属性
        _BreathSpeed("Breath Speed", Float) = 1.0
        _BreathRange("Breath Range", Float) = 0.2
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 200

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha // 启用透明度混合
                ZWrite Off // 关闭深度写入，确保2D对象正确渲染

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

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

                sampler2D _MainTex;
                float4 _MainTex_ST; // 纹理的缩放和偏移
                float4 _Color; // 颜色属性
                float _BreathSpeed;
                float _BreathRange;

                v2f vert(appdata v)
                {
                    v2f o;

                    // 将顶点位置转换到裁剪空间
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    // 传递 UV 坐标
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 计算呼吸效果因子
                    float breathFactor = (sin(_Time.y * _BreathSpeed) * _BreathRange) + 1.0;

                // 采样纹理
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // 将纹理颜色与自定义颜色结合，并应用呼吸效果到透明度
                fixed4 col = texColor * _Color;
                col.a *= breathFactor; // 呼吸效果影响透明度

                return col;
            }
            ENDCG
        }
        }
            FallBack "Diffuse" // 回退 Shader
}