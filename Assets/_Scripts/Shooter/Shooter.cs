using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	public float missleSpeed;
	public Rigidbody missle;
	public Transform  leftMuzzle, rightMuzzle, missleMuzzle;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Instantiate(missle, missleMuzzle.position, missleMuzzle.rotation);
	}
}
