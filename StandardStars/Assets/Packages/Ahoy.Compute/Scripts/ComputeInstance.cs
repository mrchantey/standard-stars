using System;
using UnityEngine;
using Ahoy.Shaders;

namespace Ahoy.Compute
{

	public class ComputeInstance : MonoBehaviour
	{

		// public bool debug = true;
		[SerializeField]

		[HideInInspector]
		public ComputeShader computeShader;

		Vector3Int numGroups;


		public bool applyShaderPropertiesOnDispatch = true;
		public ShaderPropertiesBase shaderProperties;

		public int kernelIndex { get; private set; }

		Vector3Int Optimize(int M)
		{
			var N = M / 512f;
			var N_cuberoot = Mathf.Pow(N, 1 / 3);
			var n1 = Mathf.CeilToInt(Mathf.Pow(N, 1 / 3));
			var n2 = Mathf.CeilToInt(Mathf.Pow(N / n1, 1 / 2));
			var n3 = Mathf.CeilToInt(N / n1 / n2);
			return new Vector3Int(n1, n2, n3) * 8;
		}

		public Vector3Int Init(ComputeShader computeTemplate, int numThreads, bool optimize = false)
		{
			var appendedNumThreads = numThreads;
			//ceil instead to generate all
			Vector3Int numThreadsDim = optimize ? Optimize(numThreads) : new Vector3Int(numThreads, 1, 1);
			uint totalNumThreads = (uint)numThreadsDim.x * (uint)numThreadsDim.y * (uint)numThreadsDim.z;
			if (totalNumThreads > int.MaxValue)
				throw new Exception($"{totalNumThreads} > ${numThreads}, integer max exceeded");
			Init(computeTemplate, numThreadsDim);
			return numThreadsDim;
		}

		public void Init(ComputeShader computeTemplate, Vector3Int numThreads)
		{
			Init(computeTemplate, numThreads.x, numThreads.y, numThreads.z);
		}

		public void Init(ComputeShader computeTemplate, int numThreadsX, int numThreadsY, int numThreadsZ)
		{
			computeShader = Instantiate(computeTemplate);

			kernelIndex = computeShader.FindKernel("CSMain");

			uint groupSizeX, groupSizeY, groupSizeZ;//these are 8,8,8
			computeShader.GetKernelThreadGroupSizes(kernelIndex, out groupSizeX, out groupSizeY, out groupSizeZ);

			numGroups.x = GetNumGroups((int)groupSizeX, numThreadsX);
			numGroups.y = GetNumGroups((int)groupSizeY, numThreadsY);
			numGroups.z = GetNumGroups((int)groupSizeZ, numThreadsZ);

			int numThreads = numThreadsX * numThreadsY * numThreadsZ;
			computeShader.SetInt("Ahoy_NumThreads", (int)numThreads);
			computeShader.SetInt("Ahoy_NumThreadsX", (int)numThreadsX);
			computeShader.SetInt("Ahoy_NumThreadsY", (int)numThreadsY);
			computeShader.SetInt("Ahoy_NumThreadsZ", (int)numThreadsZ);
			// computeShader.setL

			if (shaderProperties != null)
				shaderProperties.Apply(computeShader, kernelIndex);
		}

		//from that lady
		int GetNumGroups(int groupSize, int numThreads)
		{
			//increase so that num groups includes all threads
			if (numThreads % groupSize > 0)
				numThreads += groupSize - (numThreads % groupSize);
			return (numThreads / groupSize);//integer division
		}

		public virtual void Dispatch()
		{
			if (shaderProperties != null & applyShaderPropertiesOnDispatch)
				shaderProperties.Apply(computeShader, kernelIndex);
			computeShader.Dispatch(kernelIndex, numGroups.x, numGroups.y, numGroups.z);
		}

		//this should only be done after initialization
		public void SetBool(String name, bool val) { computeShader.SetBool(name, val); }
		public void SetFloat(String name, float val) { computeShader.SetFloat(name, val); }
		public void SetInt(String name, int val) { computeShader.SetInt(name, val); }
		public void SetMatrix(String name, Matrix4x4 val) { computeShader.SetMatrix(name, val); }
		public void SetVector(String name, Vector4 val) { computeShader.SetVector(name, val); }
		public void SetBuffer(String name, ComputeBuffer val) { computeShader.SetBuffer(kernelIndex, name, val); }
		public void SetTexture(String name, Texture val) { computeShader.SetTexture(kernelIndex, name, val); }

	}
}