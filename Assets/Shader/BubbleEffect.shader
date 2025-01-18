Shader "Custom/BubbleEffect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // ����
        _Color("Color", Color) = (1, 1, 1, 0.5) // ������ɫ��͸����
        _RefractionStrength("Refraction Strength", Range(0, 1)) = 0.1 // ����ǿ��
        _SpecularPower("Specular Power", Float) = 32.0 // �߹�ǿ��
        _FresnelPower("Fresnel Power", Float) = 2.0 // ������Ч��ǿ��
        _BubbleThickness("Bubble Thickness", Range(0, 1)) = 0.5 // ���ݺ��
        _BubbleSpeed("Bubble Speed", Float) = 1.0 // ���ݶ�̬�仯�ٶ�
        _BreathSpeed("Breath Speed", Float) = 1.0
        _BreathRange("Breath Range", Float) = 0.2
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 200

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha // ͸�����
                ZWrite Off // �������д��

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"
                #include "Lighting.cginc" // ������ռ����

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL; // ����
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float3 worldNormal : TEXCOORD1; // ����ռ䷨��
                    float3 worldPos : TEXCOORD2; // ����ռ�λ��
                    float3 viewDir : TEXCOORD3; // ���߷���
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

                    // �������Ч������
                    float breathFactor = sin(_Time.y * _BreathSpeed) * _BreathRange + 1.0;

                    // Ӧ�ú���Ч��������λ�ã����ţ�
                    v.vertex.xyz *= breathFactor;

                   
                    

                    // ������λ��ת�����ü��ռ�
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    // ���� UV ����
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                    // ��������ռ䷨�ߺ�λ��
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                    // �������߷���
                    o.viewDir = normalize(_WorldSpaceCameraPos - o.worldPos);

                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // ��������Ч��
                    float3 refractedDir = refract(-i.viewDir, normalize(i.worldNormal), _RefractionStrength);
                    float2 refractedUV = i.uv + refractedDir.xy * _BubbleThickness;

                    // ��������������
                    fixed4 refractedColor = tex2D(_MainTex, refractedUV);

                    // ����߹�Ч��
                    float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                    float3 halfwayDir = normalize(lightDir + i.viewDir);
                    float spec = pow(max(0, dot(normalize(i.worldNormal), halfwayDir)), _SpecularPower);

                    // ���������Ч��
                    float fresnel = 1.0 - max(0, dot(normalize(i.worldNormal), i.viewDir));
                    fresnel = pow(fresnel, _FresnelPower);

                    // ���㶯̬����Ч��
                    float bubbleEffect = sin(_Time.y * _BubbleSpeed) * 0.5 + 0.5;

                    // ������ɫ
                    fixed4 col = refractedColor * _Color;
                    col.rgb += spec * bubbleEffect; // ��Ӹ߹�Ͷ�̬Ч��
                    col.a = _Color.a * fresnel; // ʹ�÷�����Ч������͸����

                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}