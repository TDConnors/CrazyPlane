using UnityEngine;
using System.Collections;

public class SPARTA_PLANE_SCRIPT : MonoBehaviour {
	
	//OBJECTS TO GET REGARDING THE SPARTA AIRPLANE
	private GameObject spartacannon1, spartacannon2,  plane_camera_parent;
	private Animator spartacannonanim1, spartacannonanim2, anim;
	private AudioSource plane_audio;
	//CONTROLLER VARIABLES
	private bool  cannonReady, cannonOpen;
	private float  roll_speed, pitch_speed, yaw_speed, gyrotiming, exaustfloat, delaytime_forcannonweapondoor,
	gyroX, gyroY, verticalaxis_pos, horizontalaxis_pos;
	
	//My VARIABLES
	public bool TiltToggle;
	public float addSpeed, max_speed, maxVelocityChange, pitchFactor, plane_pitch_volume, rollFactor, speed, yawFactor, YawTurnSpeed;
	public Transform boostParticle;
	public Transform explosion;
	public Transform planeMesh;
	private GameController gameController;
	private Vector3 forward;	
	private Vector3 velocityChange;
   

	//initialization
	void Start () 
	{
		spartacannon1 = GameObject.Find ("SPARTA CANNON ANIMATED");
		spartacannon2 = GameObject.Find ("SPARTA CANNON ANIMATED (1)");
		anim = gameObject.GetComponentInChildren<Animator> ();
		spartacannonanim1 = spartacannon1.GetComponentInParent<Animator> ();
		spartacannonanim2 = spartacannon2.GetComponentInParent<Animator> ();
		plane_audio = gameObject.GetComponent<AudioSource> ();
		
		
		roll_speed = 0.0f;
		pitch_speed = 0.0f;
		yaw_speed = 0.0f;
		gyrotiming = 0;
		exaustfloat = 0.0f;
		delaytime_forcannonweapondoor = 0.0f;
		
		verticalaxis_pos = 0.0f; horizontalaxis_pos = 0.0f;

		cannonOpen = false;
		cannonReady = true;
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
	
	void FixedUpdate () 
	{
		FlightController();
	}
	
	void FlightController()
	{	
		//ROLL AND PITCH
		rotationUpdate();
		//GYRO
		if (gyrotiming > 0.05f) 
		{
			gyroUpdate();
		}
		//WEAPONS
		weaponDoorUpdate();
		//YAW
		yawUpdate();
		// EXAUST
		exhaustUpdate();
		//PLANE SPEED
		planeSpeedUpdate();
		//MOVE PLANE
		flightUpdate();
		//ENGINE SOUND
		engineSoundUpdate();
	}
	
	
	void yawUpdate()
	{
		
		YawTurnSpeed = Input.GetAxis ("HorizontalX");
		
		if (YawTurnSpeed < 0.0f) 
		{
	        anim.SetBool ("SPARTA PLANE YAW RIGHT BOOL", false);	
			anim.SetBool ("SPARTA PLANE YAW LEFT BOOL", true);
			yaw_speed = -(YawTurnSpeed * yawFactor);
		} 
		else 
		{
			anim.SetBool ("SPARTA PLANE YAW RIGHT BOOL", true);
			anim.SetBool ("SPARTA PLANE YAW LEFT BOOL", false);
			yaw_speed = -(YawTurnSpeed * yawFactor);
		}
		if (YawTurnSpeed == 0.0f)
		{
			yaw_speed = 0.0f;
		}
	}
	
	void rotationUpdate()
	{
		transform.Rotate (roll_speed, yaw_speed, pitch_speed);
		
		if(TiltToggle == true) //Use Tilt
		{
			horizontalaxis_pos = Input.GetAxis ("SideTilt");
			verticalaxis_pos = Input.GetAxis ("FrontTilt");
		}
		else //Use joystick
		{
			horizontalaxis_pos = Input.GetAxis ("ShoulderAxis");//reference
			verticalaxis_pos = Input.GetAxis ("VerticalY");//reference
		}
		
		
		gyroY = horizontalaxis_pos;//this controls animation of rolling wing flaps
		gyroX = verticalaxis_pos;  //		||
		roll_speed = horizontalaxis_pos * rollFactor;//this controls the plane rotation ingame
		pitch_speed = verticalaxis_pos * pitchFactor;//		||
		gyrotiming += 1 * Time.deltaTime;//<- THIS IS TO CONTROL SURFACES FLICKERING BY SMOOTHING THE MOVEMENTS.	
	}
	
	void gyroUpdate()
	{
		anim.SetFloat ("SPARTA PLANE PITCH FLOAT", gyroX);
		anim.SetFloat ("SPARTA PLANE ROLL FLOAT", gyroY);
		
		//THIS IS TO CONTROL FLAPS DOWN IF THE AIRCRAFT IS GOING SLOW IE WHEN ITS GOING TO LAND OR TO PROMOTE LIFT
		//ends here
		//THIS IS TO PREVENT INPUT FROM GOING ABOVE 1
		if (gyroX > 1) {
			gyroX = 1;
		}
		if (gyroX < -1) {
			gyroX = -1;
		}
		if (gyroY > 1) {
			gyroY = 1;
		}
		if (gyroY < -1) {
			gyroY = -1;
		}
		gyrotiming = 0;
	}
	
	void weaponDoorUpdate()
	{
		
		if ((Input.GetButtonDown ("Triangle Button"))&&(cannonOpen == false)&&(cannonReady)) {//this simulate tapping when weapons door are closed
			cannonOpen = true;
			cannonReady = false;
		} 
		if ((Input.GetButtonDown ("Triangle Button"))&&(cannonOpen == true)&&(cannonReady)) {//this simulate tapping when weapons door are opened
			cannonOpen = false;
			cannonReady = false;
		}
		if (!Input.GetButton ("Triangle Button")) {
			cannonReady = true;
		}
		delaytime_forcannonweapondoor -= 0.1f;
		if ((Input.GetButton("Triangle Button"))&&(cannonOpen == true)) {//THIS OPEN WEAPONS DOOR
			anim.SetBool ("SPARTA PLANE WEAPONS DOOR BOOL", true);
			spartacannonanim1.SetBool ("SPARTA CANNON BOOL", true);
			spartacannonanim2.SetBool ("SPARTA CANNON BOOL", true);
			delaytime_forcannonweapondoor = 0.5F;
		}
		if ((Input.GetButton("Triangle Button"))&&(cannonOpen == false)) {//THIS CLOSE WEAPONS DOOR
			
			if (delaytime_forcannonweapondoor < 0.0F) {
				anim.SetBool ("SPARTA PLANE WEAPONS DOOR BOOL", false);
				spartacannonanim1.SetBool ("SPARTA CANNON BOOL", false);
				spartacannonanim2.SetBool ("SPARTA CANNON BOOL", false);
				delaytime_forcannonweapondoor = 0;
			}
		}
	}
	

	void exhaustUpdate()
	{
		//this controls the exaust by using the "exaustfloat" timing
		if (Input.GetButton ("Cross Button")) 
		{
			boostParticle.gameObject.SetActive(true);
			if (exaustfloat < 2.0f) 
			{
				exaustfloat += 0.1f;
			}
		}
		else 
		{
			boostParticle.gameObject.SetActive(false);
			if (exaustfloat > 0.1f) 
			{
				exaustfloat -= 0.2f;
			}
		}
		
		if (exaustfloat > 1.3f) {
			anim.SetBool ("SPARTA PLANE EXAUST BOOL", true);
		}
		if (exaustfloat < 1.3f) {
			anim.SetBool ("SPARTA PLANE EXAUST BOOL", false);
		}
	}
	
	void planeSpeedUpdate()
	{	
		if (addSpeed > max_speed) {//this prevent/controls the plane from going beyond set max speed
			addSpeed = max_speed;
		}
		
		
		if ((Input.GetButton ("Cross Button")) && (addSpeed < max_speed)) {//forward
			
			addSpeed += 0.5f;
		}
		
		//bring back to normal
		if  (!Input.GetButton ("Cross Button")) {
			if (addSpeed < 0.0f) {
				addSpeed = 0.0f;
			}
			if (addSpeed > 0.0f) {
				addSpeed -= 0.5f;
			}
			
		}
	}
	
	void flightUpdate()
	{
	    forward = transform.TransformDirection(Vector3.left);
		
        var targetDirection = forward * (speed + addSpeed);
		
        var targetVelocity = targetDirection;
		
        var velocity = GetComponent<Rigidbody>().velocity;
		
        velocityChange = (targetVelocity - velocity);
		
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
        GetComponent< Rigidbody > ().AddForce(velocityChange, ForceMode.VelocityChange);	
	}	
	
	void engineSoundUpdate()
	{
		plane_audio.pitch = (addSpeed/100) + 1.0f;
		plane_audio.volume = plane_pitch_volume;
	}
	
	
	
	
	void OnCollisionEnter(Collision other)
	{
		BlowUp();	
	}
	
	public void BlowUp()
	{
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, planeMesh.position);
		Vector3 pos = planeMesh.position;
		Instantiate(explosion, pos, rot);
		var gamecontroller = GameObject.FindWithTag ("GameController");
		gamecontroller.BroadcastMessage("GameOver");
		Destroy(gameObject);
	}
}
