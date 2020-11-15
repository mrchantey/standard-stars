// using UnityEngine;

// using Ahoy;
// using System.Linq;

// namespace StandardStars
// {

// 	[CreateAssetMenu(fileName = "SignaturesManager", menuName = "StandardStars/SignaturesManager", order = 0)]
// 	public class SignaturesManager : InvocableSO
// 	{

// 		public TextAsset observerDataJson;
// 		public GameObject signaturePrefab;

// 		public Texture2D[] signatures;
// 		public string assetPath = "Assets/Packages/StandardStars/Signatures";

// 		// public GameObject[] prefabs;

// 		// public GameObject[] GetPrefabs()
// 		// {
// 		// 	return prefabs;
// 		// }

// 		public override void Invoke()
// 		{
// 			var observerInfos = JsonArrayUtility.ArrayFromJson<SignatureInfo>(observerDataJson.text);

// 			// prefabs = observerInfos.Select((info, i) =>
// 			observerInfos.ForEach((info, i) =>
// 			  {
// 				  // info.texture = signatures[i];
// 				  var go = GameObject.Instantiate(signaturePrefab, Vector3.zero, Quaternion.identity);
// 				  go.GetComponent<SignatureInstance>().Init(info, signatures[i]);
// 				  go.name = $"signature - {info.id}";
// 				  AssetUtility.SavePrefabAsset(assetPath, go);
// 			  });

// 		}


// 	}
// }