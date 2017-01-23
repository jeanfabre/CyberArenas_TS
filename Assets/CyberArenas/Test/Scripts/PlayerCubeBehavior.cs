using UnityEngine;
using TrueSync;


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
    * @brief Player's movement speed.
    **/
    public int SpeedFactor = 5;

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


        // Sets sprite and animator controller based on player's id
		if (owner.Id == 1) {
			
			tsRigidBody.position = new TSVector(1,0,0);
		} else {
	
			tsRigidBody.position = new TSVector(-1, 0,0);
		}
			
    }
		
    /**
    * @brief Sets player inputs.
    **/
    public override void OnSyncedInput () {
		int horizontal_input = (int)(Input.GetAxis("Horizontal") * 100);
		TrueSyncInput.SetInt(INPUT_KEY_MOVE_HORIZONTAL, horizontal_input);

		int vertical_input = (int)(Input.GetAxis("Vertical") * 100);
		TrueSyncInput.SetInt(INPUT_KEY_MOVE_VERTICAL, vertical_input);

	}

    /**
    * @brief Updates player and movements.
    **/
    public override void OnSyncedUpdate () {


		// Horozontal Movement
        // Set a velocity based on player's speed and inputs
		TSVector velocity = tsRigidBody.velocity;
		velocity.x = TrueSyncInput.GetInt(INPUT_KEY_MOVE_HORIZONTAL) * SpeedFactor / (FP) 100;

		// do lerping

        // Assigns this velocity as new player's linear velocity
		tsRigidBody.velocity = velocity;

		// Vertical Movement
		FP horizontal_input = TrueSyncInput.GetInt(INPUT_KEY_MOVE_VERTICAL) / (FP) 100;


		TSVector appliedForce = new TSVector (FP.Zero, horizontal_input * (FP)ForceFacter, FP.Zero);

		tsRigidBody.AddForce(appliedForce);

	}
		
}