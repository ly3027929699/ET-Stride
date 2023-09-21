using System;
using Stride.Core.Mathematics;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [UIEvent(UIType.UILSRoom)]
    public class UILSRoomEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            string assetsName = $"Assets/Bundles/UI/LockStep/{UIType.UILSRoom}.prefab";
            ResourcesLoaderComponent resourcesLoaderComponent = uiComponent.Room().GetComponent<ResourcesLoaderComponent>();
            if (resourcesLoaderComponent == null)
            {
                throw new NullReferenceException(nameof (resourcesLoaderComponent));
            }

            Prefab prefab = await resourcesLoaderComponent.LoadAssetAsync<Prefab>(assetsName);
            TransformComponent transformComponent = uiComponent.UIGlobalComponent.GetLayer((int)uiLayer);
            GlobalComponent globalComponent =uiComponent.Root().GetComponent<GlobalComponent>();
            Stride.Engine.Entity entity = globalComponent.Global.Entity.Scene.SpawnPrefabModel(prefab,transformComponent.Entity,Matrix.Identity);
            
            UI ui = uiComponent.AddChild<UI, string, Stride.Engine.Entity>(UIType.UILSRoom, entity);
            ui.AddComponent<UILSRoomComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}