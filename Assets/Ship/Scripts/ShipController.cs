using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public GameObject ship;
    public GameObject shipPrefab;
    public SmoothFollow2 camScript;
    public SmoothFollow2 camScriptCrossHair;
    public ShipUI scriptUI;
    private ShipMovements shipMovementsScript;
    private ShipWeapons shipWeaponsScript;
    private ShipCrosshair shipCrosshairScript;
    public Camera cam;
    public float vecticalSensibility = 0.2f;
    public float horizontalSensibility = 0.4f;
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool movingFront = false;
    private bool movingBack = false;
    private bool barrelLeft = false;
    private bool barrelRight = false;
    private GameObject[] respawns;

    void Start()
    {
      this.respawns = GameObject.FindGameObjectsWithTag("Respawn");
      spawnNewPlayer();
    }

  	public void spawnNewPlayer()
    {
      Transform spawn = respawns[Random.Range(0, this.respawns.Length)].transform;
      this.movingLeft = false;
      this.movingRight = false;
      this.movingFront = false;
      this.movingBack = false;
      this.barrelLeft = false;
      this.barrelRight = false;
      this.ship = Network.Instantiate(shipPrefab, spawn.position, spawn.rotation, 0) as GameObject;
      //this.ship.rigidbody.interpolation = RigidbodyInterpolation.Interpolate; // use this if add velocity in Update and not in Fixed update
      this.camScript.setTarget(this.ship.transform);
      this.camScriptCrossHair.setTarget(this.ship.transform);
      this.shipMovementsScript = ship.GetComponent<ShipMovements>();
      this.shipWeaponsScript = ship.GetComponent<ShipWeapons>();
      this.shipCrosshairScript = ship.GetComponent<ShipCrosshair>();
      this.camScript.resetZoom();
      this.camScriptCrossHair.resetZoom();
      this.scriptUI.setScriptLife(ship.GetComponent<shipLife>());
      this.scriptUI.setScriptWeapons(ship.GetComponent<ShipWeapons>());
      this.scriptUI.setScriptMovements(this.shipMovementsScript);
  	}

    void  FixedUpdate()
    {
      if (this.ship)
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

        if (movingLeft || movingRight || movingFront || movingBack || barrelLeft || barrelRight)
          this.shipCrosshairScript.setSize(true);
        else
          this.shipCrosshairScript.setSize(false);
      }

    }

  	// Update is called once per frame
  	void Update ()
    {
      if (this.ship)
      {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
          movingFront = true;
          ship.networkView.RPC("startMoveFront", RPCMode.OthersBuffered);
          this.shipMovementsScript.startMoveFront();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
          movingFront = false;
          ship.networkView.RPC("stopMoveFront", RPCMode.OthersBuffered);
          this.shipMovementsScript.stopMoveFront();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
          movingBack = true;
          ship.networkView.RPC("startMoveBack", RPCMode.OthersBuffered);
          this.shipMovementsScript.startMoveBack();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
          movingBack = false;
          ship.networkView.RPC("stopMoveBack", RPCMode.OthersBuffered);
          this.shipMovementsScript.stopMoveBack();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
          movingRight = true;
          ship.networkView.RPC("startMovingRight", RPCMode.OthersBuffered);
          this.shipMovementsScript.startMovingRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
          movingLeft = true;
          ship.networkView.RPC("startMovingLeft", RPCMode.OthersBuffered);
          this.shipMovementsScript.startMovingLeft();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
          movingRight = false;
          ship.networkView.RPC("stopMovingRight", RPCMode.OthersBuffered);
          this.shipMovementsScript.stopMovingRight();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
          movingLeft = false;
          ship.networkView.RPC("stopMovingLeft", RPCMode.OthersBuffered);
          this.shipMovementsScript.stopMovingLeft();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
          barrelRight = true;
          ship.networkView.RPC("startBarrelRight", RPCMode.OthersBuffered);
          this.shipMovementsScript.startBarrelRight();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
          barrelRight = false;
          ship.networkView.RPC("stopBarrelRight", RPCMode.OthersBuffered);
          this.shipMovementsScript.stopBarrelRight();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
          barrelLeft = true;
          ship.networkView.RPC("startBarrelLeft", RPCMode.OthersBuffered);
          this.shipMovementsScript.startBarrelLeft();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
          barrelLeft = false;
          ship.networkView.RPC("stopBarrelLeft", RPCMode.OthersBuffered);
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
          this.shipCrosshairScript.zoom();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
          this.shipWeaponsScript.stopPrimaryFire();
          this.shipWeaponsScript.selectMachinGun();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
          this.shipWeaponsScript.stopPrimaryFire();
          this.shipWeaponsScript.selectRailGun();
        }
      }
  	}
}
