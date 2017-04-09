using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public float lifetime;
	public int scoreValue;
	public GameObject gameController;

	void Start ()
	{
		//Destroy (gameObject, lifetime);
	}

	void OnTriggerEnter (Collider other)
	{
		gameController.GetComponent<gameController>().AddScore(scoreValue);
		Destroy (gameObject);
	}
}