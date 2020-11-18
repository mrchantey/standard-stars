// using UnityEngine;
// using Ahoy;
// using Starchart3D;
// using System.Linq;

// namespace StandardStars
// {

// 	public class EquatorialTrackerSystem : MonoBehaviour
// 	{


// 		public DoubleVariable day;
// 		public GeographicCoordsSO geographicCoordsSO;
// 		public AstrobodiesSO astrobodiesSO;



// 		public EquatorialTracker[] trackers;

// 		void UpdateTrackerPosition(EquatorialTracker tracker, double lst)
// 		{
// 			// float deltaDec = tracker.raPerSecond * Time.deltaTime;
// 			// tracker.coords.declination = (tracker.coords.declination + deltaDec) % 24;
// 			// if (tracker.coords.declination < 24)
// 			// 	tracker.coords.declination += 24;
// 			float deltaRa = tracker.raPerSecond * Time.deltaTime;
// 			tracker.coords.rightAscention = ((tracker.coords.rightAscention + deltaRa + 24) % 48) - 24;

// 			var fwd = tracker.coords
// 				.ToHorizontal(geographicCoordsSO.value, lst)
// 				.ToVector3();
// 			tracker.transform.rotation = Quaternion.LookRotation(fwd, Vector3.up);
// 		}

// 		void Update()
// 		{
// 			var lst = StarMath.LocalSiderealTime(astrobodiesSO.value, geographicCoordsSO.value, day.value);
// 			trackers.ForEach(t => UpdateTrackerPosition(t, lst));
// 		}
// 	}

// }