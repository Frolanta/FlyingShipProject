using UnityEngine;
using System.Collections;

public class ShipCrosshair : MonoBehaviour {

  public float ratioSpeedSize = 1.0f;
  private float min = 0.40f;
  private bool zoomed = false;

	// Use this for initialization
	void Start ()
  {
    if (!transform.parent.gameObject.networkView.isMine)
      Destroy(this.gameObject.GetComponent<MeshRenderer>());
	}

	// Update is called once per frame
	void Update ()
  {

	}

  public void setSize(Vector3 velocity)
  {
    float value = ((Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) + Mathf.Abs(velocity.z)) * ratioSpeedSize) + this.min;
    transform.localScale = new Vector3(value, 1, value);
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
