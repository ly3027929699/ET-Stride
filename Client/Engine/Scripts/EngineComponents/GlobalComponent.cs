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
            Stride.Engine.Entity defaultGlobal = GlobelEngineScript.Default.Global;

            Stride.Engine.Entity defaultUnit = GlobelEngineScript.Default.Unit;

            Stride.Engine.Entity defaultUi = GlobelEngineScript.Default.UI;

            self.Global = defaultGlobal.Transform;
            self.Unit = defaultUnit.Transform;
            self.UI = defaultUi.Transform;
            self.GlobalConfig = ResourcesLoader.Instance.LoadGlobalConfig();
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