using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float speed;
	private Transform targetParent;
	
	void Start()
	{	
		GameObject spawnerObject = GameObject.FindGameObjectWithTag ("TargetParent");
		if (spawnerObject != null)
		{
			targetParent = spawnerObject.transform;
		}
		if (targetParent == null)
		{
			Debug.Log ("Cannot find Spawner");
		}
		transform.parent = targetParent;
	}
    void Update () 
    {
        transform.Rotate (new Vector3 (15, 30, 45) * (speed * Time.deltaTime));
    }
}