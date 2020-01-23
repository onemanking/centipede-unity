using System;
using System.Collections.Generic;
using CentipedeGame.Managers;
using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Centipede : MoveableObject
	{
		public bool TurnLeft => _TurnLeft;
		public bool IsGoUp => _GoUp;

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

		private Centipede _FrontCentipede;
		private Centipede _TailCentipede;

		protected override void Update()
		{
			base.Update();

			if (_FrontCentipede)
			{
				if (_FrontCentipede.TurnLeft != _TurnLeft && _FrontCentipede.transform.position.y == transform.position.y) ToggleDirection();
			}

			if (!_TurnLeft)
				GoRight();
			else
				GoLeft();
		}

		protected override Vector2 LimitMovePosition(Vector2 _position)
		{
			if (InvalidNextPosition(_position))
			{
				CurrentGrid.SetCurrentUnitObject(this);

				if (_GoUp) GoUp();
				else GoDown();

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
			if ((GameManager.ScreenBounds.y - transform.position.y >= GameManager.ScreenBounds.y && !_GoUp)
				|| (GameManager.ScreenBounds.y - transform.position.y <= GridManager.Instance.CellSize && _GoUp))
				ToggleUpDown();

			return CheckReachedLimit(_nextPosition) || (_nextPosition.HasObject() && _nextPosition.GetCurrentUnitObject().tag != tag);
		}

		public void ToggleDirection()
		{
			_TurnLeft = !_TurnLeft;
			_SpriteRenderer.flipX = _TurnLeft;
		}

		private void ToggleUpDown() => _GoUp = !_GoUp;

		private void OnTriggerEnter2D(Collider2D _other)
		{
			if (_other.tag == GameManager.BULLET)
			{
				GameManager.Instance.UpdateCentipede(this);
				Destroy(gameObject);
			}
		}

		protected void SetDirection(bool _turnLeft)
		{
			_TurnLeft = _turnLeft;
		}

		public void SetNeighbor(Centipede _front, Centipede _tail)
		{
			_FrontCentipede = _front;
			_TailCentipede = _tail;
		}

		protected void RemoveFront()
		{
			_FrontCentipede = null;
		}

		protected void RemoveTail()
		{
			_TailCentipede = null;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			if (_FrontCentipede)
			{
				_FrontCentipede.SetDirection(_TurnLeft);
				_FrontCentipede.RemoveTail();
			}

			if (_TailCentipede)
			{
				_TailCentipede.SetDirection(!_TurnLeft);
				_TailCentipede.RemoveFront();
			}
		}
	}
}