using UnityEngine;

public class Grid
{
	private Vector2 _Position;
	public Vector2 Position => _Position;

	public Grid(Vector2 _position)
	{
		_Position = _position;
	}
}
