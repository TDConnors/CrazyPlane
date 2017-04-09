using UnityEngine;
using System.Collections;

public class planeExplosion : MonoBehaviour {

public float radius = 5.0f;
public float power = 10.0f;

	void Start () 
	{
		Vector3 explosionPos= transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
		Destroy (gameObject, 5);	
		foreach (Collider hit in colliders)
		{
			if (hit && hit.GetComponent<Rigidbody>())
				hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0f);
		}	
	}
}
