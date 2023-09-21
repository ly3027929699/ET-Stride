
using Stride.UI;
using Stride.UI.Controls;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UI))]
	public class UILSLobbyComponent : Entity, IAwake
	{
		public Button enterMap;
		public TextBlock text;
		public Button replay;
		public EditText replayPath;
	}
}
