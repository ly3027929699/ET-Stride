using System;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace ET.Client
{
    [EntitySystemOf(typeof(UILSRoomComponent))]
    [FriendOf(typeof (UILSRoomComponent))]
    public static partial class UILSRoomComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UILSRoomComponent self)
        {
            UIPage uiPage = self.GetParent<UI>().GameObject.Get<Stride.Engine.UIComponent>().Page;
            
            var replay  = uiPage.RootElement.FindVisualChildOfType<UIElement>("Replay");
            var play  = uiPage.RootElement.FindVisualChildOfType<UIElement>("Play");

            self.frameText = uiPage.RootElement.FindVisualChildOfType<TextBlock>("progress");

            Room room = self.Room();
            if (room.IsReplay)
            {
                replay.Visibility = Visibility.Visible;
                play.Visibility = Visibility.Hidden;
                
                
                self.totalFrame = uiPage.RootElement.FindVisualChildOfType<TextBlock>("framecount");
                self.jumpToField = uiPage.RootElement.FindVisualChildOfType<EditText>("jumpToCount");
                self.jump = uiPage.RootElement.FindVisualChildOfType<Button>("jump");
                self.replaySpeed = uiPage.RootElement.FindVisualChildOfType<Button>("speed");

                self.jump.Click += (_, _) =>self.JumpReplay();
                self.replaySpeed.Click += (_, _) =>self.OnReplaySpeedClicked();
                

                self.totalFrame.Text = self.Room().Replay.FrameInputs.Count.ToString();
            }
            else
            {
                replay.Visibility = Visibility.Hidden;
                play.Visibility = Visibility.Visible;
                
                self.predictFrameText = uiPage.RootElement.FindVisualChildOfType<TextBlock>("predict");
                self.saveName = uiPage.RootElement.FindVisualChildOfType<EditText>("SaveName");
                self.saveReplay = uiPage.RootElement.FindVisualChildOfType<Button>("SaveReplay");
                self.saveReplay.Click += (_, _) =>self.OnSaveReplay();
            }
        }

        [EntitySystem]
        private static void Update(this UILSRoomComponent self)
        {
            Room room = self.Room();
            if (self.frame != room.AuthorityFrame)
            {
                self.frame = room.AuthorityFrame;
                self.frameText.Text = room.AuthorityFrame.ToString();
            }

            if (!room.IsReplay)
            {
                if (self.predictFrame != room.PredictionFrame)
                {
                    self.predictFrame = room.PredictionFrame;
                    self.predictFrameText.Text = room.PredictionFrame.ToString();
                }
            }
        }

        private static void OnSaveReplay(this UILSRoomComponent self)
        {
            string name = self.saveName.Text;

            LSClientHelper.SaveReplay(self.Room(), name);
        }

        private static void JumpReplay(this UILSRoomComponent self)
        {
            int toFrame = int.Parse(self.jumpToField.Text);
            LSClientHelper.JumpReplay(self.Room(), toFrame);
        }

        private static void OnReplaySpeedClicked(this UILSRoomComponent self)
        {
            LSReplayUpdater lsReplayUpdater = self.Room().GetComponent<LSReplayUpdater>();
            if (lsReplayUpdater == null)
            {
                throw new NullReferenceException(nameof (lsReplayUpdater));
            }

            lsReplayUpdater.ChangeReplaySpeed();
            self.replaySpeed.FindVisualChildOfType<TextBlock>().Text = $"X{lsReplayUpdater.ReplaySpeed}";
        }
    }
}