using Stride.Core.Mathematics;
using Stride.Input;
using Unity.Mathematics;

namespace ET.Client
{
	[EntitySystemOf(typeof(LSCameraComponent))]
	[FriendOf(typeof(LSCameraComponent))]
	public static partial class LSCameraComponentSystem
	{
		[EntitySystem]
		private static void Awake(this LSCameraComponent self)
		{
			self.Camera = GlobelEngineScript.Default.Camera;
			self.Camera.Entity.Transform.Rotation = Quaternion.RotationX(math.radians(20));
		}
		
		[EntitySystem]
		private static void LateUpdate(this LSCameraComponent self)
		{
			// 摄像机每帧更新位置
			Room room = self.GetParent<Room>();
			if (room.IsReplay)
			{
				if (GlobelEngineScript.Default.Input.IsKeyDown(Keys.Tab))
				{
					++self.index;
					self.MyUnitView = new LSUnitView();
				}
			}

			LSUnitView? lsUnit = self.MyUnitView;
			if (lsUnit == null)
			{
				long id = room.IsReplay? room.PlayerIds[self.index % room.PlayerIds.Count] : room.GetParent<Scene>().GetComponent<PlayerComponent>().MyId;
				self.MyUnitView = room.GetComponent<LSUnitViewComponent>().GetChild<LSUnitView>(id);
			}

			if (lsUnit == null)
			{
				return;
			}

			Vector3 pos = lsUnit.Transform.Position;
			self.Transform.Position = new Vector3(pos.X, pos.X + 3, pos.Z - 5);
		}
	}
}
