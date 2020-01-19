using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
	public Grid CurrentGrid => GridManager.Instance.GetGrid(transform.position);

	[SerializeField] protected int m_Hp;

	protected float Width => _Width;
	protected float Height => _Height;

	private float _Width;
	private float _Height;
	private bool _Inited;

	protected virtual void Start()
	{
		var bounds = GetComponent<SpriteRenderer>().bounds;
		_Width = bounds.extents.x;
		_Height = bounds.extents.y;
	}

	protected virtual void Update()
	{
		if (!_Inited)
		{
			_Inited = true;
			CurrentGrid.SetCurrentUnitObject(this);
		}
	}

	protected virtual void OnDestroy() => CurrentGrid.SetCurrentUnitObject(null);
}
