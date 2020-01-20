using CentipedeGame.GameObjects;
using UnityEngine;

namespace CentipedeGame.Entities
{
	public class Grid
	{
		private Vector2 _Position;
		public Vector2 Position => _Position;

		private UnitObject _CurrentUnitObject;
		public UnitObject CurrentUnitObject => _CurrentUnitObject;

		public Grid(Vector2 _position)
		{
			_Position = _position;
		}

		public void SetCurrentUnitObject(UnitObject _currentUnitObject) => _CurrentUnitObject = _currentUnitObject;

		public override string ToString() => $"Grid : {Position}";
	}
}
