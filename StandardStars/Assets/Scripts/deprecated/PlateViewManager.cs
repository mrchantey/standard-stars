// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// using Ahoy.Shaders;

// namespace StandardStars
// {
// 	public class PlateViewManager : MonoBehaviour
// 	{


// 		public GameObject starchart3D;
// 		public GameObject platePrefab;
// 		public GameObject signaturePrefab;
// 		public Transform player;


// 		[Range(0, 10)]
// 		public float starchartSpawnRadius = 2;
// 		[Range(0, 10)]
// 		public float plateSpawnRadius = 0.5f;
// 		[Range(0, 10)]
// 		public float plateToSignatureDuration = 2;
// 		[Range(0, 10)]
// 		public float plateToFinaleDuration = 7;

// 		GameObject plateInstance;
// 		List<GameObject> signatureInstances = new List<GameObject>();

// 		public void StartView(out Transform plate, out Transform origin3D)
// 		{
// 			var starchartPos = Random.insideUnitCircle.normalized.ToVector3XZ() * starchartSpawnRadius;

// 			starchart3D.transform.position = starchartPos;

// 			var dirPlayerStarchart = (starchartPos - player.position).normalized;

// 			// var plateLocalPos = Random.insideUnitCircle.ToVector3XZ().normalized * plateSpawnSphereRadius;
// 			// var plateWorldPos = plateLocalPos + starchartPos;
// 			var platePos = starchartPos + dirPlayerStarchart * plateSpawnRadius;
// 			var plateRotation = Quaternion.LookRotation(dirPlayerStarchart, Vector3.up);


// 			plateInstance = GameObject.Instantiate(platePrefab, platePos, plateRotation);

// 			plate = plateInstance.transform;
// 			origin3D = starchart3D.transform;
// 		}

// 		public void ViewFinal()
// 		{
// 			plateInstance.transform.parent = player;
// 			var starchartLast = starchart3D.transform.position;
// 			var plateLast = plateInstance.transform.localPosition;
// 			var plateLastRot = plateInstance.transform.localRotation;
// 			this.CoroutineTimedLoop(t =>
// 			{
// 				starchart3D.transform.position = Vector3.Lerp(starchartLast, Vector3.zero, t);
// 				plateInstance.transform.localPosition = Vector3.Lerp(plateLast, Vector3.forward * 0.3f, t);
// 				plateInstance.transform.localRotation = Quaternion.Slerp(plateLastRot, Quaternion.identity, t);
// 			}, plateToFinaleDuration);
// 		}

// 		public void PlateToSignature()
// 		{
// 			if (plateInstance == null)
// 			{
// 				Debug.LogWarning($"PlateViewManager - no plate instance to convert to signature");
// 				return;
// 			}

// 			var pose = plateInstance.transform.Pose();
// 			var sig = GameObject.Instantiate(signaturePrefab, pose.position + pose.rotation * Vector3.forward * -0.01f, pose.rotation);
// 			signatureInstances.Add(sig);

// 			//even on reset finish this coroutine
// 			plateInstance.GetComponent<MaterialsManager>().FadeTo(0, plateToSignatureDuration);
// 			var temp = plateInstance;
// 			this.CoroutineDelay(() => GameObject.Destroy(temp), plateToSignatureDuration);
// 			plateInstance = null;
// 		}

// 		public void Reset()
// 		{
// 			if (plateInstance != null)
// 				GameObject.Destroy(plateInstance);
// 			// if (plateInstanceFinal != null)
// 			// 	GameObject.Destroy(plateInstanceFinal);
// 			signatureInstances.ForEach(go => GameObject.Destroy(go));
// 			signatureInstances.Clear();
// 		}
// 	}
// }