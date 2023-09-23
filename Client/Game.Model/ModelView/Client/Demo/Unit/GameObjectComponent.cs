using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class EngineEntityComponent: Entity, IAwake, IDestroy
    {
        private Stride.Engine.Entity entity;

        public Stride.Engine.Entity Entity
        {
            get
            {
                return this.entity;
            }
            set
            {
                this.entity = value;
                this.Transform = value.Transform;
            }
        }

        public TransformComponent Transform { get; private set; }
    }
}