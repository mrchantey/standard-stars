// using UnityEngine;
// using System.Collections;
// using System.Linq;

// using Ahoy;

// namespace StandardStars
// {

// 	public class GameLoopManager : MonoBehaviour
// 	{

// 		public bool debug = true;

// 		public PlateViewManager plateViewManager;
// 		public HotColdManager hotColdManager;
// 		public ScoreKeeper scoreKeeper;

// 		public Transform[] transformsToReset;

// 		[Range(0, 60)]
// 		public float delay = 10;

// 		IEnumerator coroutine;

// 		void Start()
// 		{
// 			StartPlateViewDelay();
// 		}

// 		void OnTargetReached()
// 		{
// 			if (debug) Debug.Log($"GameLoopManager - target reached");
// 			if (scoreKeeper.IncrementScore() == true)
// 				plateViewManager.ViewFinal();
// 			else
// 			{
// 				plateViewManager.PlateToSignature();
// 				StartPlateViewDelay();
// 			}
// 		}

// 		void StartPlateView()
// 		{
// 			if (debug) Debug.Log($"GameLoopManager - starting plate view");
// 			coroutine = null;
// 			Transform plate, origin3D;
// 			plateViewManager.StartView(out plate, out origin3D);
// 			hotColdManager.Begin(origin3D, plate, OnTargetReached);
// 		}

// 		void StartPlateViewDelay()
// 		{
// 			if (debug) Debug.Log($"GameLoopManager - starting delay");
// 			coroutine = this.CoroutineDelay(StartPlateView, delay);
// 		}

// 		public void Reset()
// 		{
// 			if (coroutine != null)
// 				StopCoroutine(coroutine);
// 			scoreKeeper.Reset();
// 			hotColdManager.Reset();
// 			plateViewManager.Reset();
// 			transformsToReset.ForEach(t => { t.position = Vector3.zero; t.rotation = Quaternion.identity; });

// 			StartPlateViewDelay();
// 		}



// 	}
// }