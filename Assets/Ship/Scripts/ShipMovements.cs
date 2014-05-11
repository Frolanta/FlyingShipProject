using UnityEngine;
using System.Collections;

public class ShipMovements : MonoBehaviour {

	// Use this for initialization
  public float rotation = 10;
  public float speed = 10;
  public float straff = 10;
  public GameObject reactorPrefab;
  public GameObject littleReactorPrefab;

  //reactor pos
  public Transform reactorPos;
  public Transform reactorSideFrontLeftPos;
  public Transform reactorSideBackLeftPos;
  public Transform reactorSideFrontRightPos;
  public Transform reactorSideBackRightPos;
  public Transform reactorFrontLeftPos;
  public Transform reactorFrontRightPos;
  public Transform reactorWingsBottomLeftPos;
  public Transform reactorWingsBottomRightPos;
  public Transform reactorWingsTopRightPos;
  public Transform reactorWingsTopLeftPos;

  //private
  private ParticleSystem reactor;
  private ParticleSystem reactorSideFrontLeft;
  private ParticleSystem reactorSideBackLeft;
  private ParticleSystem reactorSideFrontRight;
  private ParticleSystem reactorSideBackRight;
  private ParticleSystem reactorFrontLeft;
  private ParticleSystem reactorFrontRight;
  private ParticleSystem reactorWingsBottomLeft;
  private ParticleSystem reactorWingsBottomRight;
  private ParticleSystem reactorWingsTopRight;
  private ParticleSystem reactorWingsTopLeft;

  private bool boost_enabled = false;
  private bool zoomEnabled = false;

  private float energie = 100.0f;

	void Awake ()
  {
    //instantiate reactors
    GameObject react;
    react = Instantiate(reactorPrefab, reactorPos.position, reactorPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactor = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorSideFrontLeftPos.position, reactorSideFrontLeftPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorSideFrontLeft = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorSideBackLeftPos.position, reactorSideBackLeftPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorSideBackLeft = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorSideFrontRightPos.position, reactorSideFrontRightPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorSideFrontRight = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorSideBackRightPos.position, reactorSideBackRightPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorSideBackRight = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorFrontLeftPos.position, reactorFrontLeftPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorFrontLeft = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorFrontRightPos.position, reactorFrontRightPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorFrontRight = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorWingsBottomLeftPos.position, reactorWingsBottomLeftPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorWingsBottomLeft = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorWingsBottomRightPos.position, reactorWingsBottomRightPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorWingsBottomRight = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorWingsTopRightPos.position, reactorWingsTopRightPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorWingsTopRight = react.GetComponent<ParticleSystem>();

    react = Instantiate(littleReactorPrefab, reactorWingsTopLeftPos.position, reactorWingsTopLeftPos.rotation) as GameObject;
    react.transform.parent = this.transform;
    this.reactorWingsTopLeft = react.GetComponent<ParticleSystem>();

    this.reactor.enableEmission = false;
    this.reactorSideFrontLeft.enableEmission = false;
    this.reactorSideBackLeft.enableEmission = false;
    this.reactorSideFrontRight.enableEmission = false;
    this.reactorSideBackRight.enableEmission = false;
    this.reactorFrontLeft.enableEmission = false;
    this.reactorFrontRight.enableEmission = false;
    this.reactorWingsBottomLeft.enableEmission = false;
    this.reactorWingsBottomRight.enableEmission = false;
    this.reactorWingsTopRight.enableEmission = false;
    this.reactorWingsTopLeft.enableEmission = false;
	}

  void  Update()
  {
    if (this.boost_enabled)
    {
      if (energie > 0.0f)
      {
        energie -= Time.deltaTime * 50;
        if (energie < 0.0f)
        {
          energie = 0.0f;
          stopBoost();
        }
      }
    }
    else
    {
      if (energie < 100.0f)
      {
        energie += Time.deltaTime * 10;
        if (energie > 100.0f)
          energie = 100.0f;
      }
    }
  }

	// Update is called once per frame
  public void moveRight()
  {
    this.rigidbody.AddRelativeForce(Vector3.right * straff);
  }

  public void moveLeft()
  {
    this.rigidbody.AddRelativeForce(Vector3.left * straff);
  }

  [RPC]
  public void startMovingLeft()
  {
    reactorSideFrontRight.enableEmission = true;
    reactorSideBackRight.enableEmission = true;
  }

  [RPC]
  public void stopMovingLeft()
  {
    reactorSideFrontRight.enableEmission = false;
    reactorSideBackRight.enableEmission = false;
  }

  [RPC]
  public void startMovingRight()
  {
    reactorSideFrontLeft.enableEmission = true;
    reactorSideBackLeft.enableEmission = true;
  }

  [RPC]
  public void stopMovingRight()
  {
    reactorSideFrontLeft.enableEmission = false;
    reactorSideBackLeft.enableEmission = false;
  }

  public void rotateUp(float value)
  {
    this.transform.Rotate(Vector3.left * value * Time.deltaTime);
  }

  public void rotateDown(float value)
  {
    this.transform.Rotate(Vector3.right * value * Time.deltaTime);
  }

  public void rotateLeft(float value)
  {
    this.transform.Rotate(Vector3.down * value * Time.deltaTime);
  }

  public void rotateRight(float value)
  {
    this.transform.Rotate(Vector3.up * value* Time.deltaTime);
  }

  public void moveFront()
  {
      this.rigidbody.AddRelativeForce(Vector3.forward * speed);
  }

  [RPC]
  public void startMoveFront()
  {
      reactor.enableEmission = true;
  }

  [RPC]
  public void stopMoveFront()
  {
      reactor.enableEmission = false;
  }

  public void moveBack()
  {
      this.rigidbody.AddRelativeForce(Vector3.back * (speed / 2));
  }

  [RPC]
  public void startMoveBack()
  {
      reactorFrontLeft.enableEmission = true;
      reactorFrontRight.enableEmission = true;
  }

  [RPC]
  public void stopMoveBack()
  {
      reactorFrontLeft.enableEmission = false;
      reactorFrontRight.enableEmission = false;
  }

  public void barrelRight()
  {
    this.rigidbody.AddRelativeTorque(Vector3.back * rotation);
  }

  [RPC]
  public void startBarrelRight()
  {
    reactorWingsTopRight.enableEmission = true;
    reactorWingsBottomLeft.enableEmission = true;
  }

  [RPC]
  public void stopBarrelRight()
  {
    reactorWingsTopRight.enableEmission = false;
    reactorWingsBottomLeft.enableEmission = false;
  }

  public void barrelLeft()
  {
    this.rigidbody.AddRelativeTorque(Vector3.forward * rotation);
  }

  [RPC]
  public void startBarrelLeft()
  {
    reactorWingsTopLeft.enableEmission = true;
    reactorWingsBottomRight.enableEmission = true;
  }

  [RPC]
  public void stopBarrelLeft()
  {
    reactorWingsTopLeft.enableEmission = false;
    reactorWingsBottomRight.enableEmission = false;
  }

  public void startBoost()
  {
    if (!this.boost_enabled)
    {
      this.speed *= 2;
      this.straff *= 2;
      this.boost_enabled = true;
    }
  }

  public void stopBoost()
  {
    if (this.boost_enabled)
    {
      this.speed /= 2;
      this.straff /= 2;
      this.boost_enabled = false;
    }
  }

  public void zoom()
  {
    if (this.zoomEnabled)
    {
      this.speed *= 4;
      this.straff *= 4;
      this.rotation *= 4;
      this.zoomEnabled = false;
    }
    else
    {
      this.speed /= 4;
      this.straff /= 4;
      this.rotation /= 4;
      this.zoomEnabled = true;
    }
  }

  public float getEnergie()
  {
    return this.energie;
  }
}
