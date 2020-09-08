using UnityEngine;

namespace StandardStars
{

	public class MirrorInfo : MonoBehaviour
	{


		[Range(-60, 60)]
		public float rpm = 10;

		[Range(-90, 90)]
		public float declination;

		[Range(0, 24)]
		public float rightAscention;

	}
}