using System;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
	[EntitySystemOf(typeof(AnimatorComponent))]
	[FriendOf(typeof(AnimatorComponent))]
	public static partial class AnimatorComponentSystem
	{
		[EntitySystem]
		private static void Destroy(this AnimatorComponent self)
		{
			
		}
			
		[EntitySystem]
		private static void Awake(this AnimatorComponent self)
		{
			self.speed = 0;
		}
		
		[EntitySystem]
		private static void Update(this AnimatorComponent self)
		{
			Unit unit = self.GetParent<Unit>();
			MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
			float maxSpeed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);
			float moveComponentSpeed = moveComponent.Speed;
			if (moveComponentSpeed != self.speed)
			{
				float t;
				if (moveComponentSpeed == 0)
				{
					t = 0.3f;
				}
				else
				{
					t = GlobelEngineScript.Default.DeltaTime;
				}
				self.speed= self.speed *  (1 - t) + moveComponentSpeed * t;
				AnimationController.RunSpeedEventKey.Broadcast(self.speed/maxSpeed);
			}
		}

	}
}