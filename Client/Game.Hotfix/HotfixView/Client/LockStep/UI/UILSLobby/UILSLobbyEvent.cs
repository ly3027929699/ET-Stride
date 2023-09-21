using UnityEngine;

namespace ET.Client
{
    [UIEvent(UIType.UILSLobby)]
    public class UILSLobbyEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = await UIHelper.CreateUIAsync(uiComponent,UIType.UILSLobby, uiLayer);
            ui.AddComponent<UILSLobbyComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}