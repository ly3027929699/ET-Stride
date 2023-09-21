using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace ET.Client
{
    [EntitySystemOf(typeof(UILSLoginComponent))]
    [FriendOf(typeof(UILoginComponent))]
    [FriendOfAttribute(typeof(ET.Client.UILSLoginComponent))]
    public static partial class UILSLoginComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UILSLoginComponent self)
        {
            
            UIPage uiPage = self.GetParent<UI>().GameObject.Get<Stride.Engine.UIComponent>().Page;
            self.loginBtn = uiPage.RootElement.FindVisualChildOfType<Button>("LoginBtn");
            
            self.account =uiPage.RootElement.FindVisualChildOfType<EditText>("Account");
            self.password =uiPage.RootElement.FindVisualChildOfType<EditText>("Password");
        }


        public static void OnLogin(this UILSLoginComponent self)
        {
            LoginHelper.Login(
                self.Root(),
                self.account.Text,
                self.password.Text).Coroutine();
        }
    }
}
