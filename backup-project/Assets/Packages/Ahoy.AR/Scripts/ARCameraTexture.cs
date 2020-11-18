using UnityEngine;
using Ahoy;


[RequireComponent(typeof(Camera))]
public class ARCameraTexture : MonoBehaviour
{


	float _width;
	float _height;

	Camera cam;
	public RenderTexture myTex;
	// public Texture2DUnityEvent onTexChange;

	void Start()
	{
		cam = GetComponent<Camera>();
	}

	void Update()
	{
		if (_width == Screen.width && _height == Screen.height)
			return;
		_width = Screen.width;
		_height = Screen.height;
		if (cam.targetTexture != null)
			cam.targetTexture.Release();
		var tex = new RenderTexture(Screen.width, Screen.height, 24);
		cam.targetTexture = tex;
		myTex = tex;
		// onTexChange.Invoke(tex.);
	}
}
