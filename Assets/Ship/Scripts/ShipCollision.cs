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
    //check if collision != fire
    /*float[] array = { Mathf.Abs(this.rigidbody.velocity.x), Mathf.Abs(this.rigidbody.velocity.y), Mathf.Abs(this.rigidbody.velocity.z) };
    float dmg = Mathf.Max(array);
    this.gameObject.SendMessage("makeDamage", dmg / 4, SendMessageOptions.DontRequireReceiver);*/
    //ContactPoint contact = collision.contacts[0];
    //this.rigidbody.AddForceAtPosition(new Vector3(1000,1000,1000), contact.point);
  }
}
