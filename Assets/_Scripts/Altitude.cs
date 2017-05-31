using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Altitude : MonoBehaviour {

	
	public Text AltText;
	private GameObject target;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{ 	
		if (target != null)
		{
			AltText.text =  "Alt: " + (target.transform.position.y + 300);
		}
		else
		{
			target = GameObject.FindGameObjectWithTag ("Player");
		}
	}
}
