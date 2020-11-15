using UnityEngine;
using Ahoy.Shaders;
namespace StandardStars
{

	public class SignatureInstance : MonoBehaviour
	{

		public Material signatureMat;
		public MeshRenderer meshRenderer;

		public SpriteController startPlayController;
		public SpriteController continuePlayController;

		[Range(0, 30)]
		public float framesPerSecond = 8;

		void OnValidate()
		{
			startPlayController.framesPerSecond = framesPerSecond;
			continuePlayController.framesPerSecond = framesPerSecond;
		}

		public void Initialize(SignatureInfo info, Texture2D texture)
		{
			var material = Object.Instantiate(signatureMat);
			material.SetTexture("_MainTex", texture);
			meshRenderer.material = material;
			startPlayController.spriteMat = material;
			startPlayController.startFrameIndex = 0;
			startPlayController.numFrames = info.numFrames;
			continuePlayController.spriteMat = material;
			continuePlayController.startFrameIndex = info.numFrames - info.numLoopFrames;
			continuePlayController.numFrames = info.numLoopFrames;
		}

	}
}