using UnityEngine;
using System.Collections;

public class RandomCloud : MonoBehaviour {


	public Material[] materials;
	public GameObject sphere;
	public Renderer rend;
	// Use this for initialization
	void Start () 
	{
		rend = sphere.GetComponent<Renderer>();
        rend.enabled = true;
		int value = Random.Range (0, materials.Length);
		rend.sharedMaterial = materials[value];
	}
	

}
