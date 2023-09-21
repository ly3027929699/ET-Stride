using UnityEngine;
using Stride.UI;
using Stride.UI.Controls;

namespace ET.Client
{
	[ComponentOf(typeof(UI))]
	public class UIHelpComponent : Entity, IAwake
	{
		public TextBlock text;
	}
}
