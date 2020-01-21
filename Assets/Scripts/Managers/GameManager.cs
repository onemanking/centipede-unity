using System;
using System.Collections.Generic;
using CentipedeGame.GameObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CentipedeGame.Managers
{
	public class GameManager : Singleton<GameManager>
	{
		public const string BULLET = "Bullet";
		public const string MUSHROOM = "Mushroom";
		public const string PLAYER = "Player";
		public const string CENTIPEDE = "Centipede";

		private const float _LIMIT_PERCENTAGE = 0.15f;
		private const int _PAUSE_TIME_SCALE = 0;
		private const int _NORMAL_TIME_SCALE = 1;

		public static Vector2 ScreenBounds => _ScreenBounds;
		private static Vector3 _ScreenBounds;

		public static float LimitScreenHeight => _LimitScreenHeight;
		private static float _LimitScreenHeight;

		[Header("GAME PREFABS")]
		[SerializeField] private Player m_PlayerPrefab;
		[SerializeField] private Centipede m_CentipedePrefab;
		[SerializeField] private Mushroom m_MushroomPrefab;

		[Header("GAME CONFIGURATION")]
		[SerializeField] private int m_PlayerLife = 3;
		[SerializeField] private int m_PlayerSpeed = 20;
		[SerializeField] private int m_PlayerFireRate = 10;
		[SerializeField] private int m_CentipedeLength = 15;
		[SerializeField] private int m_CentipedeSpeed = 10;
		[SerializeField] private int m_CentipedeScore = 100;
		[SerializeField] private int m_MushroomMax = 35;
		[SerializeField] private int m_MushroomHp = 3;

		[Header("GAME UI")]
		[SerializeField] private Text m_ScoreText;
		[SerializeField] private Text m_PlayerLifeText;
		[SerializeField] private Text m_GameOverText;

		public int CentipedeLength => m_CentipedeLength;
		public int PlayerSpeed => m_PlayerSpeed;
		public int PlayerFireRate => m_PlayerFireRate;
		public int CentipedeSpeed => m_CentipedeSpeed;
		public int MushroomHp => m_MushroomHp;

		private int _CurrentPlayerLife;
		private int _Score;
		private bool _IsGameOver;
		private List<Centipede> _CentipedeList;

		protected override void Awake()
		{
			base.Awake();

			_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
			_LimitScreenHeight = GameManager.ScreenBounds.y * _LIMIT_PERCENTAGE;
			_CurrentPlayerLife = m_PlayerLife;
		}

		private void Start()
		{
			DisplayGameOver(false);
			DisplayPlayLife();
			UpdateScore(0);
			CreatePlayer();
			CreateCentipede();
			CreateMushroom();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) && _IsGameOver) SceneManager.LoadScene(0);
		}

		private void UpdateScore() => UpdateScore(m_CentipedeScore);

		private void UpdateScore(int _score)
		{
			_Score += _score;
			m_ScoreText.text = $"Score: {_Score}";
		}

		private void DecreasePlayerLife()
		{
			_CurrentPlayerLife--;
			DisplayPlayLife();
		}

		private void DisplayPlayLife()
		{
			m_PlayerLifeText.text = $"Life: {_CurrentPlayerLife}";
		}

		public void CheckGameOver()
		{
			DecreasePlayerLife();
			if (_CurrentPlayerLife > 0)
				RestartGame();
			else
				DisplayGameOver(true);
		}
		private void RestartGame()
		{
			foreach (var go in FindObjectsOfType<UnitObject>())
			{
				Destroy(go.gameObject);
			}

			Start();
		}

		private void DisplayGameOver(bool _isGameOver)
		{
			Time.timeScale = _isGameOver ? _PAUSE_TIME_SCALE : _NORMAL_TIME_SCALE;
			_IsGameOver = _isGameOver;
			m_GameOverText.gameObject.SetActive(_isGameOver);
		}

		private void CreatePlayer()
		{
			SpawnUnitObjectToGrid(m_PlayerPrefab, GridManager.Instance.RandomGridBottomPosition());
		}

		private void CreateCentipede()
		{
			_CentipedeList = new List<Centipede>();
			var pos = Vector2.zero;
			for (int i = 0; i < CentipedeLength; i++)
			{
				pos = new Vector2(i, GridManager.Instance.GetTopLeftGridPosition().y);
				var centipede = SpawnUnitObjectToGrid(m_CentipedePrefab, pos) as Centipede;
				centipede.SetOrder(i);
				_CentipedeList.Add(centipede);
			}
		}

		private void CreateMushroom()
		{
			for (int i = 0; i < m_MushroomMax; i++)
			{
				var pos = GridManager.Instance.RandomGridPosition();
				SpawnUnitObjectToGrid(m_MushroomPrefab, pos);
			}
		}

		public UnitObject SpawnUnitObjectToGrid(UnitObject _prefab, Vector2 _position)
		{
			var unitObject = Instantiate(_prefab, _position, Quaternion.identity);
			_position.ToGrid().SetCurrentUnitObject(unitObject);
			return unitObject;
		}

		public void UpdateCentipede(Centipede _centipede, int _order)
		{
			UpdateScore();
			_CentipedeList.Remove(_centipede);

			if (_CentipedeList.Count <= 0)
			{
				DisplayGameOver(true);
				return;
			}

			for (int i = 0; i < _CentipedeList.Count; i++)
			{
				if (i < _order && _CentipedeList[i]) _CentipedeList[i].ToggleDirection();
			}

		}
	}
}