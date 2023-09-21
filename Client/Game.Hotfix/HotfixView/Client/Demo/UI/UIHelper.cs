using Stride.Core.Mathematics;
using Stride.Engine;

namespace ET.Client
{
    public static class UIHelper
    {
        [EnableAccessEntiyChild]
        public static async ETTask<UI> Create(Entity scene, string uiType, UILayer uiLayer)
        {
            return await scene.GetComponent<UIComponent>().Create(uiType, uiLayer);
        }
        
        [EnableAccessEntiyChild]
        public static async ETTask Remove(Entity scene, string uiType)
        {
            scene.GetComponent<UIComponent>().Remove(uiType);
            await ETTask.CompletedTask;
        }

        public static async ETTask<UI> CreateUIAsync(UIComponent uiComponent,string uiType, UILayer uiLayer)
        {
            string assetsName = $"Assets/Bundles/UI/Demo/{uiType}.prefab";
            ResourcesLoaderComponent resourcesLoaderComponent = uiComponent.Scene().GetComponent<ResourcesLoaderComponent>();
            if (resourcesLoaderComponent == null)
            {
                throw new NullReferenceException(nameof (resourcesLoaderComponent));
            }
            
            TransformComponent parent = uiComponent.UIGlobalComponent.GetLayer((int)uiLayer);
            Prefab prefab = await resourcesLoaderComponent.LoadAssetAsync<Prefab>(assetsName);
            Stride.Engine.Entity entity = uiComponent.Scene().Root().GetComponent<GlobalComponent>().Global.Entity.Scene.SpawnPrefabModel(prefab,parent.Entity,Matrix.Identity);
            UI ui = uiComponent.AddChild<UI, string, Stride.Engine.Entity>(uiType, entity);
            return ui;
        }
    }
}