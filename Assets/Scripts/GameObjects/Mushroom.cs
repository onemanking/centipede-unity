using CentipedeGame.Managers;
using UnityEngine;

namespace CentipedeGame.GameObjects
{
	public class Mushroom : UnitObject
	{
		protected override void OnTriggerEnter2D(Collider2D _other)
		{
			if (_other.tag == GameManager.BULLET)
				if (--m_Hp <= 0) Destroy(gameObject);
		}
	}
}