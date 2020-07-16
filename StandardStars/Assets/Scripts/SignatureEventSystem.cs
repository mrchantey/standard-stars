using UnityEngine;
using System.Collections.Generic;

namespace StandardStars
{

	public struct SignatureEventInfo
	{
		public float startTime;
		public GameObject gameObject;
		public MirrorInfo mirrorInfo;
		public float mirrorRpm;
	}

	public class SignatureEventSystem : MonoBehaviour
	{


		public float eventFrequency = 20;
		public float eventDuration = 10;
		public float radius = 10;
		public float scale = 10;

		float lastEvent;

		public MirrorSystem mirrorSystem;
		public GameObject prefab;

		List<SignatureEventInfo> infos;

		void Start()
		{
			lastEvent = Time.time;
			infos = new List<SignatureEventInfo>();
		}
		void Update()
		{
			float dt = Time.time - lastEvent;
			if (dt > eventFrequency)
				SpawnEvent();

			for (int i = infos.Count - 1; i >= 0; i--)
			{
				var info = infos[i];
				var t = Time.time - info.startTime;
				if (t > eventDuration)
				{
					info.mirrorInfo.rpm = info.mirrorRpm;
					infos.Remove(info);
					GameObject.Destroy(info.gameObject);
				}
			}
		}

		public void SpawnEvent()
		{
			lastEvent = Time.time;

			var mirrorInfoIndex = Random.Range(0, mirrorSystem.mirrorInfos.Length);
			var mirrorInfo = mirrorSystem.mirrorInfos[mirrorInfoIndex];
			var dir = mirrorSystem.HACK_GetCoords(mirrorInfo).ToVector3();
			var pos = dir.normalized * radius;
			var rot = Quaternion.LookRotation(dir, Vector3.up);

			var go = GameObject.Instantiate(prefab, pos, rot, transform);
			go.transform.localScale = Vector3.one * scale;
			var sigInfo = new SignatureEventInfo()
			{
				startTime = Time.time,
				gameObject = go,
				mirrorInfo = mirrorInfo,
				mirrorRpm = mirrorInfo.rpm
			};

			mirrorInfo.rpm = 0;
			infos.Add(sigInfo);

		}

	}
}