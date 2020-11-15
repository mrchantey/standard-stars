// using UnityEngine;
// using UnityEngine.UI;
// using System;

// using Ahoy;

// namespace StandardStars
// {

// 	public class HotColdManager : MonoBehaviour
// 	{

// 		public Image gizmoImage;
// 		public Material gizmoMat;
// 		public Transform player;

// 		[Header("general")]
// 		public bool debug;
// 		[Range(0, 1)]
// 		public float targetReachedThreshold = 0.1f;
// 		[Header("gizmo")]
// 		// public Color colorCold = Color.red;
// 		public Color gizmoColor = new Color(255, 240, 200);
// 		[Range(0.5f, 1f)]
// 		public float gizmoScreenEdge = 0.9f;
// 		[Range(0f, 1f)]
// 		public float gizmoSpeed = 0.01f;

// 		float totalDist;
// 		float hueCold;
// 		float hueWarm;
// 		Transform goToTarget;
// 		Transform lookAtTarget;
// 		Action onTargetReached;

// 		void Awake()
// 		{
// 			gizmoImage.gameObject.SetActive(false);
// 			// float h, s, v;
// 			// Color.RGBToHSV(colorCold, out h, out s, out v);
// 			// hueCold = h;
// 			// Color.RGBToHSV(colorWarm, out h, out s, out v);
// 			// hueWarm = h;
// 		}



// 		public void Begin(Transform goToTarget, Transform lookAtTarget, Action onTargetReached)
// 		{
// 			this.goToTarget = goToTarget;
// 			this.lookAtTarget = lookAtTarget;
// 			this.onTargetReached = onTargetReached;
// 			gizmoImage.gameObject.SetActive(true);
// 			totalDist = Vector3.Distance(player.position, goToTarget.position);
// 		}

// 		void Update()
// 		{
// 			if (goToTarget == null) return;
// 			float dist = Vector3.Distance(player.position, goToTarget.position);
// 			float progress = CalculateProgress(dist);
// 			Vector3 viewportPos;
// 			Vector2 symmetricalViewportPos;
// 			// SetImageColor(progress);
// 			SetImagePosition(out viewportPos, out symmetricalViewportPos);
// 			if (symmetricalViewportPos.magnitude < gizmoScreenEdge - 0.01)
// 				gizmoImage.gameObject.SetActive(false);
// 			// SetImageColor(Color.clear);
// 			else
// 				gizmoImage.gameObject.SetActive(true);
// 			// SetImageColor(gizmoColor);

// 			if (dist < targetReachedThreshold)
// 				OnTargetReached();
// 		}


// 		// void SetImageColor(Color col)
// 		// {
// 		// 	gizmoMat.SetColor("_Color", col);
// 		// }
// 		// void SetImageColor(float t)
// 		// {
// 		// 	var h = Mathf.Lerp(hueWarm, hueCold, t);
// 		// 	gizmoMat.SetColor("_Color", Color.HSVToRGB(h, 1, 1));
// 		// }

// 		//Screenspace- overlay, rect transform everything 0 except scale
// 		Vector3 WorldPosToAnchoredPos(Vector3 worldPos, Graphic graphic)
// 		{
// 			Vector3 viewportPos = Camera.main.WorldToViewportPoint(worldPos);
// 			return ViewportPosToAnchoredPos(viewportPos, graphic);
// 		}

// 		Vector3 ViewportPosToAnchoredPos(Vector3 viewportPos, Graphic graphic)
// 		{
// 			var canvasSize = graphic.canvas.GetComponent<RectTransform>().sizeDelta;
// 			Vector3 anchoredPos = viewportPos * canvasSize;
// 			return anchoredPos;
// 		}


// 		void SetImagePosition(out Vector3 viewportPos, out Vector2 symmetricalViewportPos)
// 		{
// 			viewportPos = Camera.main.WorldToViewportPoint(lookAtTarget.position);
// 			float z = viewportPos.z;
// 			symmetricalViewportPos = viewportPos * 2 - Vector3.one;

// 			if (z < 0)
// 			{
// 				symmetricalViewportPos.x *= -1000;
// 				symmetricalViewportPos.y *= -10;
// 			}

// 			if (
// 				z < 0 ||
// 				Mathf.Abs(symmetricalViewportPos.x) > gizmoScreenEdge ||
// 				Mathf.Abs(symmetricalViewportPos.y) > gizmoScreenEdge)
// 			{
// 				symmetricalViewportPos = symmetricalViewportPos.normalized * gizmoScreenEdge;
// 			}

// 			viewportPos = symmetricalViewportPos * 0.5f + new Vector2(0.5f, 0.5f);
// 			viewportPos.z = z;

// 			var anchoredPos = ViewportPosToAnchoredPos(viewportPos, gizmoImage);
// 			var lerped = Vector3.Lerp(gizmoImage.rectTransform.anchoredPosition, anchoredPos, gizmoSpeed);
// 			gizmoImage.rectTransform.anchoredPosition = lerped;

// 		}

// 		float CalculateProgress(float dist)
// 		{
// 			var progress = dist / totalDist;
// 			if (progress > 1)
// 			{
// 				//bump up total distance if nessecary
// 				totalDist = dist;
// 				progress = 1;
// 			}
// 			return progress;
// 		}

// 		void OnTargetReached()
// 		{
// 			if (debug) Debug.Log($"HotColdManager - target reached!");
// 			Reset();
// 			onTargetReached();
// 		}

// 		public void Reset()
// 		{
// 			goToTarget = null;
// 			lookAtTarget = null;
// 			gizmoImage.gameObject.SetActive(false);

// 		}

// 	}
// }