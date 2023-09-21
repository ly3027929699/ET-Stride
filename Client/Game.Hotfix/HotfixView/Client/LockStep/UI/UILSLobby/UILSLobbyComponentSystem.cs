using System.IO;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace ET.Client
{
    [EntitySystemOf(typeof(UILSLobbyComponent))]
    [FriendOf(typeof(UILSLobbyComponent))]
    public static partial class UILSLobbyComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UILSLobbyComponent self)
        {
            UIPage uiPage = self.GetParent<UI>().GameObject.Get<Stride.Engine.UIComponent>().Page;
            self.enterMap = uiPage.RootElement.FindVisualChildOfType<Button>("EnterMap");
            
            self.replay =uiPage.RootElement.FindVisualChildOfType<Button>("Replay");
            self.replayPath =uiPage.RootElement.FindVisualChildOfType<EditText>("ReplayPath");
            self.text =uiPage.RootElement.FindVisualChildOfType<TextBlock>("Replay");
            
            self.enterMap.Click += (sender, args) =>self.EnterMap().Coroutine();
        }

        private static async ETTask EnterMap(this UILSLobbyComponent self)
        {
            await EnterMapHelper.Match(self.Fiber());
        }
        
        private static void Replay(this UILSLobbyComponent self)
        {
            byte[] bytes = File.ReadAllBytes(self.replayPath.Text);
            
            Replay replay = MemoryPackHelper.Deserialize(typeof (Replay), bytes, 0, bytes.Length) as Replay;
            Log.Debug($"start replay: {replay.Snapshots.Count} {replay.FrameInputs.Count} {replay.UnitInfos.Count}");
            LSSceneChangeHelper.SceneChangeToReplay(self.Root(), replay).Coroutine();
        }
    }
}