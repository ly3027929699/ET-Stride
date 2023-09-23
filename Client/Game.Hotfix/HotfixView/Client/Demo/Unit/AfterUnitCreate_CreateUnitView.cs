using Stride.Core.Mathematics;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            // Unit View层
            string assetsName = $"Bundles/Units/Unit";
            ResourcesLoaderComponent resourcesLoaderComponent = scene.GetComponent<ResourcesLoaderComponent>();
            if (resourcesLoaderComponent == null)
            {
                throw new ArgumentNullException(nameof (resourcesLoaderComponent));
            }

            Prefab prefab = await resourcesLoaderComponent.LoadAssetAsync<Prefab>(assetsName);
            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();
            Stride.Engine.Entity entity = globalComponent.Global.Entity.Scene.SpawnPrefabModel(prefab,globalComponent.Global.Entity,Matrix.Identity);

            entity.Transform.Position = unit.Position.ToVector3();
            unit.AddComponent<EngineEntityComponent>().Entity = entity;
            unit.AddComponent<AnimatorComponent>();
            await ETTask.CompletedTask;
        }
    }
}