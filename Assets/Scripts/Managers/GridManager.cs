using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Grid = CentipedeGame.Entities.Grid;

namespace CentipedeGame.Managers
{
	public class GridManager : Singleton<GridManager>
	{
		[SerializeField] private int m_Size;
		[SerializeField] private float m_CellSize;

		public float CellSize => m_CellSize;

		private List<Grid> _GridList = new List<Grid>();

		private void Start()
		{
			for (int x = 0; x < m_Size; x++)
			{
				for (int y = 0; y < m_Size; y++)
				{
					var pos = new Vector2(x, y) * m_CellSize;
					var grid = new Grid(pos);
					_GridList.Add(grid);
				}
			}
		}

		public Vector2 GetLeftPosition(Vector2 _position)
		{
			var grid = _GridList.Where(x => x.Position.x < _position.x && _position.y == x.Position.y && Vector2.Distance(x.Position, _position) <= m_CellSize).FirstOrDefault();
			if (grid == null) return _position;
			return grid.Position;
		}

		public Vector2 GetRightPosition(Vector2 _position)
		{
			var grid = _GridList.Where(x => x.Position.x > _position.x && _position.y == x.Position.y && Vector2.Distance(x.Position, _position) >= m_CellSize).FirstOrDefault();
			if (grid == null) return _position;
			return grid.Position;
		}
		public Vector2 GetUpPosition(Vector2 _position)
		{
			var grid = _GridList.Where(x => x.Position.y > _position.y && _position.x == x.Position.x && Vector2.Distance(x.Position, _position) >= m_CellSize).FirstOrDefault();
			if (grid == null) return _position;
			return grid.Position;
		}

		public Vector2 GetDownPosition(Vector2 _position)
		{
			var grid = _GridList.Where(x => x.Position.y < _position.y && _position.x == x.Position.x && Vector2.Distance(x.Position, _position) <= m_CellSize).FirstOrDefault();
			if (grid == null) return _position;
			return grid.Position;
		}

		public Grid GetGrid(Vector2 _position) => _GridList.Where(x => x.Position == _position).FirstOrDefault();

		public Vector2 RandomGridPosition() => _GridList.Where(_ => _.Position.y >= m_CellSize * 2 && _.Position.y < GetTopLeftGridPosition().y && _.CurrentUnitObject == null).Select(x => x).OrderBy(grid => Guid.NewGuid()).FirstOrDefault().Position;

		public Vector2 RandomGridBottomPosition() => _GridList.Where(_ => _.Position.y == 0 && _.CurrentUnitObject == null).Select(x => x).OrderBy(grid => Guid.NewGuid()).FirstOrDefault().Position;

		public Vector2 GetTopLeftGridPosition()
		{
			var pos = Vector2.zero;
			return _GridList.Where(g => g.Position.x <= pos.x && g.Position.y >= pos.y).Select(gird => pos = gird.Position).LastOrDefault();
		}

		public Vector2 GetTopRightGridPosition()
		{
			var pos = Vector2.zero;
			return _GridList.Where(g => g.Position.x >= pos.x && g.Position.y >= pos.y).Select(gird => pos = gird.Position).LastOrDefault();
		}
	}
}