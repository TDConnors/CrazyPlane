using UnityEngine;
using System.Collections;

public class OrbitMove : MonoBehaviour {

    public float orbitSpeed;
	public Transform originPoint;
	private Vector3 axe;
	
	void Start ()
	{
		axe = new Vector3(0, 0, 30.0f);
	}
	
	void Update () 
	{
	transform.RotateAround (originPoint.position, axe, orbitSpeed * Time.deltaTime);
	}
}
