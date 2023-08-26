using Unity.VisualScripting;
using UnityEngine;

//moves a thing from one spot to another 
public class PositionInterpolator : MonoBehaviour {
    [SerializeField]
	Transform relativeTo = default;

	[SerializeField]
	Rigidbody body = default;
	
	[SerializeField]
	Vector3 from = default, to = default;
	[SerializeField]
	bool moving = true;
	
	public void Interpolate (float t) {
		Vector3 p;
		if (relativeTo) {
			p = Vector3.LerpUnclamped(
				relativeTo.TransformPoint(from), relativeTo.TransformPoint(to), t
			);
		}
		else {
			p = Vector3.LerpUnclamped(from, to, t);
		}
		body.MovePosition(p);
	}
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(moving){
			Interpolate(5f);
		}
	}
}
