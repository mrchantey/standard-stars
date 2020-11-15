using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [ExecuteAlways]
public class DevicePoseAggregator : MonoBehaviour
{

	public Transform poseAr;
	public Transform poseImu;
	public Transform originAgg;
	public Transform poseAgg;

	void Update()
	{
		var qAgg = poseImu.rotation * Quaternion.Inverse(poseAr.localRotation);
		originAgg.rotation = qAgg;
		var posAgg = originAgg.TransformPoint(poseAr.localPosition);
		poseAgg.position = posAgg;
		poseAgg.rotation = poseImu.rotation;
	}
}
