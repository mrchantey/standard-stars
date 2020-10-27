using UnityEngine;
using System.Collections;

using Ahoy;

namespace StandardStars
{

	public class GameLoopManager : MonoBehaviour
	{

		public bool debug = true;

		public PlateViewManager plateViewManager;
		public HotColdManager hotColdManager;
		public ScoreKeeper scoreKeeper;
		public FinaleManager finaleManager;

		[Range(0, 60)]
		public float delay = 10;

		IEnumerator coroutine;

		void Start()
		{
			StartPlateViewDelay();
		}

		void OnTargetReached()
		{
			if (debug) Debug.Log($"GameLoopManager - target reached");
			plateViewManager.PlateToSignature();
			if (scoreKeeper.IncrementScore() == true)
				finaleManager.BeginFinale();
			else
				StartPlateViewDelay();
		}

		void StartPlateView()
		{
			if (debug) Debug.Log($"GameLoopManager - starting plate view");
			coroutine = null;
			Transform plate, origin3D;
			plateViewManager.StartView(out plate, out origin3D);
			hotColdManager.Begin(origin3D, plate, OnTargetReached);
		}

		void StartPlateViewDelay()
		{
			if (debug) Debug.Log($"GameLoopManager - starting delay");
			coroutine = this.CoroutineDelay(StartPlateView, delay);
		}

		public void Reset()
		{
			if (coroutine != null)
				StopCoroutine(coroutine);
			scoreKeeper.Reset();
			finaleManager.Reset();
			hotColdManager.Reset();
			plateViewManager.Reset();

			StartPlateViewDelay();
		}



	}
}