using Stride.UI.Controls;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UILSRoomComponent: Entity, IAwake, IUpdate
    {
        public Button saveReplay;
        public EditText saveName;
        public TextBlock totalFrame;
        public TextBlock frameText;
        public TextBlock predictFrameText;
        public EditText jumpToField;
        public Button jump;
        public Button replaySpeed;
        public int frame;
        public int predictFrame;
    }
}