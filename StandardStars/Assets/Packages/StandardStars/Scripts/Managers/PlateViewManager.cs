using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ahoy.Shaders;

namespace StandardStars
{
	public class PlateViewManager : MonoBehaviour
	{


		public GameObject starchart3D;
		public GameObject platePrefab;
		public GameObject signaturePrefab;


		[Range(0, 10)]
		public float starchartOriginSpawnCircleRadius = 2;
		[Range(0, 10)]
		public float plateSpawnSphereRadius = 0.5f;
		[Range(0, 10)]
		public float plateToSignatureDuration = 2;

		GameObject plateInstance;
		List<GameObject> signatureInstances = new List<GameObject>();

		public void StartView(out Transform plate, out Transform origin3D)
		{
			var starchartPos = Random.insideUnitCircle.normalized.ToVector3XZ() * starchartOriginSpawnCircleRadius;

			starchart3D.transform.position = starchartPos;

			var plateLocalPos = Random.insideUnitCircle.ToVector3XZ().normalized * plateSpawnSphereRadius;
			var plateWorldPos = plateLocalPos + starchartPos;
			var plateRotation = Quaternion.LookRotation(plateLocalPos, Vector3.up);


			plateInstance = GameObject.Instantiate(platePrefab, plateWorldPos, plateRotation);

			plate = plateInstance.transform;
			origin3D = starchart3D.transform;
		}

		public void PlateToSignature()
		{
			if (plateInstance == null)
			{
				Debug.LogWarning($"PlateViewManager - no plate instance to convert to signature");
				return;
			}

			var pose = plateInstance.transform.Pose();
			var sig = GameObject.Instantiate(signaturePrefab, pose.position + pose.rotation * Vector3.forward * -0.01f, pose.rotation);
			signatureInstances.Add(sig);

			//even on reset finish this coroutine
			plateInstance.GetComponent<MaterialsManager>().FadeTo(0, plateToSignatureDuration);
			var temp = plateInstance;
			this.CoroutineDelay(() => GameObject.Destroy(temp), plateToSignatureDuration);
			plateInstance = null;
		}

		public void Reset()
		{
			if (plateInstance != null)
				GameObject.Destroy(plateInstance);
			signatureInstances.ForEach(go => GameObject.Destroy(go));
			signatureInstances.Clear();
		}
	}
}