using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {


public Transform text;
public Text ScoreText;
public Transform player;
public Vector3 startPos;
public int score;
private bool createdPlayer = false;
private bool playerDead = false;
private bool CursorLockedVar = true;
private bool gameend = false;
 
	// Use this for initialization
	void Start () 
	{
		score = 0;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		text.gameObject.SetActive(true);
		CursorLockedVar = true;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!createdPlayer || playerDead)
		{
			text.gameObject.SetActive(true);
			if(Input.anyKeyDown)
			{
				StartCoroutine(onStart());
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
		ScoreText.text =  "Score: " + score.ToString();
	}
	
	void GameOver()
	{
	print(score);
	playerDead = true;
	gameend = true;
	}
	public void AddScore (int newScoreValue)
	{
		if(gameend == false)
		{
			score += newScoreValue;
		}
	}
	public bool isOver()
	{
		return gameend;
	}
	IEnumerator onStart()
	{
		yield return new WaitForSeconds(0.8f);
		if (!createdPlayer || playerDead)
		{
			Instantiate(player, startPos, Quaternion.identity);
			text.gameObject.SetActive(false);
			createdPlayer = true;
			playerDead = false;
		}
	}	
}
