using System;
using UnityEngine;

namespace ET.Client
{
    [UIEvent(UIType.UILSLogin)]
    public class UILSLoginEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = await UIHelper.CreateUIAsync(uiComponent,UIType.UILSLogin, uiLayer);
            ui.AddComponent<UILSLoginComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}