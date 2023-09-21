using Stride.Core.Mathematics;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(LSUnitViewComponent))]
    public static partial class LSUnitViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this LSUnitViewComponent self)
        {

        }
        
        [EntitySystem]
        private static void Destroy(this LSUnitViewComponent self)
        {

        }

        public static async ETTask InitAsync(this LSUnitViewComponent self)
        {
            Room room = self.Room();
            LSUnitComponent lsUnitComponent = room.LSWorld.GetComponent<LSUnitComponent>();
            Scene root = self.Root();
            foreach (long playerId in room.PlayerIds)
            {
                LSUnit lsUnit = lsUnitComponent.GetChild<LSUnit>(playerId);
                string assetsName = $"Assets/Bundles/Unit/Unit.prefab";
                ResourcesLoaderComponent resourcesLoaderComponent = room.GetComponent<ResourcesLoaderComponent>();
                if (resourcesLoaderComponent == null)
                {
                    throw new NullReferenceException(nameof (resourcesLoaderComponent));
                }

                Prefab prefab = await resourcesLoaderComponent.LoadAssetAsync<Prefab>(assetsName);
                GlobalComponent globalComponent =root.GetComponent<GlobalComponent>();
                Stride.Engine.Entity entity = globalComponent.Global.Entity.Scene.SpawnPrefabModel(prefab,globalComponent.Global.Entity,Matrix.Identity);

                entity.Transform.Position = lsUnit.Position.ToVector();

                LSUnitView lsUnitView = self.AddChildWithId<LSUnitView, Stride.Engine.Entity>(lsUnit.Id, entity);
                lsUnitView.AddComponent<LSAnimatorComponent>();
            }
        }
    }
}