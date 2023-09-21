using Stride.Core;
using Stride.Engine;

namespace ET;

public class UILayerHodler: ScriptComponent
{
	[Display]
	public TransformComponent Hidden { get; set; }

	[Display]
	public TransformComponent Low { get; set; }
	[Display]
	public TransformComponent Mid { get; set; }
	[Display]
	public TransformComponent High { get; set; }
}