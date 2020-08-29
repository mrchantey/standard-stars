Shader "Standard Stars/Sprite"
{
    Properties
    {
		[Header(General)]
		_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color",Color) = (0,1,1,0.5)
		[Enum(Off,0,On,1)]_ZWrite ("ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 4 //"LessEqual"
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0

		[Header(Spritesheet)]
		_Columns("Columns", int) = 3
		_Rows("Rows", int) = 3
		_FrameIndex ("Frame Index", int) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite [_ZWrite]
        ZTest [_ZTest]
        Cull [_Cull]

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
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
            };

			sampler2D _MainTex;
            float4 _MainTex_ST;

			float2 _Pos;
			float2 _Size;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 _Color;
			uint _Columns,_Rows,_FrameIndex;

            fixed4 frag (v2f i) : SV_Target
            {
				float2 offPerFrame = float2((1 / (float)_Columns), (1 / (float)_Rows));

				float2 spriteUV = float2(
					i.uv.x / _Columns, 
					i.uv.y / _Rows);

				float invCols = 1/(float)_Columns;
				float invRows= 1/(float)_Rows;
				float2 spriteOffset = float2(
					floor(_FrameIndex / (float)_Rows) * invCols,
					(_Rows - 1 - (_FrameIndex % _Rows)) * invRows
				);

				fixed4 col = tex2D(_MainTex, spriteOffset + spriteUV);
				col *= _Color;
                return col;
            }
            ENDCG
        }
    }
}
