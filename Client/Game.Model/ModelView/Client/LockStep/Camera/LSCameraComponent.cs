using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof (Room))]
    public class LSCameraComponent: Entity, IAwake, ILateUpdate
    {
        // 战斗摄像机
        private CameraComponent camera;

        public TransformComponent Transform;

        public CameraComponent Camera
        {
            get
            {
                return this.camera;
            }
            set
            {
                this.camera = value;
                this.Transform = this.camera.Entity.Transform;
            }
        }

        private EntityRef<LSUnitView>? unitView;

        public LSUnitView? MyUnitView
        {
            get
            {
                return this.unitView;
            }
            set
            {
                if (value == null)
                    this.unitView = null;
                else
                    this.unitView = value;
            }
        }

        public int index;
    }
}