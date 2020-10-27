using UnityEngine;
using UnityEngine.UI;

using Ahoy;

namespace StandardStars
{

	public class ScoreKeeper : MonoBehaviour
	{
		public Text text;

		[Range(0, 50)]
		public int targetScore = 5;


		int score = 0;

		void OnValidate()
		{
			SetText();
		}

		void SetText() { text.text = $"{score} / {targetScore}"; }

		void Start()
		{
			SetText();
		}

		public bool IncrementScore()
		{
			score++;
			SetText();
			return (score >= targetScore);
		}

		public void Reset()
		{
			score = 0;
			SetText();
		}

	}
}