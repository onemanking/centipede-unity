public class Bullet : MoveableObject
{
	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		GoUp();
	}
}
