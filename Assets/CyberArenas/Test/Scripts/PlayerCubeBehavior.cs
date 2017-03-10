using UnityEngine;
using TrueSync;

/// <summary>
/// Player cube behavior.
/// TODO: fix fire rate
/// TODO: implement bullet trigger to affect health or kill player
/// </summary>
public class PlayerCubeBehavior : TrueSyncBehaviour {


    /**
    * @brief Key to set/get player's movement from {@link TrueSyncInput}.
    **/
	private const byte INPUT_KEY_MOVE_HORIZONTAL = 0;

	/**
    * @brief Key to set/get player's movement from {@link TrueSyncInput}.
    **/
	private const byte INPUT_KEY_MOVE_VERTICAL = 1;

    /**
    * @brief Key to set/get player's jump from {@link TrueSyncInput}.
    **/
    private const byte INPUT_KEY_JUMP = 2;

	/**
    * @brief Key to set/get player's Fire from {@link TrueSyncInput}.
    **/
	private const byte INPUT_KEY_FIRE = 3;

	public GameObject BulletPrefab;

    /**
    * @brief Player's movement speed.
    **/
    //public int SpeedFactor = 5;
	public int AngularVelocityFactor = 5;

	/// <summary>
	/// The last fire frame.
	/// </summary>
	int lastFireFrame;

	/**
	 * Added by Aaron to reuse code
	 * Resets position and stops motion 
	 **/
	private void ResetPosition () {
		// Sets sprite and animator controller based on player's id
		if (owner.Id == 1) {

			tsRigidBody.position = new TSVector(1,0,0);
		} else {

			tsRigidBody.position = new TSVector(-1, 0,0);
		}
		TSVector velocity = tsRigidBody.velocity;
		velocity.x = velocity.y = (FP)0;
		tsRigidBody.velocity = velocity;
	}

	/// <summary>
	/// The force.
	/// </summary>
	public int ForceFactor = 5;

    /**
    * @brief Controlled {@link TSRigidBody} of the player.
    **/
	TSRigidBody tsRigidBody;

    /**
    * @brief Initial setup when game is started.
    **/
    public override void OnSyncedStart () {
		tsRigidBody = GetComponent<TSRigidBody>();
		ResetPosition ();
    }
		
    /**
    * @brief Sets player inputs.
    **/
    public override void OnSyncedInput () {
		int horizontal_input = (int)(Input.GetAxis("Horizontal") * 100);
		TrueSyncInput.SetInt(INPUT_KEY_MOVE_HORIZONTAL, horizontal_input);

		int vertical_input = (int)(Input.GetAxis("Vertical") * 100);
		TrueSyncInput.SetInt(INPUT_KEY_MOVE_VERTICAL, vertical_input);

		TrueSyncInput.SetByte(INPUT_KEY_JUMP,Input.GetButtonDown("Jump")? (byte)1 : (byte)0);

		TrueSyncInput.SetByte(INPUT_KEY_FIRE,Input.GetButtonDown("Fire1")? (byte)1 : (byte)0);

	}

    /**
    * @brief Updates player and movements.
    **/
    public override void OnSyncedUpdate () {

		// Rotate around Z
		// Set a velocity based on player's speed and inputs
		TSVector angularVelocity = tsRigidBody.angularVelocity;
		angularVelocity.z = -(TrueSyncInput.GetInt(INPUT_KEY_MOVE_HORIZONTAL) * AngularVelocityFactor / (FP) 100);
		tsRigidBody.angularVelocity = angularVelocity;

		//// Vertical Movement
		FP horizontal_input = TrueSyncInput.GetInt(INPUT_KEY_MOVE_VERTICAL) / (FP) 100;
		FP absoluteForce = horizontal_input * (FP)ForceFactor;
		tsRigidBody.AddForce(tsRigidBody.tsTransform.up * absoluteForce);
			
		if (TrueSyncInput.GetByte (INPUT_KEY_FIRE) == (byte)1) {
			Fire ();
		}


		if (TrueSyncInput.GetByte (INPUT_KEY_JUMP) == (byte)1) {
			ResetPosition ();
		}


	}

	void Fire()
	{
		if (lastFireFrame != Time.frameCount) {
			TrueSyncManager.SyncedInstantiate (this.BulletPrefab, tsTransform.position + tsTransform.up * (FP)3f, tsTransform.rotation);
			lastFireFrame = Time.frameCount;
		}

	}

		
}