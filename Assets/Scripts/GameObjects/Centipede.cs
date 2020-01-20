using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Centipede : MoveableObject
	{
		private int _Order;
		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();

			GoRight();
		}

		public void SetOrder(int _order) => _Order = _order;

		public override void OnCollisionCondition(UnitObject _anotherObject)
		{
			if (_anotherObject.tag == "Bullet")
			{
				if (--m_Hp <= 0) Destroy(gameObject);
			}
		}
	}
}