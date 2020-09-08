Shader "Ahoy/Transparent Scalable"
{
    Properties
    {
        _Color ("Color",Color) = (0,1,1,0.5)
		_Scale ("Scale",Range(-10,10)) = 1
		[Enum(Off,0,On,1)]_ZWrite ("ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 4 //"LessEqual"
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent"}
        ZWrite [_ZWrite]
        ZTest [_ZTest]
        Cull [_Cull]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

			float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
				float4 scaledVert = float4(v.vertex.xyz * _Scale,v.vertex.w);
                o.vertex = UnityObjectToClipPos(scaledVert);
                return o;
            }

            fixed4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
