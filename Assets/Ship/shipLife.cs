using UnityEngine;
using System.Collections;

public class shipLife : MonoBehaviour {

  public int life = 100;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  void makeDamage(int dmg)
  {
    this.life -= dmg;
    if (this.life <= 0)
      Destroy(this.gameObject);
  }

  void OnDestroy()
  {
    print("ship Destroyed");
  }
}
