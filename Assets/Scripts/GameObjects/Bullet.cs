using UnityEngine;

public class Bullet : MoveableObject
{
	protected override void Update()
	{
		base.Update();

		GoUp();
	}

	protected override void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag != "Player") Destroy(gameObject);
	}
}
