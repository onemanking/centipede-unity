using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MoveableObject
{
	[SerializeField] private Bullet m_Bullet;

	protected override void Start()
	{
		base.Start();
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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		Instantiate(m_Bullet, transform.position, transform.rotation);
	}

	protected override Vector2 LimitMovePosition(Vector2 _position)
	{
		var limitScreenHeight = GridManager.ScreenBounds.y * 0.15f;
		if (_position.x > GridManager.ScreenBounds.x - Width || _position.x < -(GridManager.ScreenBounds.x + Width)
			|| _position.y > limitScreenHeight || _position.y < -(GridManager.ScreenBounds.y + Height))
			return transform.position;

		return new Vector2(Mathf.Clamp(_position.x, -(GridManager.ScreenBounds.x + Width), GridManager.ScreenBounds.x - Width),
							Mathf.Clamp(_position.y, -(GridManager.ScreenBounds.y + Height), limitScreenHeight));

	}
}
