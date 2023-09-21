using System;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(UIGlobalComponent))]
    [FriendOf(typeof(UIGlobalComponent))]
    public static partial class UIGlobalComponentSystem
    {
        [EntitySystem]
        public static void Awake(this UIGlobalComponent self)
        {
            string globalUiName = "/Global/UI";
            Stride.Engine.Entity? uiRoot = GlobelEngineScript.Default.GetEntity(globalUiName);
            if (uiRoot == null)
                throw new NullReferenceException($"can't find the entity named {globalUiName}");
            
            UILayerHodler? uiLayerHodler = uiRoot.Get<UILayerHodler>();
            if (uiLayerHodler == null)
                throw new NullReferenceException($"{globalUiName} can't find the component named {globalUiName}");
            
            self.UILayers.Add((int)UILayer.Hidden, uiLayerHodler.Hidden);
            self.UILayers.Add((int)UILayer.Low, uiLayerHodler.Low);
            self.UILayers.Add((int)UILayer.Mid, uiLayerHodler.Mid);
            self.UILayers.Add((int)UILayer.High, uiLayerHodler.High);
        }

        public static async ETTask<UI> OnCreate(this UIGlobalComponent self, UIComponent uiComponent, string uiType, UILayer uiLayer)
        {
            try
            {
                UI ui = await UIEventComponent.Instance.UIEvents[uiType].OnCreate(uiComponent, uiLayer);
                return ui;
            }
            catch (Exception e)
            {
                throw new Exception($"on create ui error: {uiType}", e);
            }
        }

        public static TransformComponent GetLayer(this UIGlobalComponent self, int layer)
        {
            return self.UILayers[layer];
        }

        public static void OnRemove(this UIGlobalComponent self, UIComponent uiComponent, string uiType)
        {
            try
            {
                UIEventComponent.Instance.UIEvents[uiType].OnRemove(uiComponent);
            }
            catch (Exception e)
            {
                throw new Exception($"on remove ui error: {uiType}", e);
            }
        }
    }
}