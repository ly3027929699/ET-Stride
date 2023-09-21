
using Stride.UI;
using UnityEngine;
using Stride.UI.Controls;

namespace ET.Client
{
	[ComponentOf(typeof(UI))]
	public class UILobbyComponent : Entity, IAwake
	{
		public Button btnEnterMap;
		public TextBlock text;
	}
}
