using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : UnitObject
{
	[Range(0, 100)]
	[SerializeField] protected int m_Speed;

	private float _WidhtOrtho;
	protected override void Start()
	{
		base.Start();

		_WidhtOrtho = Camera.main.orthographicSize * ((float)Screen.width / (float)Screen.height);

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

		if (_Direction.x == -1)
			transform.position = GridManager.Instance.GetLeftPosition(transform.position);
		else if (_Direction.x == 1)
			transform.position = GridManager.Instance.GetRightPosition(transform.position);
		if (_Direction.y == 1)
			transform.position = GridManager.Instance.GetUpPosition(transform.position);
		else if (_Direction.y == -1)
			transform.position = GridManager.Instance.GetDownPosition(transform.position);

		_Direction = Vector2.zero;
	}

	protected virtual void GoLeft() => _Direction = new Vector2(-1, _Direction.y);

	protected virtual void GoRight() => _Direction = new Vector2(1, _Direction.y);

	protected virtual void GoUp() => _Direction = new Vector2(_Direction.x, 1);

	protected virtual void GoDown() => _Direction = new Vector2(_Direction.x, -1);

}
