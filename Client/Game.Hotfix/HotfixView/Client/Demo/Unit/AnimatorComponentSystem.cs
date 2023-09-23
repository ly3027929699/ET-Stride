using System;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof (AnimatorComponent))]
    [FriendOf(typeof (AnimatorComponent))]
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
            self.unit = self.GetParent<Unit>();
            if (self.unit == null)
            {
                throw new NullReferenceException(nameof (self.unit));
            }

            self.moveComponent = self.unit.GetComponent<MoveComponent>();
            if (self.moveComponent == null)
            {
                Log.Warning($"Has no moveComponent on unit:{self.unit}");
            }

            self.numericComponent = self.unit.GetComponent<NumericComponent>();
            if (self.numericComponent == null)
            {
                Log.Warning($"Has no numericComponent on unit:{self.unit}");
            }

            var engineEntityComponent = self.unit.GetComponent<EngineEntityComponent>();
            if (engineEntityComponent == null)
            {
                Log.Warning($"Has no engineEntityComponent on unit:{self.unit}");
                return;
            }

            self.animationController = engineEntityComponent.Entity.Get<AnimationController>();
            if (self.animationController == null)
            {
                Log.Warning($"Has no animationController on unit:{self.unit}'s model");
            }
        }

        [EntitySystem]
        private static void Update(this AnimatorComponent self)
        {
            if(self.animationController==null)
                return;
            if(self.numericComponent==null)
                return;
            if(self.animationController==null)
                return;

            float maxSpeed = self.numericComponent.GetAsFloat(NumericType.Speed);
            float moveComponentSpeed = self.moveComponent.Speed;
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

                self.speed = self.speed * (1 - t) + moveComponentSpeed * t;
                self.animationController.SetSpeed(self.speed / maxSpeed);
            }
        }
    }
}