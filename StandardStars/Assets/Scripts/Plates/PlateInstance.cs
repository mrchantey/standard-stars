using UnityEngine;
using Ahoy.Shaders;
using Starchart3D;

namespace StandardStars
{

	public class PlateInstance : MonoBehaviour
	{


		public PlateInfo plateInfo;
		public int uuid;

		public void Initialize(PlateInfo plateInfo)
		{
			this.plateInfo = plateInfo;
			uuid = (int)(plateInfo.dec * plateInfo.ra * 1000);
			var frameController = GetComponent<SpriteController>();
			float hfps = frameController.framesPerSecond / 2;
			var fpsMin = frameController.framesPerSecond - hfps;
			var fpsMax = frameController.framesPerSecond + hfps;
			frameController.framesPerSecond = Random.Range(fpsMin, fpsMax);
			// frameController.frameOffset = Random.Range(0, frameController.numFrames);

			var tracker = GetComponent<EquatorialTracker>();
			tracker.coords.rightAscention = plateInfo.ra;
			tracker.coords.declination = plateInfo.dec;
		}


	}
}