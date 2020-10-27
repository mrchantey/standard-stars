using UnityEngine;
using Ahoy.Shaders;
namespace StandardStars
{

	public class Signature : MonoBehaviour
	{

		public Material signatureMat;

		public MeshRenderer meshRenderer;

		public SpriteController startPlayController;
		public SpriteController continuePlayController;

		void Awake()
		{
			var matInstance = Object.Instantiate(signatureMat);
			meshRenderer.material = matInstance;
			startPlayController.spriteMat = matInstance;
			continuePlayController.spriteMat = matInstance;

			// startPlayController.SetFrameIndex(startPlayController.startFrameIndex);

			// this.DelayAction(() =>
			// {
			// 	meshRenderer.gameObject.SetActive(true);
			// 	// startPlayController.StartPlaying();
			// }, 2f);
		}

	}
}