using UnityEngine;
using Stride.UI.Controls;

namespace ET.Client
{
	[ComponentOf(typeof(UI))]
	public class UILoadingComponent : Entity, IAwake
	{
		public TextBlock text;
	}
}
