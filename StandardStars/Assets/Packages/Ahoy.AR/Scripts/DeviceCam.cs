// using System.Net.Mime;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.UI;

// public enum CameraState
// {
// 	AwaitingAuth,
// 	Authorized,
// 	Failed,
// 	InitializedWebcam,
// 	InitializedARCam
// }

// public class DeviceCam : MonoBehaviour
// {

// 	public bool frontFacing;
// 	[Header("Ensure the Unity Remote App has camera permissions")]
// 	public int editorSkipDevices = 2;

// 	CameraState cameraState = CameraState.AwaitingAuth;
// 	public RawImage img;

// 	// [Header("AR Info")]
// 	// public RenderTexture arTex;
// 	public ARCameraTexture arCamTex;

// 	WebCamTexture webcamTex;
// 	AspectRatioFitter fitter;

// 	float _ratio;
// 	float _orientation;

// 	void Start()
// 	{
// 		fitter = img.GetComponent<AspectRatioFitter>();
// 		Application.RequestUserAuthorization(UserAuthorization.WebCam);
// 	}

// 	void InitializeWebcam()
// 	{
// 		var numDevices = WebCamTexture.devices.Length;
// 		if (numDevices == 0)
// 		{
// 			Debug.Log($"DeviceCam - no cameras detected");
// 			cameraState = CameraState.Failed;
// 		}
// 		else if (Application.isEditor && numDevices <= editorSkipDevices)
// 		{
// 			Debug.Log($"DeviceCam - only {numDevices} devices found");
// 			return;
// 		}
// 		else
// 		{
// 			Debug.Log($"DeviceCam - {numDevices} devices found\n{WebCamTexture.devices.Select(d => d.name).ElementsToString("\t")}");
// 			var devs = WebCamTexture.devices
// 			.Skip(Application.isEditor ? editorSkipDevices : 0)
// 				.Where(d => d.isFrontFacing == frontFacing)
// 				.ToArray();
// 			if (devs.Length == 0)
// 			{
// 				Debug.Log($"DeviceCam - no cameras where frontfacing={frontFacing}");
// 				cameraState = CameraState.Failed;
// 			}
// 			else
// 			{
// 				webcamTex = new WebCamTexture(devs[0].name, Screen.width, Screen.height);
// 				if (webcamTex == null)
// 				{
// 					Debug.Log($"DeviceCam - failed to initialize camera");
// 					cameraState = CameraState.Failed;
// 				}
// 				Debug.Log($"DeviceCam - camera found:{devs[0].name}");
// 				img.texture = webcamTex;
// 				webcamTex.Play();
// 				cameraState = CameraState.InitializedWebcam;
// 			}
// 		}
// 	}

// 	void InitializeCamera()
// 	{
// 		switch (ARSession.state)
// 		{
// 			case ARSessionState.None:
// 			case ARSessionState.Unsupported:
// 			case ARSessionState.NeedsInstall:
// 				InitializeWebcam();
// 				return;
// 			case ARSessionState.Ready:
// 			case ARSessionState.SessionInitializing:
// 			case ARSessionState.SessionTracking:
// 				cameraState = CameraState.InitializedARCam;
// 				return;
// 			default:
// 				return;
// 		}
// 	}


// 	void Update()
// 	{
// 		if (cameraState == CameraState.AwaitingAuth && Application.HasUserAuthorization(UserAuthorization.WebCam))
// 			cameraState = CameraState.Authorized;

// 		switch (cameraState)
// 		{
// 			case CameraState.Authorized:
// 				InitializeCamera();
// 				return;
// 			case CameraState.InitializedWebcam:
// 				UpdateWebcam();
// 				return;
// 			case CameraState.InitializedARCam:
// 				UpdateAR();
// 				return;
// 			case CameraState.Failed:
// 			default:
// 				return;
// 		}
// 	}

// 	void UpdateAR()
// 	{
// 		if (img.texture != arCamTex.myTex)
// 		{
// 			img.texture = arCamTex.myTex;
// 			UpdateRatio((float)img.texture.width / (float)img.texture.height);
// 		}
// 	}

// 	void UpdateWebcam()
// 	{
// 		//check orientation
// 		int orientation = -webcamTex.videoRotationAngle;
// 		if (orientation == _orientation)
// 			return;
// 		_orientation = orientation;
// 		img.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

// 		UpdateRatio((float)webcamTex.width / (float)webcamTex.height);

// 		//flip y
// 		float scaleY = webcamTex.videoVerticallyMirrored ? -1 : 1;
// 		img.rectTransform.localScale = new Vector3(1, scaleY, 1);
// 	}

// 	void UpdateRatio(float ratio)
// 	{
// 		if (ratio == _ratio)
// 			return;
// 		_ratio = ratio;
// 		fitter.aspectRatio = ratio;
// 	}

// }