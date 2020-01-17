using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	public static GridManager Instance => _Instance;
	private static GridManager _Instance;

	[SerializeField] private int m_Width;
	[SerializeField] private int m_Height;
	[SerializeField] private float m_CellSize;

	public int GridLength => _GridList.Count;

	private List<Grid> _GridList = new List<Grid>();
	private void Awake()
	{
		_Instance = GetComponent<GridManager>();
	}

	private void Start()
	{
		for (int x = 0; x < m_Width; x++)
		{
			for (int y = 0; y < m_Height; y++)
			{
				var pos = new Vector2(x, y) * m_CellSize;
				var grid = new Grid(pos);
				_GridList.Add(grid);
			}
		}
	}

	public Vector2 GetPosition(int _index)
	{
		return _GridList[_index].Position;
	}

	public Vector2 GetLeftPosition(Vector2 _position)
	{
		var grid = _GridList.Where(x => x.Position.x < _position.x && _position.y == x.Position.y && Vector2.Distance(x.Position, _position) == m_CellSize).FirstOrDefault();
		if (grid == null) return _position;
		return grid.Position;
	}

	public Vector2 GetRightPosition(Vector2 _position)
	{
		var grid = _GridList.Where(x => x.Position.x > _position.x && _position.y == x.Position.y && Vector2.Distance(x.Position, _position) == m_CellSize).FirstOrDefault();
		if (grid == null) return _position;
		return grid.Position;
	}
	public Vector2 GetUpPosition(Vector2 _position)
	{
		var grid = _GridList.Where(x => x.Position.y > _position.y && _position.x == x.Position.x && Vector2.Distance(x.Position, _position) == m_CellSize).FirstOrDefault();
		if (grid == null) return _position;
		return grid.Position;
	}

	public Vector2 GetDownPosition(Vector2 _position)
	{
		var grid = _GridList.Where(x => x.Position.y < _position.y && _position.x == x.Position.x && Vector2.Distance(x.Position, _position) == m_CellSize).FirstOrDefault();
		if (grid == null) return _position;
		return grid.Position;
	}
}
