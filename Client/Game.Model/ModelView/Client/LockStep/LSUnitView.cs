using Stride.Core.Mathematics;
using Stride.Engine;
using UnityEngine;

namespace ET
{
    [ChildOf(typeof(LSUnitViewComponent))]
    public class LSUnitView: Entity, IAwake<Stride.Engine.Entity>, IUpdate, ILSRollback
    {
        public Stride.Engine.Entity GameObject { get; set; }
        public TransformComponent Transform { get; set; }
        public EntityRef<LSUnit> Unit;
        public Vector3 Position;
        public Quaternion Rotation;
        public float totalTime;
        public float t;
    }
}