using UnityEngine;

public class Mushroom : UnitObject
{
	protected override void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Bullet") m_Hp--;

		base.OnTriggerEnter2D(_other);
	}
}