using CentipedeGame.Managers;
using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Centipede : MoveableObject
	{
		[SerializeField] private Centipede m_CentipedeBodyPrefab;

		private int _Order;
		private bool _TurnLeft;
		private bool _GoUp;
		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();

			if (!_TurnLeft)
				GoRight();
			else
				GoLeft();
		}

		public void SetOrder(int _order) => _Order = _order;

		protected override Vector2 LimitMovePosition(Vector2 _position)
		{
			if (InvalidNextPosition(_position))
			{
				ToggleDirection();
				if (!_GoUp) GoDown();
				else GoUp();

				return transform.position;
			}

			return _position;
		}

		protected override bool InvalidNextPosition(Vector2 _nextPosition)
		{
			CheckCollisionCondition(_nextPosition);

			return _nextPosition.x > GameManager.ScreenBounds.x - Width || _nextPosition.x < -(GameManager.ScreenBounds.x + Width)
					|| _nextPosition.y > GameManager.ScreenBounds.y - Height || _nextPosition.y < -(GameManager.ScreenBounds.y + Height)
					|| _nextPosition.HasObject();
		}

		public override void OnCollisionCondition(UnitObject _anotherObject)
		{
			if (_anotherObject.tag == "Bullet")
			{
				// GameManager.Instance.UpdateCentipede(_Order);
				GameManager.Instance.UpdateScore();
				Destroy(gameObject);
			}
			else if (_anotherObject.tag == "Mushroom")
			{
				ToggleDirection();
			}
		}

		protected void ToggleDirection() => _TurnLeft = !_TurnLeft;
	}
}