using UnityEngine;

public class Bullet : MoveableObject
{
	protected override void Update()
	{
		base.Update();

		GoUp();
	}
}
