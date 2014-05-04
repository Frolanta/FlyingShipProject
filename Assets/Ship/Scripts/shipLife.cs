using UnityEngine;
using System.Collections;

public class shipLife : MonoBehaviour {

  public float life = 100;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  void makeDamage(int dmg)
  {
    if (Network.isServer)
    {
      this.life -= dmg;
      if (this.life <= 0)
        Network.Destroy(this.gameObject);
    }
  }

  void OnDestroy()
  {
    if (networkView.isMine)
    {
      GameObject.Find("Controller").GetComponent<ShipController>().spawnNewPlayer();
    }
    print("ship Destroyed");
  }

  public float getLife()
  {
    return this.life;
  }
}
