using UnityEngine;
using Ahoy;
using Starchart3D;
using System.Linq;

namespace StandardStars
{

	public class MirrorSystem : MonoBehaviour
	{


		public Material mat;

		public DoubleVariable day;
		public GeographicCoordsSO geographicCoordsSO;
		public AstrobodiesSO astrobodiesSO;

		[Header("max of 32 beams")]
		public MirrorInfo[] mirrorInfos;


		public HorizontalCoords HACK_GetCoords(MirrorInfo info)
		{
			var lst = StarMath.LocalSiderealTime(astrobodiesSO.value, geographicCoordsSO.value, day.value);
			float deltaRotation = info.rpm / 60 * Time.deltaTime * 24;
			info.rightAscention = (info.rightAscention + deltaRotation) % 24;
			return new EquatorialCoords(info.rightAscention, info.declination)
				.ToHorizontal(geographicCoordsSO.value, lst);
		}

		HorizontalCoords GetCoords(MirrorInfo info, double lst)
		{
			float deltaRotation = info.rpm / 60 * Time.deltaTime * 24;

			info.rightAscention = (info.rightAscention + deltaRotation) % 24;
			// transform.Rotate(0, y, 0, Space.World);

			var horiz = new EquatorialCoords(info.rightAscention, info.declination)
				.ToHorizontal(geographicCoordsSO.value, lst);

			var dirFwd = horiz.ToVector3();
			info.transform.rotation = Quaternion.LookRotation(dirFwd, Vector3.up);
			return horiz;
		}

		void Update()
		{
			var lst = StarMath.LocalSiderealTime(astrobodiesSO.value, geographicCoordsSO.value, day.value);
			// var horiz = new HorizontalCoords(transform.rotation);

			var coords = mirrorInfos
			.Select(i => GetCoords(i, lst))
			.ToArray();

			var azimuthArray = coords.Select(c => (float)c.azimuth).ToArray();
			var altitudeArray = coords.Select(c => (float)c.altitude).ToArray();

			mat.SetInt("_NumElements", coords.Length);
			mat.SetFloatArray("_AzimuthArray", azimuthArray);
			mat.SetFloatArray("_AltitudeArray", altitudeArray);

			// mat.SetFloat("_Azimuth", (float)horiz.azimuth);
			// mat.SetFloat("_Altitude", (float)horiz.altitude);

		}

	}
}