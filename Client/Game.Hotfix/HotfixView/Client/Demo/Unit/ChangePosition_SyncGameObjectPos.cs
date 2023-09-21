using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ChangePosition_SyncGameObjectPos: AEvent<Scene, ChangePosition>
    {
        protected override async ETTask Run(Scene scene, ChangePosition args)
        {
            Unit unit = args.Unit;
            EngineEntityComponent? gameObjectComponent = unit.GetComponent<EngineEntityComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }

            var transform = gameObjectComponent.Transform;
            transform.Position = unit.Position.ToVector3();
            await ETTask.CompletedTask;
        }
    }
}