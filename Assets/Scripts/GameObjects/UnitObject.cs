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

	protected virtual void Start()
	{
		var bounds = GetComponent<SpriteRenderer>().bounds;
		_Width = bounds.extents.x;
		_Height = bounds.extents.y;
	}

	protected virtual void Update()
	{

	}

	protected virtual void OnCollisionEnter2D(Collision2D _col)
	{
	}

	protected virtual void OnTriggerEnter2D(Collider2D _other)
	{
		if (m_Hp == 0) Destroy(gameObject);
	}
}
