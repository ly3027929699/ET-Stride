using System;

namespace ET.Client
{
    [EntitySystemOf(typeof(EngineEntityComponent))]
    public static partial class GameObjectComponentSystem
    {
        [EntitySystem]
        private static void Destroy(this EngineEntityComponent self)
        {
            self.GameObject.Scene = null;
        }
        
        [EntitySystem]
        private static void Awake(this EngineEntityComponent self)
        {

        }
    }
}