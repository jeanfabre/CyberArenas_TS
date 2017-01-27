using UnityEngine;
using TrueSync;


public class PlayerCubeBehavior : TrueSyncBehaviour {

	public FP ZRot;
	public FP ZAbs;
	public FP ZCosRot;
	public FP ZSinRot;
	public FP ZCosRotAbs;
	public FP ZSinRotAbs;

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
    * @brief Player's movement speed.
    **/
    //public int SpeedFactor = 5;
	public int AngularVelocityFactor = 5;

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
	public int ForceFacter = 5;

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

		TrueSyncInput.SetByte(INPUT_KEY_JUMP,Input.GetButton("Jump")? (byte)1 : (byte)0);
	}

    /**
    * @brief Updates player and movements.
    **/
    public override void OnSyncedUpdate () {


		//// Horozontal Movement
        //// Set a velocity based on player's speed and inputs
		//TSVector velocity = tsRigidBody.velocity;
		//velocity.x = TrueSyncInput.GetInt(INPUT_KEY_MOVE_HORIZONTAL) * SpeedFactor / (FP) 100;
		//tsRigidBody.velocity = velocity;

		// Rotate around Z
		// Set a velocity based on player's speed and inputs
		TSVector angularVelocity = tsRigidBody.angularVelocity;
		angularVelocity.z = -(TrueSyncInput.GetInt(INPUT_KEY_MOVE_HORIZONTAL) * AngularVelocityFactor / (FP) 100);
		tsRigidBody.angularVelocity = angularVelocity;

		//// Vertical Movement
		FP horizontal_input = TrueSyncInput.GetInt(INPUT_KEY_MOVE_VERTICAL) / (FP) 100;
		//TSVector appliedForce = new TSVector (FP.Zero, horizontal_input * (FP)ForceFacter, FP.Zero);
		//tsRigidBody.AddForce(appliedForce);

		FP zRotation = tsRigidBody.rotation.z.AsFloat ();
		FP absoluteForce = horizontal_input * (FP)ForceFacter;
		TSVector appliedForce = new TSVector (FP.Cos (zRotation) * absoluteForce, FP.Sin (zRotation) * absoluteForce, FP.Zero);
		tsRigidBody.AddForce(appliedForce);
		ZRot = zRotation;
		ZAbs = absoluteForce;
		ZCosRot = FP.Cos (zRotation);
		ZSinRot = FP.Sin (zRotation);
		ZCosRotAbs = FP.Cos (zRotation) * absoluteForce;
		ZSinRotAbs = FP.Sin (zRotation) * absoluteForce;

		//string logString = new string (zRotation);
		//Debug.Log ("zRotation = " + logString);


		//TSVector forceVectororce = TSVector();


		if (TrueSyncInput.GetByte (INPUT_KEY_JUMP) == (byte)1) {
			ResetPosition ();
		}
	}
		
}