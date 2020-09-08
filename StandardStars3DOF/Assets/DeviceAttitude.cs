using UnityEngine;

namespace Ahoy.Android
{

	public class DeviceAttitude : MonoBehaviour
	{


		[Header("parent rotation must = (90,0,0)")]
		public bool _;

		void Start()
		{
			if (SystemInfo.supportsGyroscope)
			{
				Input.gyro.enabled = true;
				Input.gyro.updateInterval = 0.0167f;// set the update interval to it's highest value (60 Hz)
				Debug.Log($"DeviceAttitude - Gyro Initialized");
			}
			else
			{
				Debug.Log($"DeviceAttitude - Gyro Not Supported");
			}
		}

		void Update()
		{
			var qrh = Input.gyro.attitude;
			// right handed to left handed
			var qlh = new Quaternion(qrh.x, qrh.y, -qrh.z, -qrh.w);
			transform.localRotation = qlh;
		}



	}
}