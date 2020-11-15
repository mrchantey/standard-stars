using System;
using UnityEngine;
using Ahoy;
namespace StandardStars
{
	[CreateAssetMenu(fileName = "PlateData", menuName = "StandardStars/PlateData", order = 0)]
	public class PlateData : ScriptableObject
	{

		public TextAsset platesJson;

		// PlateInfo[] plates;

		public PlateInfo[] GetPlates()
		{
			// if (plates == null)
			return JsonArrayUtility.ArrayFromJson<PlateInfo>(platesJson.text);
			// Debug.Log($"PlateData - {plates.Length}");
			// return plates;
		}
	}
}