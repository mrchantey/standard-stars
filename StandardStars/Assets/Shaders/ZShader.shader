Shader "Ahoy/ZWrite"
{
    Properties
    {
		[Header(General)][Space(10)]

		[Header(Culling)][Space(10)]
		[Enum(Off,0,On,1)]_ZWrite ("ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 4 				//LessEqual
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0

		// [Header(Stencils)][Space(10)]
		// _StencilRef("Stencil Ref", Int) = 0
		// [Enum(UnityEngine.Rendering.CompareFunction)] _StencilTest("Stencil Test", Float) = 3	//Equal
		// [Enum(UnityEngine.Rendering.StencilOp)] _StencilPass("Stencil Pass Op", Float) = 0		//Keep
		// [Enum(UnityEngine.Rendering.StencilOp)] _StencilFail("Stencil Fail Op", Float) = 0		//Keep
		// [Enum(UnityEngine.Rendering.StencilOp)] _StencilZFail("Stencil ZFail Op", Float) = 0	//Keep
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent"}
		ColorMask 0
        ZWrite [_ZWrite]
        ZTest [_ZTest]
        Cull [_Cull]

		// Stencil{
		// 	Ref [_StencilRef]
		// 	Comp [_StencilTest]
		// 	Pass [_StencilPass]
		// 	Fail [_StencilFail]
		// 	ZFail [_StencilZFail]
		// }
		
		Pass
		{
		}
    }
}