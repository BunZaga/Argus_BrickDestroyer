// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/LaserShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_ScrollXSpeed("X Scroll Speed", float) = 0.0
		_ScrollYSpeed("Y Scroll Speed", float) = 0.0
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        LOD 100
		
        Pass
        {
			Blend SrcAlpha One, SrcAlpha DstAlpha
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Color(0,0,0,0) }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 uv : TEXCOORD0;
				float4 color : COLOR;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _ScrollXSpeed;
			float _ScrollYSpeed;
			fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); 
				o.uv = v.uv;
				o.uv.xy = ((o.uv.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
				o.color.a = 1.0;
				o.color.r = _Color.r;
				o.color.g = _Color.g;
				o.color.b = _Color.b;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				float2 timeOffset = (_Time.y * float3(_ScrollXSpeed, _ScrollYSpeed, 0.0));
				float2 uv = (i.uv.xy / i.uv.z) + timeOffset;
                fixed4 col = tex2D(_MainTex, uv) * i.color;
				col.a *= _Color.a;
                return col;
            }
            ENDCG
        }
    }
}
