using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MoveableObject
{
	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			GoLeft();
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			GoRight();
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			GoUp();
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			GoDown();
		}
	}
}
