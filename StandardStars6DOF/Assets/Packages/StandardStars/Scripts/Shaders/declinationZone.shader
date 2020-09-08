/*
coordinate systems & airmass?: 
http://spiff.rit.edu/classes/phys373/lectures/radec/radec.html#:~:text=Right%20Ascension%20(or%20%22RA%22,up%20his%20own%20reference%20frame.
conversion formulae:
https://au.mathworks.com/help/matlab/ref/cart2sph.html
https://au.mathworks.com/help/matlab/ref/sph2cart.html
*/



Shader "Standard Stars/Declination Zone"
{
    Properties
    {
		_ColorZone ("Zone Color",Color) = (0,0,32,1)
		_ColorBack ("Background Color",Color) = (1,1,1,1)
		_ColorStep1 ("Color Step 1",Range(-1,1)) = 0
		_ColorStep2 ("Color Step 2",Range(-1,1)) = 0.5
		_DecMin ("Declination Min",Range(-90,90)) = 0
		_DecMax ("Declination Max",Range(-90,90)) = 30
    }
    SubShader
    {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Front
		ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "helper.hlsl"
            struct appdata
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float3 objPos : TANGENT;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.objPos = v.vertex;
                o.uv = v.uv;
				o.normal = v.normal;
                return o;
            }

			fixed4 _ColorZone,_ColorBack;
			float _DecMin,_DecMax,_ColorStep1,_ColorStep2;

			float getDeclination(float3 pos){
				// float x = length(pos.xz);
				// float theta_r = acos(x/pos.y);
				float theta_r = atan2(pos.y,length(pos.xz));
				return theta_r * RAD_2_DEG;
			}

            fixed4 frag (v2f i) : SV_Target
            {
				float dec = getDeclination(i.objPos);


				// float dec_t = smoothstep(-90,_DecMin,dec);
				float dec_t = step(_DecMin,dec) * step(dec,_DecMax);
				// float t = dec / 90 * 0.5 + 0.5;
				return lerp(_ColorBack,_ColorZone,dec_t);
				// return fixed4(t,t,t,1);
				// fixed4 col = fillCol;
				// fixed4 col = fixed4(fillCol.xyz,torch_a);
                // return col;
            }
            ENDCG
        }
    }
}
