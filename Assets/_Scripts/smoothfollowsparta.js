private var target : Transform;
private var player : GameObject;
var distance = 3.0;
var height = 3.0;
var damping = 5.0;
var smoothRotation = true;
var rotationDamping = 10.0;
 
private var revertFogState = false;
 
function OnPreRender () 
{
	RenderSettings.fog = false;
}
 
function OnPostRender () {
	RenderSettings.fog = revertFogState;
}

function Start()
{
	
}

function FixedUpdate () 
{
	if (player == null)
	{
		player = GameObject.FindWithTag ("Player");
	}
	else
	{
		target = player.transform;
	var wantedPosition = target.TransformPoint(distance, height, 0);
	transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

	if (smoothRotation) {
		var wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
	}
	else transform.LookAt (target,Vector3.up);
	}
	
}
 
@script AddComponentMenu ("Rendering/Fog Layer")
@script RequireComponent (Camera)