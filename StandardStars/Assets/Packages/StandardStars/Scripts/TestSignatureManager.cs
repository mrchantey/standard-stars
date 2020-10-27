using UnityEngine;

using Ahoy;
namespace StandardStars
{

	public class TestSignatureManager : InvocableMono
	{

		public GameObject prefab;
		public float range;
		GameObject instance;


		public override void Invoke()
		{
			// if (instance != null)
			// 	GameObject.Destroy(instance);
			var pos = Random.insideUnitCircle * range;
			instance = GameObject.Instantiate(prefab, pos, Quaternion.identity);

		}


	}

}