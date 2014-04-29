using UnityEngine;
using System.Collections;

public class ShipCrosshair : MonoBehaviour {

  public Transform crosshair;
  public float ratioSpeedSize = 1.0f;
  private float min = 0.40f;
  private bool zoomed = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
  {

	}

  public void setSize(Vector3 velocity)
  {
    float value = ((Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) + Mathf.Abs(velocity.z)) * ratioSpeedSize) + this.min;
    transform.localScale = new Vector3(value, 0, value);
  }

  public void zoom()
  {
    if (zoomed)
    {
      this.min *= 3;
      zoomed = false;
    }
    else
    {
      this.min /= 3;
      zoomed = true;
    }
  }
}
