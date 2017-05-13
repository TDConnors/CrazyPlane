using UnityEngine;
using System.Collections;

public class DialPitch : MonoBehaviour {

	public Transform target;
	public float pitch;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{ 	
		if(target.transform.forward.y == 0)
			pitch = 0; 
		else
			pitch = ((Mathf.Asin(target.transform.forward.y)* 180) / Mathf.PI); //converts the radians to degrees
		
		pitch = 0 - pitch;// inverts the pitch 
        Vector3 newRotation = new Vector3(0,pitch,0); //translate the new x rotation to y movement
        this.transform.localPosition = newRotation; // move the gauge up and down

	}
}
