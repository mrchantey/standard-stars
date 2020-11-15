using System.ComponentModel;
using UnityEngine;
using System.Linq;
using Starchart3D;

namespace StandardStars
{

	public enum PlateArrangement
	{
		Random,
		Spherical,
	}

	public class PlateInstantiator : MonoBehaviour
	{

		public PlateArrangement plateArrangement;

		public EquatorialTrackerSystem trackerSystem;
		public PlateData plateData;

		public GameObject platePrefab;

		PlateInstance[] instances;



		[Range(0, 10)]
		public float radiusMin = 0.5f;
		[Range(0, 10)]
		public float radiusMax = 1.5f;

		[Range(1, 200)]
		public int maxPlates = 30;

		void Start()
		{
			Reset();
		}


		public void Reset()
		{
			if (instances != null)
				instances.ForEach(i => GameObject.Destroy(i.gameObject));
			var plates = plateData.GetPlates();
			plates = plates
				.Take(Mathf.Min(plates.Length, maxPlates))
				.ToArray();

			instances = plates.Select((p, i) =>
			{
				var go = GameObject.Instantiate(platePrefab, Vector3.zero, Quaternion.identity, transform);
				var instance = go.GetComponent<PlateInstance>();
				var tracker = go.GetComponent<EquatorialTracker>();
				instance.Initialize(p);
				trackerSystem.trackers.Add(tracker);
				return instance;
			}).ToArray();
		}


		void Update()
		{
			instances = instances.Where(i => i != null).ToArray();
			instances.ForEach((plate) =>
			{
				Random.InitState(plate.uuid);
				float radius = Random.Range(radiusMin, radiusMax);
				switch (plateArrangement)
				{
					default:
					case PlateArrangement.Random:
						plate.transform.position = Random.insideUnitSphere.normalized * radius;
						break;
					case PlateArrangement.Spherical:
						plate.transform.position = plate.transform.rotation * Vector3.forward * radius;
						break;


				}
			});
		}

	}
}