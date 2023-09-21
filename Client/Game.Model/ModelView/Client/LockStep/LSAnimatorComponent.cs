using System.Collections.Generic;
using Stride.Animations;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf]
	public class LSAnimatorComponent : Entity, IAwake, IUpdate, IDestroy
	{
		public Dictionary<string, AnimationClip> animationClips = new();
		public HashSet<string> Parameter = new();

		public MotionType MotionType;
		public float MontionSpeed;
		public bool isStop;
		public float stopSpeed;
		public AnimationComponent Animator;
	}
}