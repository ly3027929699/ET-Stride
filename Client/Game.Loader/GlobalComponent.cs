using Stride.Engine;
using UnityEngine;

namespace ET
{
    [FriendOf(typeof (GlobalComponent))]
    public static partial class GlobalComponentSystem
    {
        [EntitySystem]
        public static void Awake(this GlobalComponent self)
        {
            GlobalInitContextData globalInitContextData = GlobalInitContext.Instance.Data;
            if (globalInitContextData.Global == null)
            {
                throw new NullReferenceException("Global is null");
            }

            if (globalInitContextData.Unit == null)
            {
                throw new NullReferenceException("Unit is null");
            }

            if (globalInitContextData.UI == null)
            {
                throw new NullReferenceException("UI is null");
            }

            self.Global = globalInitContextData.Global.Transform;
            self.Unit = globalInitContextData.Unit.Transform;
            self.UI = globalInitContextData.UI.Transform;
            self.GlobalConfig = ResourcesLoader.Instance.LoadBuiltin<GlobalConfig>("GlobalConfig");
        }
    }

    [ComponentOf(typeof (Scene))]
    public class GlobalComponent: Entity, IAwake
    {
        public Stride.Engine.TransformComponent Global{ get; set; }
        public Stride.Engine.TransformComponent Unit{ get; set; }
        public Stride.Engine.TransformComponent UI{ get; set; }
        public GlobalConfig GlobalConfig { get; set; }
    }
}