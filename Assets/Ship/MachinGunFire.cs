using UnityEngine;
using System.Collections;

public class MachinGunFire : MonoBehaviour {

  public float life = 3.0f;
  public int damage;
  public GameObject sparks;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

    life -= Time.deltaTime;
    if (life <= 0)
    {
      if (Network.isServer)
        Network.Destroy(this.gameObject);
    }
	}

  void OnCollisionEnter(Collision collision)
  {
    if (Network.isServer)
    {
      ContactPoint contact = collision.contacts[0];
      collision.collider.transform.gameObject.SendMessage("makeDamage", damage, SendMessageOptions.DontRequireReceiver);
      Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
      Vector3 pos = contact.point;
      Network.Instantiate(sparks, pos, rot, 0);
      Network.Destroy(this.gameObject);
    }
  }
}
