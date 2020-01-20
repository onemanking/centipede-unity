using CentipedeGame.Managers;
using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Player : MoveableObject
	{
		[SerializeField] private Bullet m_Bullet;
		[Range(1, 100)]
		[SerializeField] private float m_FireRate = 1.0f;

		private float _NextFire;

		protected override void Start()
		{
			base.Start();

			m_FireRate = m_FireRate <= 0 ? 1 : m_FireRate;
		}

		protected override void Update()
		{
			base.Update();

			if (Input.GetKey(KeyCode.LeftArrow))
			{
				GoLeft();
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				GoRight();
			}

			if (Input.GetKey(KeyCode.UpArrow))
			{
				GoUp();
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				GoDown();
			}

			if (Input.GetKeyDown(KeyCode.Space) && Time.time > _NextFire)
			{
				_NextFire = Time.time + 1f / m_FireRate;
				Shoot();
			}
		}

		private void Shoot()
		{
			Instantiate(m_Bullet, transform.position, transform.rotation);
		}

		protected override Vector2 LimitMovePosition(Vector2 _position)
		{
			if (InvalidNextPosition(_position))
				return transform.position;

			return new Vector2(Mathf.Clamp(_position.x, -(GameManager.ScreenBounds.x + Width), GameManager.ScreenBounds.x - Width),
								Mathf.Clamp(_position.y, -(GameManager.ScreenBounds.y + Height), GameManager.LimitScreenHeight));
		}

		protected override bool InvalidNextPosition(Vector2 _nextPosition)
		{
			CheckCollisionCondition(_nextPosition);

			return _nextPosition.x > GameManager.ScreenBounds.x - Width || _nextPosition.x < -(GameManager.ScreenBounds.x + Width)
					|| _nextPosition.y > GameManager.LimitScreenHeight || _nextPosition.y < -(GameManager.ScreenBounds.y + Height)
					|| _nextPosition.HasObject();
		}
	}
}