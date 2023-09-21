using Stride.UI;
using UnityEngine;
using Stride.UI.Controls;

namespace ET.Client
{
	[ComponentOf(typeof(UI))]
	public class UILoginComponent: Entity, IAwake
	{
		public EditText account;
		public EditText password;
		public Button loginBtn;
	}
}
