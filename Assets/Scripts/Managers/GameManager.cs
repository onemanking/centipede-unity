using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	public static Vector2 ScreenBounds => _ScreenBounds;
	private static Vector3 _ScreenBounds;

	[Header("GAME PREFABS")]
	[SerializeField] private Centipede m_CentipedePrefab;

	[Header("GAME CONFIGURATION")]
	[SerializeField] private int m_PlayerLive = 3;
	[SerializeField] private int m_CentipedeLength = 15;

	[Header("GAME UI")]
	[SerializeField] private Text m_ScoreText;

	public int Score => _Score;
	private int _Score;

	protected override void Awake()
	{
		base.Awake();

		_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
	}

	void Start()
	{
		UpdateScore(_Score);
	}

	void Update()
	{

	}

	public void UpdateScore(int _score)
	{
		_Score += _score;
		m_ScoreText.text = $"Score: {_Score}";
	}


	private void CreateCentipede()
	{
		var pos = Vector2.zero;
		for (int i = 0; i < m_CentipedeLength; i++)
		{
			pos = new Vector2(i, GridManager.Instance.GridLength - 1);
			Instantiate(m_CentipedePrefab, pos, Quaternion.identity);
		}
	}
}
