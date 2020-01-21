using System;
using CentipedeGame.Managers;
using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Centipede : MoveableObject
	{
		private int _Order;
		private bool _TurnLeft;
		private bool _GoUp;
		private float _LimitPositionX;
		private SpriteRenderer _SpriteRenderer;

		protected override void Start()
		{
			base.Start();

			m_Speed = GameManager.Instance.CentipedeSpeed;
			_LimitPositionX = GridManager.Instance.GetTopRightGridPosition().x;
			_SpriteRenderer = GetComponent<SpriteRenderer>();
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
				CurrentGrid.SetCurrentUnitObject(this);
				ToggleUpDown();
				ToggleDirection();

				return transform.position;
			}

			CurrentGrid.SetCurrentUnitObject(null);
			return _position;
		}

		private bool CheckReachedLimit(Vector2 _nextPosition)
		{
			return transform.position.x == _nextPosition.x && transform.position.y == _nextPosition.y;
		}

		protected override bool InvalidNextPosition(Vector2 _nextPosition)
		{
			return CheckReachedLimit(_nextPosition) || _nextPosition.y > GameManager.ScreenBounds.y - Height
					|| _nextPosition.y < -(GameManager.ScreenBounds.y + Height)
					|| (_nextPosition.HasObject() && _nextPosition.GetCurrentUnitObject().tag != tag);
		}

		public void ToggleDirection()
		{
			_TurnLeft = !_TurnLeft;
			_SpriteRenderer.flipX = _TurnLeft;
		}

		private void ToggleUpDown()
		{
			if (!_GoUp) GoDown();
			else GoUp();
		}

		private void OnTriggerEnter2D(Collider2D _other)
		{
			if (_other.tag == GameManager.BULLET)
			{
				GameManager.Instance.UpdateCentipede(this, _Order);
				Destroy(gameObject);
			}
		}
	}
}