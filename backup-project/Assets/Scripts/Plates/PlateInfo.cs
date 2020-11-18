using UnityEngine;

namespace StandardStars
{

	[System.Serializable]
	public class PlateInfo
	{
		public float dec;
		public float ra;
		public int id;

		public override string ToString()
		{
			return $"id: {id}\tdec: {dec}\tra: {ra}";
		}
	}

}