using UnityEngine;
using System.Collections;

public class ShipCrosshair : MonoBehaviour {

  public float ratioSpeedSize = 1.0f;
  public Transform left;
  public Transform right;
  public Transform top;
  public Transform bottom;
  public MeshRenderer dot;

  private bool  zoomed = false;
  private float time = 0.0f;

	// Use this for initialization
	void Start ()
  {
    if (!this.gameObject.networkView.isMine)
    {
      Destroy(left.gameObject);
      Destroy(right.gameObject);
      Destroy(top.gameObject);
      Destroy(dot.gameObject);
      Destroy(bottom.gameObject);
      Destroy(this);
    }
    else
      dot.enabled = false;
	}

	// Update is called once per frame
	void Update ()
  {

	}

  public void setSize(bool moving)
  {
    if (moving && time < 0.15f)
    {
      left.Translate(-ratioSpeedSize, 0, 0);
      right.Translate(ratioSpeedSize, 0, 0);
      top.Translate(ratioSpeedSize, 0, 0);
      bottom.Translate(-ratioSpeedSize, 0, 0);
      time += Time.fixedDeltaTime;
      if (time >= 5.0f)
        time = 5.0f;
    }
    else if (!moving && time > 0.0f)
    {
      left.Translate(ratioSpeedSize, 0, 0);
      right.Translate(-ratioSpeedSize, 0, 0);
      top.Translate(-ratioSpeedSize, 0, 0);
      bottom.Translate(ratioSpeedSize, 0, 0);
      time -= Time.fixedDeltaTime;
      if (time <= 0.0f)
        time = 0.0f;
    }
  }

  public void zoom()
  {
    if (zoomed)
    {
      dot.enabled = false;
      zoomed = false;
    }
    else
    {
      dot.enabled = true;
      zoomed = true;
    }
  }
}
