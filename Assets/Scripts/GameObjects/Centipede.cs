using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Centipede : MoveableObject
	{
		protected override void Start()
		{
			base.Start();

			GoRight();
		}

		protected override void Update()
		{
			base.Update();
		}
	}
}