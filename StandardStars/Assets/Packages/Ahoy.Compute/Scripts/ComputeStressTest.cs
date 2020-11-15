using UnityEngine;

namespace Ahoy.Compute
{

	public class ComputeStressTest : InvocableMono
	{


		public ComputeShader shader;
		public ComputeInstance instance;
		public int numThreads = 100;
		public int iterations = 100;

		public bool optimize;

		ComputeBuffer buff;
		float[] data;

		void OnEnable()
		{
			buff = new ComputeBuffer(numThreads, sizeof(float));
			data = numThreads.SelectArray(i => Random.value);
			var numThreadsAppended = instance.Init(shader, numThreads, optimize);
			buff.SetData(data);
			instance.SetBuffer("data", buff);
			instance.SetInt("iterations", iterations);
			Debug.Log($"ComputeStressTest - threads:{numThreadsAppended}");
		}

		void OnDisable()
		{

			buff.Dispose();
		}

		public override void Invoke()
		{
			instance.Dispatch();
			Debug.Log($"ComputeStressTest - delta time: {Time.deltaTime}");
		}


	}
}