using System.Collections;
using System.Collections.Generic;
using CentipedeGame.Managers;
using UnityEngine;
using Grid = CentipedeGame.Entities.Grid;

namespace CentipedeGame.GameObjects
{
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
			_Inited = CurrentGrid.HasObject;

			if (!_Inited)
			{
				_Inited = true;
				CurrentGrid.SetCurrentUnitObject(this);
			}

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

		protected virtual void OnDestroy()
		{
			if (CurrentGrid.CurrentUnitObject == this) CurrentGrid.SetCurrentUnitObject(null);
		}

		protected virtual void OnTriggerEnter2D(Collider2D _other)
		{
			Debug.Log($"{_other.tag}");
		}
	}
}