using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour {

	// Use this for initialization
  public GameObject machinGunPrefab;
  public float machinGunSpeed = 50;
  public float machinGunFireRate = 0.5f;
  public Transform startFire1;
  public Transform startFire2;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  public void startPrimaryFire()
  {
    InvokeRepeating("fireMachinGun", 0.05f, this.machinGunFireRate);
  }

  public void stopPrimaryFire()
  {
    CancelInvoke("fireMachinGun");
  }

  void fireMachinGun()
  {
    GameObject instantiatedProjectile = Instantiate(machinGunPrefab,startFire1.position, startFire1.rotation) as GameObject;
    Physics.IgnoreCollision(this.gameObject.collider, instantiatedProjectile.collider);
    instantiatedProjectile.rigidbody.velocity = startFire1.TransformDirection(new Vector3(0, 0, machinGunSpeed));
    GameObject instantiatedProjectile2 = Instantiate(machinGunPrefab,startFire2.position, startFire2.rotation) as GameObject;
    Physics.IgnoreCollision(this.gameObject.collider, instantiatedProjectile2.collider);
    instantiatedProjectile2.rigidbody.velocity = startFire2.TransformDirection(new Vector3(0, 0, machinGunSpeed));
  }

}
