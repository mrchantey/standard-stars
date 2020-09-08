using UnityEngine;
using Ahoy;
using Starchart3D;
using System.Linq;

namespace StandardStars
{

	public class AtmosphereRaySystem : MonoBehaviour
	{


		public Material mat;

		[Header("max of 32 beams")]
		public Transform[] transforms;

		void Update()
		{
			var coords = transforms
			.Select(t => new HorizontalCoords(t.rotation))
			.ToArray();

			var azimuthArray = coords.Select(c => (float)c.azimuth).ToArray();
			var altitudeArray = coords.Select(c => (float)c.altitude).ToArray();

			mat.SetInt("_NumElements", coords.Length);
			mat.SetFloatArray("_AzimuthArray", azimuthArray);
			mat.SetFloatArray("_AltitudeArray", altitudeArray);
		}

	}
}