using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Diagnostics;
 
 
public class GameController : MonoBehaviour {


public Stopwatch timer;
public float timeLimit;
public int score;
public Text ScoreText;
public Text text;
public Text text2;
public Text time;
public Transform player;
public Vector3 startPos;
private bool createdPlayer;
private bool CursorLockedVar;
private bool gameend;
private bool playerDead;
private bool killPlayer;

 
	// Use this for initialization
	void Start () 
	{
		score = 0;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		text.gameObject.SetActive(true);
		text2.gameObject.SetActive(true);
		CursorLockedVar = true;
		createdPlayer = false;
		gameend = false;
		playerDead = false;
		killPlayer = false;
		timer = new Stopwatch();

	}
	//	* 100.0f) / 100.0f)
	// Update is called once per frame
	void Update () 
	{
		if (!gameend)
		{	
			if (!createdPlayer || playerDead)
			{
				text.gameObject.SetActive(true);
				text2.gameObject.SetActive(true);
				if(Input.anyKeyDown)
				{
					onStart();
				}
			}
			else
			{
				if (timer.Elapsed.Seconds < timeLimit)
				{			
					time.text = "Time: " + (timeLimit - timer.Elapsed.Seconds ).ToString();
				}
		
				if (timer.Elapsed.Seconds >= timeLimit) //only happens once
				{
					if (killPlayer == false)
					{
						timer.Stop();
						time.text = "Time: 0";
						killPlayer = true;
						GameObject playerObject = GameObject.FindWithTag ("Player");
						playerObject.BroadcastMessage("BlowUp");
					}
				}
			}
			if (Input.GetKeyDown ("escape") && !CursorLockedVar) 
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = (false);
				CursorLockedVar = (true);
			}
			else if(Input.GetKeyDown("escape") && CursorLockedVar)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = (true);
				CursorLockedVar = (false);
			}
		}
		else
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
	
	void GameOver()
	{
	print(score);
	text.text = "Game Over \n your score is : " + score +"\n \n Press 'R' to Restart";
	text.gameObject.SetActive(true);
	text2.text = "Game Over \n your score is : " + score +"\n \n Press 'R' to Restart";
	text2.gameObject.SetActive(true);
	playerDead = true;
	gameend = true;
	}
	public void AddScore (int newScoreValue)
	{
		if(gameend == false)
		{
			score += newScoreValue;
			ScoreText.text =  "Score: " + score.ToString();
		}
	}
	public bool isOver()
	{
		return gameend;
	}
	public bool isCreated()
	{
		return createdPlayer;
	}
	public void onStart()
	{
			Instantiate(player, startPos, Quaternion.identity);
			text.gameObject.SetActive(false);
			text2.gameObject.SetActive(false);
			createdPlayer = true;
			playerDead = false;
			timer.Start();
	}	
}
