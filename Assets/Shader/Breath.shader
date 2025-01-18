Shader "Custom/Breath"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1) // �����ɫ����
        _BreathSpeed("Breath Speed", Float) = 1.0
        _BreathRange("Breath Range", Float) = 0.2
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            Pass
            {
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
                float4 _MainTex_ST; // ��������ź�ƫ��
                float4 _Color; // ��ɫ����
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
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // ��������
                    fixed4 texColor = tex2D(_MainTex, i.uv);

                    // ��������ɫ���Զ�����ɫ���
                    fixed4 col = texColor * _Color;
                    return texColor;
                }
                ENDCG
            }
        }
            FallBack "Diffuse" // ���� Shader
}