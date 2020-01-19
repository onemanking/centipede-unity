using UnityEngine;

public class Bullet : MoveableObject
{
	protected override void Update()
	{
		base.Update();

		GoUp();
	}

	protected override Vector2 LimitMovePosition(Vector2 _position)
	{
		if (InvalidNextPosition(_position, GameManager.ScreenBounds.y))
		{
			CheckCollisionCondition(_position);
			return transform.position;
		}

		return new Vector2(Mathf.Clamp(_position.x, -(GameManager.ScreenBounds.x + Width), GameManager.ScreenBounds.x - Width),
							Mathf.Clamp(_position.y, -(GameManager.ScreenBounds.y + Height), GameManager.ScreenBounds.y - Height));
	}

	protected override void OnCollisionCondition(UnitObject _anotherObject)
	{
		base.OnCollisionCondition(_anotherObject);

		Destroy(gameObject);
	}
}
