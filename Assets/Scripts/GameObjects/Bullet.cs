﻿using CentipedeGame.Managers;
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
			return (GameManager.ScreenBounds.y - transform.position.y) <= GridManager.Instance.CellSize;
		}

		private void OnTriggerEnter2D(Collider2D _other)
		{
			if (_other.tag == GameManager.MUSHROOM || _other.tag == GameManager.CENTIPEDE)
				Destroy(gameObject);
		}
	}
}