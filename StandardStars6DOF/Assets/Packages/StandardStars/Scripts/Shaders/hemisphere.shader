/*
coordinate systems & airmass?: 
http://spiff.rit.edu/classes/phys373/lectures/radec/radec.html#:~:text=Right%20Ascension%20(or%20%22RA%22,up%20his%20own%20reference%20frame.
conversion formulae:
https://au.mathworks.com/help/matlab/ref/cart2sph.html
https://au.mathworks.com/help/matlab/ref/sph2cart.html
*/



Shader "Standard Stars/Hemisphere"
{
    Properties
    {
		_ColorA ("Color A",Color) = (0,0,32,1)
		_ColorB ("Color B",Color) = (1,1,1,1)
		_ColorStep1 ("Color Step 1",Range(-1,1)) = 0
		_ColorStep2 ("Color Step 2",Range(-1,1)) = 0.5
		// _Altitude ("Altitude",Range(0,90)) = 10
		// _Azimuth ("Azimuth",Range(0,360)) = 0
		_FOV ("Field of View",Range(0,180)) = 10
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
				float3 worldPos : TANGENT;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
                o.uv = v.uv;
				o.normal = v.normal;
                return o;
            }

			fixed4 _ColorA,_ColorB;
			float _FOV,_SphereRadius,_ColorStep1,_ColorStep2;

			int _NumElements;
			float _AzimuthArray[32];
			float _AltitudeArray[32];

			void getAzimuth(float3 pos){
				// float vert_az = atan2(pos.z,pos.x) + pi;//0,TWO_PI
				float vert_az = atan2(pos.z,pos.x);//-PI,PI
			}

			float getAltitude(float3 pos){
				return atan2(pos.y , sqrt(pos.x *pos.x + pos.z * pos.z));//-PI,PI
			}

			float3 altAzToCartesian(float alt, float az){//RADIANS
				float x = cos(alt) * cos(az);
				float z = cos(alt) * sin(az);
				float y = sin(alt);
				return float3(x,y,z);
			}

			float angleBetween(float3 a,float3 b){
				float d = dot(a,b);
				float m = length(a) * length(b);
				return acos(d/m);
			}

			float getTorchAlpha(float3 vertPos,float _Azimuth,float _Altitude){
				float torch_alt = _Altitude * DEG_2_RAD;
				float torch_az = _Azimuth * DEG_2_RAD * -1 + HALF_PI;//offset, so north is 0
				float torch_fov = _FOV * DEG_2_RAD;

				float3 torch_cart = altAzToCartesian(torch_alt,torch_az);
				float angle = angleBetween(torch_cart,vertPos);
				return clamp(angle / torch_fov,0,1); 
			}

            fixed4 frag (v2f i) : SV_Target
            {
				float torch_a = 1;
				for(int index = 0; index < _NumElements; index++){
					float a = getTorchAlpha(i.worldPos,_AzimuthArray[index],_AltitudeArray[index]);
					torch_a = min(torch_a,a);
				}

				// float torch_a = getTorchAlpha(i.worldPos);

				float alt = getAltitude(i.worldPos) / HALF_PI;

				//this should be airmass
				float alt_t = smoothstep(_ColorStep1,_ColorStep2,alt);

				fixed4 col = lerp(_ColorA,_ColorB,alt_t);

				 col.a = lerp(_ColorA.a,_ColorB.a * torch_a,alt_t);
                return col;
            }
            ENDCG
        }
    }
}
