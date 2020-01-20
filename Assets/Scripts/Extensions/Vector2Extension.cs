using CentipedeGame.GameObjects;
using CentipedeGame.Managers;
using UnityEngine;
using Grid = CentipedeGame.Entities.Grid;

public static class Vector2Extension
{
	public static Grid ToGrid(this Vector2 _source) => GridManager.Instance.GetGrid(_source);

	public static bool HasObject(this Vector2 _source) => GridManager.Instance.GetGrid(_source).CurrentUnitObject != null;

	public static UnitObject CurrentUnitObject(this Vector2 _source) => GridManager.Instance.GetGrid(_source).CurrentUnitObject;
}
