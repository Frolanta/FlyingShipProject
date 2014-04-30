using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour {

	// Use this for initialization
  public GameObject machinGunPrefab;
  public float machinGunSpeed = 50;
  public float machinGunFireRate = 0.5f;
  public Transform startFire1;
  public Transform startFire2;
  public Transform railStart;

  public GameObject railPrefab;

  public bool machinGun = true;
  public bool railGun = false;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  public void startPrimaryFire()
  {
    if (machinGun)
      InvokeRepeating("fireMachinGun", 0.05f, this.machinGunFireRate);
    else
      fireRail();
  }

  public void stopPrimaryFire()
  {
    if (machinGun)
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

  void  fireRail()
  {
    //Debug.DrawRay (railStart.position,  railStart.forward * 500, Color.green);
    GameObject rail = Instantiate(railPrefab, railStart.position, railStart.rotation) as GameObject;
    LineRenderer line = rail.GetComponent<LineRenderer>();
    line.useWorldSpace = false;
    line.SetVertexCount(2);
    RaycastHit[] hits;
    hits = Physics.RaycastAll(railStart.position, railStart.forward, 500.0F);
    line.SetPosition(1, new Vector3(0,0,500));
    int i = 0;
    while (i < hits.Length)
    {
      RaycastHit hit = hits[i];
      print(hit.collider.transform.gameObject.name);
      hit.collider.transform.gameObject.SendMessage("makeDamage", 100 / (hits.Length - i), SendMessageOptions.DontRequireReceiver);
      i++;
    }
  }

  public void selectMachinGun()
  {
    this.machinGun = true;
    this.railGun = false;
  }

  public void selectRailGun()
  {
    this.railGun = true;
    this.machinGun = false;
  }

}
