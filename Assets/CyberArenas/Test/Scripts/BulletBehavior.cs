using System;

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
		this.tsTransform.scale = TSVector.one*(FP)2f;
		tsRigidBody = GetComponent<TSRigidBody>();

		tsRigidBody.velocity = tsRigidBody.tsTransform.up * (FP)SpeedFactor;

		//TrueSyncManager.SyncedStartCoroutine (KillTimeOut());
	}


	public void OnSyncedTriggerEnter(TSCollision otherBody) {
		if (otherBody.gameObject.tag ==  "Player") {
			
			//score++;
			//UpdateScore();
			//otherBody.gameObject.SendMessage("GoalScored");
			//gameEndHandler.gameObject.SendMessage("GoalScored", this);
			TrueSyncManager.SyncedDestroy(this.gameObject);
		}
	}

	IEnumerator KillTimeOut()
	{
	//	yield return TrueSyncManager.

	//	TrueSyncManager.SyncedDestroy(this.gameObject);
		yield return null;
	}


}
