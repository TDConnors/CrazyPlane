﻿using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour 
{

	public float spawnRate;
    public GameObject[] target;
	public Transform ballSpawn;
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	public float zMin;
	public float zMax;
	
	private float x;
    private float y;
    private float z;
	private float nextMove;
	private bool firstStart;
	private GameController gameController;
	
	void Start ()
	{
		firstStart = false;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	int randomWeighter()
	{
		int num = Random.Range(0, 12);
		if (num <= 5) 
			return 0; //25 points
		else if (num <= 9)
			return 1; //50 points
		else
			return 2; //100 points
	}
	// Update is called once per frame
	void Update () 
	{
		if (gameController.isCreated())
		{
			if (firstStart == false)
			{
				nextMove = Time.time + 2.0f;	
				firstStart = true;
			}
			
			if (Time.time > nextMove && gameController.isOver()== false )
			{
				nextMove = Time.time + spawnRate;
				x = Random.Range(xMin, xMax);
				y = Random.Range(yMin, yMax);
				z = Random.Range(zMin, zMax);
				Vector3 movement = new Vector3 (x, y, z);
				ballSpawn.position = movement;
				int temp = randomWeighter();
				Instantiate(target[temp], ballSpawn.position, ballSpawn.rotation);
			}
		}
	}
}
