using UnityEngine;
using System.Collections;

public class ShipCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  void OnCollisionEnter(Collision collision)
  {
    //ContactPoint contact = collision.contacts[0];
    //this.rigidbody.AddForceAtPosition(new Vector3(1000,1000,1000), contact.point);
  }
}
