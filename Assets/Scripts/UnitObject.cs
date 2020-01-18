using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
	public Grid CurrentGrid => GridManager.Instance.GetGrid(transform.position);

	protected virtual void Start()
	{

	}

	protected virtual void Update()
	{

	}
}
