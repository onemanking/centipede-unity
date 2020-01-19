using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
	public Grid CurrentGrid => GridManager.Instance.GetGrid(transform.position);

	protected float Width;
	protected float Height;

	protected virtual void Start()
	{
		var bounds = GetComponent<SpriteRenderer>().bounds;
		Width = bounds.extents.x;
		Height = bounds.extents.y;
	}

	protected virtual void Update()
	{

	}
}
