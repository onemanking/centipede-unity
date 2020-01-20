﻿using System;
using CentipedeGame.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace CentipedeGame.Managers
{
	public class GameManager : Singleton<GameManager>
	{
		private const float _LIMIT_PERCENTAGE = 0.15f;

		public static Vector2 ScreenBounds => _ScreenBounds;
		private static Vector3 _ScreenBounds;

		public static float LimitScreenHeight => _LimitScreenHeight;
		private static float _LimitScreenHeight;

		[Header("GAME PREFABS")]
		[SerializeField] private Centipede m_CentipedePrefab;
		[SerializeField] private Mushroom m_MushroomPrefab;

		[Header("GAME CONFIGURATION")]
		[SerializeField] private int m_PlayerLive = 3;
		[SerializeField] private int m_CentipedeLength = 15;
		[SerializeField] private int m_MushroomMax = 35;
		[SerializeField] private int m_CentipedeScore = 100;

		[Header("GAME UI")]
		[SerializeField] private Text m_ScoreText;

		public int Score => _Score;

		public int CentipedeLength => m_CentipedeLength;

		private int _Score;

		protected override void Awake()
		{
			base.Awake();

			_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
			_LimitScreenHeight = GameManager.ScreenBounds.y * _LIMIT_PERCENTAGE;
		}

		void Start()
		{
			UpdateScore(0);
			CreateCentipede();
			CreateMushroom();
		}

		public void UpdateScore() => UpdateScore(m_CentipedeScore);

		public void UpdateScore(int _score)
		{
			_Score += _score;
			m_ScoreText.text = $"Score: {_Score}";
		}

		// private List<Centipede> _CentipedeList = new List<Centipede>();
		private void CreateCentipede()
		{
			var pos = Vector2.zero;
			for (int i = 0; i < CentipedeLength; i++)
			{
				pos = new Vector2(i, GridManager.Instance.GetTopLeftGridPosition().y);
				var centipede = SpawnUnitObjectToGrid(m_CentipedePrefab, pos) as Centipede;
				centipede.SetOrder(i);
				// _CentipedeList.Add(centipede);
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

		// public void UpdateCentipede(int _order)
		// {
		// 	for (int i = 0; i < _CentipedeList.Count; i++)
		// 	{
		// 		if (i < _order)
		// 		{
		// 			_CentipedeList[i].ToggleDirection();
		// 		}
		// 	}
		// }
	}
}