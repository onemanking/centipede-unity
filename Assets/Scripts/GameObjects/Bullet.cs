using CentipedeGame.Managers;
using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Bullet : MoveableObject
	{
		protected override void Update()
		{
			base.Update();

			GoUp();
		}

		protected override Vector2 LimitMovePosition(Vector2 _position)
		{
			if (InvalidNextPosition(_position))
			{
				Destroy(gameObject);
				return transform.position;
			}

			return _position;
		}

		protected override bool InvalidNextPosition(Vector2 _nextPosition)
		{
			CheckCollisionCondition(_nextPosition);

			return _nextPosition.HasObject() || (GameManager.ScreenBounds.y - transform.position.y) <= GridManager.Instance.CellSize;
		}

		public override void OnCollisionCondition(UnitObject _anotherObject)
		{
			base.OnCollisionCondition(_anotherObject);

			if (_anotherObject.tag != tag)
				_anotherObject.OnCollisionCondition(this);

			Destroy(gameObject);
		}
	}
}