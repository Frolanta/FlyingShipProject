using UnityEngine;
using System.Collections;

public class shipNetworkSync : MonoBehaviour {

  private float lastSynchronizationTime = 0f;
  private float syncDelay = 0f;
  private float syncTime = 0f;
  private Vector3 syncStartPosition = Vector3.zero;
  private Vector3 syncEndPosition = Vector3.zero;
  private Quaternion syncStartRotation = Quaternion.identity;
  private Quaternion syncEndRotation = Quaternion.identity;
	// Use this for initialization
	void Start () {

	}

  void Update()
  {
    if (!networkView.isMine)
        SyncedMovement();
  }

  private void SyncedMovement()
  {
      syncTime += Time.deltaTime;
      rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
      rigidbody.rotation = Quaternion.Lerp(syncStartRotation, syncEndRotation, syncTime / syncDelay);
  }

  void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
  {
      Vector3 syncPosition = Vector3.zero;
      Vector3 syncVelocity = Vector3.zero;
      Quaternion syncRotation = Quaternion.identity;
      if (stream.isWriting)
      {
          syncPosition = rigidbody.position;
          syncRotation = rigidbody.rotation;
          syncVelocity = rigidbody.velocity; //to predict movement
          stream.Serialize(ref syncPosition);
          stream.Serialize(ref syncRotation);
          stream.Serialize(ref syncVelocity);
      }
      else
      {
          stream.Serialize(ref syncPosition);
          stream.Serialize(ref syncRotation);
          stream.Serialize(ref syncVelocity);
          syncTime = 0f;
          syncDelay = Time.time - lastSynchronizationTime;
          lastSynchronizationTime = Time.time;

          syncStartPosition = rigidbody.position;
          syncStartRotation = rigidbody.rotation;
          syncEndPosition = syncPosition + syncVelocity * syncDelay;
          syncEndRotation = syncRotation;
      }
  }
}
