using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public GameObject ship;
    public GameObject shipPrefab;
    public SmoothFollow2 camScript;
    public SmoothFollow2 camScriptCrossHair;
    private ShipMovements shipMovementsScript;
    private ShipWeapons shipWeaponsScript;
    public Camera cam;
    public float vecticalSensibility = 0.2f;
    public float horizontalSensibility = 0.4f;
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool movingFront = false;
    private bool movingBack = false;
    private bool barrelLeft = false;
    private bool barrelRight = false;

    public GameObject plane;

  	void Start ()
    {
      this.ship = Instantiate(shipPrefab, Vector3.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
      this.camScript.setTarget(this.ship.transform);
      this.camScriptCrossHair.setTarget(this.ship.transform);
      this.shipMovementsScript = ship.GetComponent<ShipMovements>();
      this.shipWeaponsScript = ship.GetComponent<ShipWeapons>();
      Screen.showCursor = false;
      Screen.lockCursor = true;
  	}

    void  FixedUpdate()
    {
      if (movingLeft)
        this.shipMovementsScript.moveLeft();
      if (movingRight)
        this.shipMovementsScript.moveRight();
      if (movingFront)
        this.shipMovementsScript.moveFront();
      if (movingBack)
        this.shipMovementsScript.moveBack();
      if (barrelLeft)
        this.shipMovementsScript.barrelLeft();
      if (barrelRight)
        this.shipMovementsScript.barrelRight();
    }

  	// Update is called once per frame
  	void Update ()
    {

      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
      {
        movingFront = true;
        this.shipMovementsScript.startMoveFront();
      }

      if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
      {
        movingFront = false;
        this.shipMovementsScript.stopMoveFront();
      }

      if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
      {
        movingBack = true;
        this.shipMovementsScript.startMoveBack();
      }

      if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
      {
        movingBack = false;
        this.shipMovementsScript.stopMoveBack();
      }

      if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
      {
        movingRight = true;
        this.shipMovementsScript.startMovingRight();
      }

      if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
      {
        movingLeft = true;
        this.shipMovementsScript.startMovingLeft();
      }

      if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
      {
        movingRight = false;
        this.shipMovementsScript.stopMovingRight();
      }

      if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
      {
        movingLeft = false;
        this.shipMovementsScript.stopMovingLeft();
      }

      if (Input.GetKeyDown(KeyCode.E))
      {
        barrelRight = true;
        this.shipMovementsScript.startBarrelRight();
      }

      if (Input.GetKeyUp(KeyCode.E))
      {
        barrelRight = false;
        this.shipMovementsScript.stopBarrelRight();
      }

      if (Input.GetKeyDown(KeyCode.Q))
      {
        barrelLeft = true;
        this.shipMovementsScript.startBarrelLeft();
      }

      if (Input.GetKeyUp(KeyCode.Q))
      {
        barrelLeft = false;
        this.shipMovementsScript.stopBarrelLeft();
      }

      if (Input.GetKeyDown(KeyCode.LeftShift))
      {
        this.shipMovementsScript.startBoost();
      }

      if (Input.GetKeyUp(KeyCode.LeftShift))
      {
        this.shipMovementsScript.stopBoost();
      }

      if(Input.GetAxis("Mouse Y") < 0)
      {
          //Code for action on mouse moving down
          this.shipMovementsScript.rotateDown(this.vecticalSensibility * -Input.GetAxis("Mouse Y"));
      }

      if(Input.GetAxis("Mouse Y") > 0)
      {
          //Code for action on mouse moving up
          this.shipMovementsScript.rotateUp(this.vecticalSensibility * Input.GetAxis("Mouse Y"));
      }

      if(Input.GetAxis("Mouse X") < 0)
      {
          //Code for action on mouse moving left
          this.shipMovementsScript.rotateLeft(this.horizontalSensibility * -Input.GetAxis("Mouse X"));

      }
      if(Input.GetAxis("Mouse X") > 0)
      {
          //Code for action on mouse moving right
          this.shipMovementsScript.rotateRight(this.horizontalSensibility * Input.GetAxis("Mouse X"));
      }

      if(Input.GetButtonDown("Fire1"))
      {
        this.shipWeaponsScript.startPrimaryFire();
      }

      if(Input.GetButtonUp("Fire1"))
      {
        this.shipWeaponsScript.stopPrimaryFire();
      }

      if (Input.GetButtonDown("Fire2"))
      {
        this.camScript.zoom();
        this.camScriptCrossHair.zoom();
        this.shipMovementsScript.zoom();
      }

  	}
}
