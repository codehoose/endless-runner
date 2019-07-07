using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private static GameController _instance;

	public static GameController Instance
	{
		get
		{ 
			if (_instance != null)
			{
				return _instance;
			}
			
			_instance = FindObjectOfType<GameController>();
			if (_instance != null)
				return _instance;

			var go = new GameObject("GameController");
			_instance = go.AddComponent<GameController>();
			return _instance;
		}
	}

	private float seconds;

    public event EventHandler GameStarted;

    public event EventHandler GameEnded;

	public CanvasGroup canvasGroup;
	public GameObject gameOverText;

	public TMPro.TextMeshProUGUI score;

	public void StartGame()
	{
		Input.ResetInputAxes();
		IsRunning = true;
		canvasGroup.alpha = 0f;
		seconds = 0;
		score.text = "0";
		gameOverText.SetActive(false);
        
        if (GameStarted != null)
        {
            GameStarted(this, EventArgs.Empty);
        }
	}

	public void PickUpCoin()
	{
		seconds += 5;
	}

	public void GameOver()
	{
		gameOverText.SetActive(true);
		canvasGroup.alpha = 1f;
		IsRunning = false;

        if (GameEnded != null)
        {
            GameEnded(this, EventArgs.Empty);
        }
	}

	public bool IsRunning
	{
		get;
		private set;
	}

	void Update()
	{
		if (IsRunning)
		{
			seconds += Time.deltaTime;
			score.text = string.Format("{0:0}", seconds);			
			return;
		}

		if (Input.GetButton("Fire1"))
		{
			StartGame();
		}
	}
}
