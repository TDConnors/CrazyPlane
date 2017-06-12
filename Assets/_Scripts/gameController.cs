using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Diagnostics;
 
 
public class GameController : MonoBehaviour {

public float timeLimit;
public int score;
public Text ScoreText;
public Text text;
public Text text2;
public Text time;
public Transform player;
public Vector3 startPos;
private bool createdPlayer;
private bool gameend;
private bool playerDead;
private bool timerOn;
private bool killPlayer;
private float myTime;
 
	// Use this for initialization
	void Start () 
	{
		score = 0;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		text.gameObject.SetActive(true);
		text2.gameObject.SetActive(true);
		createdPlayer = false;
		gameend = false;
		playerDead = false;
		killPlayer = false;
		timerOn = true;

	}
	//	* 100.0f) / 100.0f)
	// Update is called once per frame
	void Update () 
	{
	if(timerOn)
		myTime += Time.deltaTime;
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
				if (myTime < timeLimit)
				{			
					time.text = "Time: " + (timeLimit - myTime ).ToString("F2");
				}
		
				if (myTime >= timeLimit) //only happens once
				{
					if (killPlayer == false)
					{
					    timerOn = false;
						time.text = "Time: 0";
						killPlayer = true;
						GameObject playerObject = GameObject.FindWithTag ("Player");
						playerObject.BroadcastMessage("BlowUp");
					}
				}
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
    timerOn = false;
	myTime = 0.0f;
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
			myTime = 0;
	}	
	public void pauseTimer()
	{
		timerOn = false;
	}
	public void resumeTimer()
	{
		timerOn = true;
	}
}
