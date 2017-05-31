using UnityEngine;
using System.Collections;

public class DialRotate : MonoBehaviour {

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
			float turn = target.transform.eulerAngles.z;
			if (turn <0.0f)
				turn = 180.0f + turn;
			Vector3 newRotation = new Vector3(0,0, turn);
			this.transform.eulerAngles = newRotation;
		}
		else
		{
			target = GameObject.FindGameObjectWithTag ("MainCamera");
		}
	}
}
