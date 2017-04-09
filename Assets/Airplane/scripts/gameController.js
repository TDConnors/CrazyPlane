#pragma strict
var text : Transform;
var player : Transform;
var startpos : Vector3;
private var createdplayer : boolean = false;
private var playerdead : boolean = false;
private var CursorLockedVar : boolean = true;
function Start () 
{
	Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = (false);
 	text.gameObject.SetActive(true);
	CursorLockedVar = true;

}

function Update () 
{
	if (!createdplayer || playerdead)
	{
		text.gameObject.SetActive(true);
		if(Input.anyKeyDown)
		{
			onstart();
		}
	}
	if (Input.GetKeyDown ("escape") && !CursorLockedVar) 
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		CursorLockedVar = (true);
	}
	else if(Input.GetKeyDown("escape") && CursorLockedVar)
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = (true);
		CursorLockedVar = (false);
}
	
	
	
	
	

}
function GameOver()
{
	print("gameover");
	playerdead = true;
}

function onstart()
{
	yield WaitForSeconds(0.8);
	if (!createdplayer || playerdead)
	{
		Instantiate(player, startpos, Quaternion.identity);
		text.gameObject.SetActive(false);
		createdplayer = true;
		playerdead = false;
	}
}