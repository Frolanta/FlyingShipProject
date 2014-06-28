using UnityEngine;
using System.Collections;

public class ShipPresentation : MonoBehaviour {

  public float rotation = 50.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
  {
	   this.transform.Rotate(new Vector3(0, this.rotation * Time.deltaTime, 0));
	}
}
