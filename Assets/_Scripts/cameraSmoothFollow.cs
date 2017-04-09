using UnityEngine;
using System.Collections;

public class cameraSmoothFollow : MonoBehaviour {

public float distance = 3.0f;
public float height = 3.0f;
public float damping = 5.0f;
public bool smoothRotation = true;
public float rotationDamping = 10.0f;
public Transform target;
public GameObject player;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void lateUpdate () 
	{		
		player = GameObject.FindGameObjectWithTag ("Player");
		Transform target= player.transform;
		var wantedPosition = target.TransformPoint(0, height, -distance);
		transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
		if (smoothRotation) 
		{
			var wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
		}

		else 
			transform.LookAt (target,Vector3.up);
	}
	
}
