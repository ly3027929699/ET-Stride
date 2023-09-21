using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Events;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(UILobbyComponent))]
    [FriendOf(typeof(UILobbyComponent))]
    public static partial class UILobbyComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UILobbyComponent self)
        {
            UIPage uiPage = self.GetParent<UI>().GameObject.Get<Stride.Engine.UIComponent>().Page;
            self.btnEnterMap = uiPage.RootElement.FindVisualChildOfType<Button>("EnterMap");
            self.btnEnterMap.Click += (sender, args) => self.EnterMap().Coroutine();
        }
        
        public static async ETTask EnterMap(this UILobbyComponent self)
        {
            Scene root = self.Root();
            await EnterMapHelper.EnterMapAsync(root);
            await UIHelper.Remove(root, UIType.UILobby);
        }
    }
}