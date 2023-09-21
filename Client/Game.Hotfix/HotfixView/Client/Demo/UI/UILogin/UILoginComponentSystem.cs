using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace ET.Client
{
    [EntitySystemOf(typeof (UILoginComponent))]
    [FriendOf(typeof (UILoginComponent))]
    public static partial class UILoginComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UILoginComponent self)
        {
            UIPage uiPage = self.GetParent<UI>().GameObject.Get<Stride.Engine.UIComponent>().Page;
            self.loginBtn = uiPage.RootElement.FindVisualChildOfType<Button>("LoginBtn");
            self.loginBtn.Click += (sender, args) => self.OnLogin();

            self.account = uiPage.RootElement.FindVisualChildOfType<EditText>("Account");
            self.password = uiPage.RootElement.FindVisualChildOfType<EditText>("Password");
        }

        public static void OnLogin(this UILoginComponent self)
        {
            LoginHelper.Login(self.Root(),
                self.account.Text,
                self.password.Text).Coroutine();
        }
    }
}