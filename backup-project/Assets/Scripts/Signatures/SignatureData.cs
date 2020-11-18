using System;
using UnityEngine;
using Ahoy;
namespace StandardStars
{
	[CreateAssetMenu(fileName = "SignatureData", menuName = "StandardStars/SignatureData", order = 0)]
	public class SignatureData : ScriptableObject
	{
		public Texture2D[] images;

		public TextAsset signaturesJson;
		public GameObject prefab;

		SignatureInfo[] sigs;

		public SignatureInfo[] GetSignatures()
		{
			if (sigs == null || sigs.Length == 0)
				sigs = JsonArrayUtility.ArrayFromJson<SignatureInfo>(signaturesJson.text);
			return sigs;
		}

	}
}