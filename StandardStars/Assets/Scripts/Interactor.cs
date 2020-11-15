using UnityEngine;
using UnityEngine.Events;
using Ahoy.Shaders;

namespace StandardStars
{

	public enum InteractionBehaviour
	{
		tempSignature,
		signatureReplacePlate,
	}

	public class Interactor : MonoBehaviour
	{

		public SignatureData signatureData;

		public Camera cam;

		public UnityEvent onInteract;

		public InteractionBehaviour interactionBehaviour;
		[Range(-1, 1)]
		public float signaturePositionOffset = 0.1f;
		[Range(0, 60)]
		public float interactionDuration = 10;
		[Range(0, 10)]
		public float fadeoutTime = 2;

		public void Reset()
		{
			foreach (Transform t in transform)
			{
				GameObject.Destroy(t.gameObject);
			}
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
				Interact(Input.mousePosition);
			if (Input.touchCount > 0)
				Input.touches.ForEach(t => Interact(t.position));
		}

		void Interact(Vector2 screenPos)
		{
			var ray = cam.ScreenPointToRay(screenPos);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				var plateInstance = hitInfo.transform.GetComponentInParent<PlateInstance>();
				if (plateInstance != null)
					OnHit(plateInstance);
			}
		}

		void OnHit(PlateInstance plate)
		{
			var index = plate.plateInfo.id - 1;
			var texture = signatureData.images[index];
			var pos = plate.transform.position + plate.transform.forward * signaturePositionOffset;
			var go = GameObject.Instantiate(
				signatureData.prefab,
				pos,
				plate.transform.rotation,
				transform);
			// instance.transform.parent);

			var sigInfo = signatureData.GetSignatures()[index];
			var sig = go.GetComponent<SignatureInstance>();
			sig.Initialize(sigInfo, texture);

			switch (interactionBehaviour)
			{
				case InteractionBehaviour.tempSignature:
					sig.CoroutineDelay(() => sig.GetComponent<MaterialsManager>().FadeTo(0, fadeoutTime), interactionDuration);
					GameObject.Destroy(sig.gameObject, interactionDuration + fadeoutTime);
					break;
				case InteractionBehaviour.signatureReplacePlate:
					plate.GetComponentsInChildren<Collider>().ForEach(c => c.enabled = false);
					plate.CoroutineDelay(() => plate.GetComponent<MaterialsManager>().FadeTo(0, fadeoutTime), interactionDuration);
					GameObject.Destroy(plate.gameObject, interactionDuration + fadeoutTime);
					break;

			}
			onInteract.Invoke();
		}

	}
}
