using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ChangeRotation_SyncGameObjectRotation: AEvent<Scene, ChangeRotation>
    {
        protected override async ETTask Run(Scene scene, ChangeRotation args)
        {
            Unit unit = args.Unit;
            EngineEntityComponent gameObjectComponent = unit.GetComponent<EngineEntityComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }
            var transform = gameObjectComponent.Entity.Transform;
            transform.Rotation = unit.Rotation.ToQuaternion();
            await ETTask.CompletedTask;
        }
    }
}
