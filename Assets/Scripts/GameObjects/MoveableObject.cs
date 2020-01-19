using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : UnitObject
{
	[Range(0, 100)]
	[SerializeField] protected int m_Speed;

	protected override void Start()
	{
		base.Start();
	}

	private Vector2 _Direction = Vector2.zero;
	protected override void Update()
	{
		base.Update();

		Move();
	}

	private float _NextMove;
	private bool CheckMove()
	{
		if (Time.time > _NextMove)
		{
			_NextMove = Time.time + 1f / m_Speed;
			return true;
		}

		return false;
	}

	private void Move()
	{
		if (!CheckMove()) return;

		CurrentGrid.SetCurrentUnitObject(null);

		if (_Direction.x == -1)
			transform.position = LimitMovePosition(GridManager.Instance.GetLeftPosition(transform.position));
		else if (_Direction.x == 1)
			transform.position = LimitMovePosition(GridManager.Instance.GetRightPosition(transform.position));
		if (_Direction.y == 1)
			transform.position = LimitMovePosition(GridManager.Instance.GetUpPosition(transform.position));
		else if (_Direction.y == -1)
			transform.position = LimitMovePosition(GridManager.Instance.GetDownPosition(transform.position));

		CurrentGrid.SetCurrentUnitObject(this);

		_Direction = Vector2.zero;
	}

	protected virtual Vector2 LimitMovePosition(Vector2 _position)
	{
		if (InvalidNextPosition(_position, GameManager.ScreenBounds.y)) return transform.position;

		return new Vector2(Mathf.Clamp(_position.x, -(GameManager.ScreenBounds.x + Width), GameManager.ScreenBounds.x - Width),
							Mathf.Clamp(_position.y, -(GameManager.ScreenBounds.y + Height), GameManager.ScreenBounds.y - Height));
	}

	protected virtual bool InvalidNextPosition(Vector2 _nextPosition, float _limitScreenHeight)
	{
		return _nextPosition.x > GameManager.ScreenBounds.x - Width || _nextPosition.x < -(GameManager.ScreenBounds.x + Width)
				|| _nextPosition.y > _limitScreenHeight || _nextPosition.y < -(GameManager.ScreenBounds.y + Height)
				|| _nextPosition.HasObject();
	}

	protected virtual void GoLeft() => _Direction = new Vector2(-1, _Direction.y);

	protected virtual void GoRight() => _Direction = new Vector2(1, _Direction.y);

	protected virtual void GoUp() => _Direction = new Vector2(_Direction.x, 1);

	protected virtual void GoDown() => _Direction = new Vector2(_Direction.x, -1);
}
