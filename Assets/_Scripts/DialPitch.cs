using UnityEngine;
using System.Collections;

public class DialPitch : MonoBehaviour {

	private GameObject target;
	private float pitch;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{ 	
	if (target != null)
		{

		pitch = target.transform.eulerAngles.z;
		//pitch = 0 - pitch;// inverts the pitch 

        Vector3 newRotation = new Vector3(0, pitch, 0); //translate the new x rotation to y movement
        this.transform.localPosition = newRotation; // move the gauge up and down
		}
		else
		{
			target = GameObject.FindGameObjectWithTag ("Plane");
		}
	}
}
