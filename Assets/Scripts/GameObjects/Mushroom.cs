using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Mushroom : UnitObject
	{
		public override void OnCollisionCondition(UnitObject _anotherObject)
		{
			if (--m_Hp <= 0) Destroy(gameObject);
		}
	}
}