using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TrueSync;

/// <summary>
/// Bullet behavior.
/// TODO: implement timeout to kill bullet
/// </summary>
public class BulletBehavior : TrueSyncBehaviour {


	public float SpeedFactor = 5f;

	/**
    * @brief Controlled {@link TSRigidBody} of the player.
    **/
	TSRigidBody tsRigidBody;

	/**
    * @brief Initial setup when game is started.
    **/
	public override void OnSyncedStart () {
		tsRigidBody = GetComponent<TSRigidBody>();

		tsRigidBody.velocity = tsRigidBody.tsTransform.up * (FP)SpeedFactor;
	}



}
