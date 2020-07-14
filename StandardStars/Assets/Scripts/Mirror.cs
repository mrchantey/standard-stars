using UnityEngine;
using Ahoy;
using Starchart3D;


namespace StandardStars
{

	public class Mirror : MonoBehaviour
	{


		public Material mat;

		public float speed = 0.1f;

		void Update()
		{
			var y = speed * Time.deltaTime;
			transform.Rotate(0, y, 0, Space.World);

			var horiz = new HorizontalCoords(transform.rotation);

			mat.SetFloat("_Azimuth", (float)horiz.azimuth);
			mat.SetFloat("_Altitude", (float)horiz.altitude);

		}

	}
}