using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class EngineEntityComponent: Entity, IAwake, IDestroy
    {
        private Stride.Engine.Entity gameObject;

        public Stride.Engine.Entity GameObject
        {
            get
            {
                return this.gameObject;
            }
            set
            {
                this.gameObject = value;
                this.Transform = value.Transform;
            }
        }

        public TransformComponent Transform { get; private set; }
    }
}