using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
  private string entId;
	void Start ()
  {
      entId = gameObject.networkView.viewID.ToString().Remove(0,13);
      if(networkView.isMine) networkView.RPC("ChangeName",RPCMode.AllBuffered, entId);
	}

	// Update is called once per frame
  [RPC]
	void changeName (string ent)
  {

	}
}
