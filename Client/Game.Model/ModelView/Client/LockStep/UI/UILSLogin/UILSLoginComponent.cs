using Stride.UI;
using Stride.UI.Controls;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UI))]
	public class UILSLoginComponent: Entity, IAwake
	{
		public EditText account;
		public EditText password;
		public Button loginBtn;
	}
}
