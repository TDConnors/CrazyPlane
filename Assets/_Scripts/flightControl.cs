using UnityEngine;
using System.Collections;

public class flightControl : MonoBehaviour {


public AudioSource myAudio;
public float dampTime ;
public float maxVelocityChange;
public float rotateSpeed;
public float sensitivity;
public float smoothSpeed;
public float speed;
public LayerMask mask;
public Transform boostParticle;
public Transform explosion;
public Transform pieces;
public Transform planeMesh;
public Vector3 velocityChange;
private float addSpeed;
private float boost;
private float meshTurn;
private float rotationX = 0f;
private float rotationY = 0f;
private float sensitivityX ;
private GameController gameController;
private Quaternion meshRotation;
private Quaternion originalMeshRotation;
private Vector3 forward = Vector3.forward;
private Vector3 moveDirection = Vector3.zero;
private Vector3 right;


	void Awake ()
	{
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Rigidbody>().useGravity = false;
	}
	// Use this for initialization
	void Start () 
	{	
		originalMeshRotation = planeMesh.localRotation;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        forward = transform.TransformDirection(Vector3.forward);

        var right = new Vector3(forward.z, 0, -forward.x);
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        if (Input.GetButton("Jump"))
        {
            addSpeed = boost;
            boostParticle.gameObject.SetActive(true);
        }
        else
        {
            addSpeed = 0;
            boostParticle.gameObject.SetActive(false);
        }
        ver = Mathf.Clamp(ver, 0, 1);

        var targetDirection = forward * (speed + addSpeed);



        var targetVelocity = targetDirection;

        var velocity = GetComponent<Rigidbody>().velocity;

        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationX += Input.GetAxis("Mouse X") * sensitivity;


        var myQuaternion = Quaternion.Euler(-rotationY, rotationX, 0);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, myQuaternion, Time.deltaTime * smoothSpeed);
        if (Input.GetAxis("Mouse X") != 0)
        {
            meshTurn += Input.GetAxis("Mouse X") * sensitivity;
            var meshQuaternion = Quaternion.Euler(0, 0, -meshTurn);
            planeMesh.transform.localRotation = Quaternion.Slerp(planeMesh.transform.localRotation, meshQuaternion, Time.deltaTime * 2);
        }
        else
        {
            meshTurn = 0;
            planeMesh.transform.localRotation = Quaternion.Slerp(planeMesh.transform.localRotation, originalMeshRotation, Time.deltaTime * 2);
        }

        var currentpitchx = Mathf.Abs(Input.GetAxis("Mouse X"));
        var currentpitchy = Mathf.Abs(Input.GetAxis("Mouse Y"));
		float tempChange = (float)(0.9 + (addSpeed / 28) + ((currentpitchx * 1.8) + (currentpitchy * 1.6)));
        myAudio.pitch = Mathf.Lerp(myAudio.pitch, tempChange, Time.deltaTime * 2);


        velocityChange = (targetVelocity - velocity);

        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
        GetComponent< Rigidbody > ().AddForce(velocityChange, ForceMode.VelocityChange);

    }

    void OnCollisionEnter (Collision other)
	{
		ContactPoint contact = other.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		Instantiate(explosion,pos,rot);
		Instantiate(pieces,pos,rot);
		gameController.BroadcastMessage("GameOver");
		Destroy(gameObject);	
	}
	
}
