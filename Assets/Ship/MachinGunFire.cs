using UnityEngine;
using System.Collections;

public class MachinGunFire : MonoBehaviour {

  public float life = 3.0f;
  public GameObject sparks;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

    life -= Time.deltaTime;
    if (life <= 0)
      Destroy(this.gameObject);
	}

  void OnCollisionEnter(Collision collision) {
    ContactPoint contact = collision.contacts[0];
    Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    Vector3 pos = contact.point;
    Instantiate(sparks, pos, rot);
    Destroy(this.gameObject);
  }
}
