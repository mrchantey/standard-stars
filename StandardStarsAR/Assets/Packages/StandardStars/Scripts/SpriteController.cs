using UnityEngine;
using Ahoy;

namespace StandardStars
{

	public class SpriteController : MonoBehaviour
	{

		public Material spriteMat;

		public float framesPerSecond = 2;

		public int startFrameIndex = 0;
		public int numFrames = 9;

		public bool autoStart;
		public bool loop = true;
		bool isPlaying = false;

		float currentTime;

		public NullUnityEvent onStopPlaying;

		void Start()
		{
			if (autoStart)
				StartPlaying();
		}

		public void StartPlaying()
		{
			currentTime = 0;
			isPlaying = true;
		}

		public void StopPlaying()
		{
			isPlaying = false;
			onStopPlaying.Invoke();
		}

		void Update()
		{
			if (!isPlaying)
				return;

			float frameDuration = 1 / framesPerSecond;
			currentTime += Time.deltaTime;

			float totalTime = (numFrames + 1) * frameDuration;

			if (currentTime >= totalTime)
			{
				if (loop)
					StartPlaying();
				else
				{
					StopPlaying();
					return;
				}
			}
			int frameIndex = startFrameIndex + Mathf.FloorToInt(currentTime * framesPerSecond);
			spriteMat.SetInt("_FrameIndex", frameIndex);
		}

	}
}