using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.position = GridManager.Instance.GetRightPosition(transform.position);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.position = GridManager.Instance.GetLeftPosition(transform.position);
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.position = GridManager.Instance.GetUpPosition(transform.position);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			transform.position = GridManager.Instance.GetDownPosition(transform.position);
		}
	}
}
