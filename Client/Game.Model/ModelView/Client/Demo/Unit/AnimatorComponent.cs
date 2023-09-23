using System.Collections.Generic;
using Stride.Animations;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
	public enum MotionType
	{
		None,
		Idle,
		Run,
	}

	[ComponentOf]
	public class AnimatorComponent : Entity, IAwake, IUpdate, IDestroy
	{
		public float speed;
		public Unit unit = null!;
		public MoveComponent? moveComponent;
		public NumericComponent? numericComponent;
		public AnimationController? animationController;
	}
}