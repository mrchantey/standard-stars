using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// using Ahoy;

namespace Ahoy.Shaders
{

	public class MultiCamManager : MonoBehaviour
	{

		// public Li
		public CameraMaterialPair[] camMatPairs;
		// vector3

		RenderTexture[] renderTextures;

		int width, height;

		void OnEnable()
		{
			ReInitialize();
		}


		void Update()
		{
			if (Screen.width != width || Screen.height != height)
			{
				ReInitialize();
				width = Screen.width;
				height = Screen.height;
			}
		}

		void ReInitialize()
		{
			TryRelease();
			Debug.Log($"MultiCamManager - reinitializing..");
			renderTextures = camMatPairs.Select(cmp =>
			{
				var tex = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGBFloat);
				tex.name = cmp.camera.name;
				tex.Create();

				cmp.camera.targetTexture = tex;
				cmp.materials.ForEach(mat => mat.SetTexture("_MainTex", tex));

				return tex;
			}).ToArray();
		}

		void TryRelease()
		{
			if (renderTextures == null)
				return;
			Debug.Log($"MultiCamManager - releasing..");
			// camMatPairs.ForEach(cmp =>
			// {
			// 	cmp.camera.targetTexture = null;
			// 	cmp.materials.ForEach(mat => mat.SetTexture("_MainTex", null));
			// });
			renderTextures.ForEach(rt => rt.Release());
			renderTextures = null;
		}
		void OnDisable()
		{
			TryRelease();
		}



	}
}