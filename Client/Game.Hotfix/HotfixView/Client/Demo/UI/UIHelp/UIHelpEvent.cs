using System;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
	[UIEvent(UIType.UIHelp)]
    public class UIHelpEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
	        try
	        {
		        UI ui = await UIHelper.CreateUIAsync(uiComponent,UIType.UIHelp, uiLayer);
		        ui.AddComponent<UIHelpComponent>();
				return ui;
	        }
	        catch (Exception e)
	        {
		        Log.Error(e);
		        return null;
	        }
		}


        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}