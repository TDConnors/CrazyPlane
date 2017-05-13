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
			Vector3 newRotation = new Vector3(0,0, target.transform.eulerAngles.z);
			this.transform.eulerAngles = newRotation;
		}
		else
		{
			target = GameObject.FindGameObjectWithTag ("Plane");
		}
	}
}
