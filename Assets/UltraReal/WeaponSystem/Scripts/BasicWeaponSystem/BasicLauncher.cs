using UnityEngine;
using System.Collections;
using UltraReal.Utilities;
using UltraReal.WeaponSystem;

/// <summary> 
/// Basic launcher class.  This would be your gun script.
/// </summary>
public class BasicLauncher : UltraRealLauncherBase {

	/// <summary> 
	/// Time before the next shot can be fired.
	/// </summary>
	float _nextShotTime;

	/// <summary> 
	/// Time delay before next shot can be fired.  A machine gun would use a really small shot delay.
	/// </summary>
	[SerializeField] 
	private float _shotDelay = 0.1f;
	
	private bool _sideToggle = false;

	/// <summary> 
	/// Reference to the Transform for the shell ejector.
	/// </summary>
	[SerializeField]
	private Transform _ejectorTransform = null;

	/// <summary> 
	/// Reference to the Transform for the left muzzle position.
	/// </summary>
	[SerializeField]
	private Transform _leftMuzzleTransform = null;
	
	/// <summary> 
	/// Reference to the Transform for the right muzzle position.
	/// </summary>
	[SerializeField]
	private Transform _rightMuzzleTransform = null;

	/// <summary> 
	/// Force applied to ejected shells.
	/// </summary>
	[SerializeField]
	protected float _ejectorForce = 100f;

	/// <summary> 
	/// Torque applied to ejected shells.
	/// </summary>
	[SerializeField]
	protected float _ejectorTorque = 100f;

    /// <summary> 
    /// Reference to the AudioSource for the gun.  If it's null, it will create one.
    /// </summary>
    [SerializeField]
    protected AudioSource _audioSource = null;


	/// <summary> 
	///	Sets defaults for weapon
	/// </summary>
	protected override void OnStart ()
	{
		base.OnStart ();
		_nextShotTime = Time.time;
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
	}

	/// <summary> 
	///	Fires the weapon
	/// </summary>
	public override void Fire ()
	{
		if (_nextShotTime <= Time.time){
			
					if(_sideToggle)
					{
						_ammo.SpawnAmmo(_rightMuzzleTransform, _ejectorTransform, _ejectorForce, _ejectorTorque, 2f, _audioSource);
						_sideToggle = false;
					}
					else
					{
						_ammo.SpawnAmmo(_leftMuzzleTransform, _ejectorTransform, _ejectorForce, _ejectorTorque, 2f, _audioSource);
						_sideToggle = true;
					}
				base.Fire ();
				_nextShotTime = Time.time + _shotDelay;
		}
	}
}
