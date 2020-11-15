// using UnityEngine;
// using System.Collections.Generic;

// namespace StandardStars
// {

// 	public struct SignatureEventInfo
// 	{
// 		public float startTime;
// 		public GameObject gameObject;
// 		public Transform target;
// 	}

// 	public class SignatureEventSystem : MonoBehaviour
// 	{


// 		public float eventFrequency = 20;
// 		public float eventDuration = 10;
// 		public float radius = 10;
// 		public float scale = 10;

// 		float lastEvent;

// 		public AtmosphereRaySystem raySystem;
// 		public GameObject prefab;

// 		List<SignatureEventInfo> infos;

// 		void Start()
// 		{
// 			lastEvent = Time.time;
// 			infos = new List<SignatureEventInfo>();
// 		}
// 		void Update()
// 		{
// 			float dt = Time.time - lastEvent;
// 			if (dt > eventFrequency)
// 				SpawnEvent();

// 			for (int i = infos.Count - 1; i >= 0; i--)
// 			{
// 				var info = infos[i];
// 				var t = Time.time - info.startTime;
// 				if (t > eventDuration)
// 				{
// 					infos.Remove(info);
// 					GameObject.Destroy(info.gameObject);
// 				}
// 			}
// 		}

// 		public void SpawnEvent()
// 		{
// 			lastEvent = Time.time;

// 			var mirrorInfoIndex = Random.Range(0, raySystem.transforms.Length);
// 			var target = raySystem.transforms[mirrorInfoIndex];
// 			var fwd = target.rotation * Vector3.forward;
// 			var pos = fwd.normalized * radius;
// 			var rot = Quaternion.LookRotation(fwd, Vector3.up);

// 			var go = GameObject.Instantiate(prefab, pos, rot, transform);
// 			go.transform.localScale = Vector3.one * scale;
// 			var sigInfo = new SignatureEventInfo()
// 			{
// 				startTime = Time.time,
// 				gameObject = go,
// 				target = target,
// 			};
// 			infos.Add(sigInfo);
// 		}

// 	}
// }