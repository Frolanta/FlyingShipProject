using UnityEngine;
using System.Collections;

public class ShipUI : MonoBehaviour
{
  private Rect lifeBarRect;
  private Rect energieBarRect;
  private Rect railBarRect;
  private Rect menuRect;
  private bool menu_enable = false;
  //public Rect lifeBarBackgroundRect;
  //public Texture2D lifeBarBackground;
  public Texture2D lifeBar;
  public Texture2D energieBar;
  public Texture2D railBar;

  private shipLife shipLifeScript;
  private ShipMovements shipMovementsScript;
  private ShipWeapons shipWeaponsScript;

  void Start()
  {
    this.lifeBarRect.y = Screen.height - 20;
    this.lifeBarRect.height = 20;

    this.energieBarRect.y = Screen.height - 50;
    this.energieBarRect.height = 20;

    this.railBarRect.y = Screen.height - 80;
    this.railBarRect.height = 20;

    this.menuRect.width = Screen.width / 2;
    this.menuRect.height = Screen.height / 3;

    this.menuRect.x = (Screen.width / 2) - (this.menuRect.width / 2);
    this.menuRect.y = (Screen.height / 2) - (this.menuRect.height / 2);

    Screen.showCursor = false;
    Screen.lockCursor = true;
  }

  void OnGUI()
  {
    if (shipLifeScript)
    {
      this.lifeBarRect.width = shipLifeScript.getLife();
      this.energieBarRect.width = shipMovementsScript.getEnergie();
      this.railBarRect.width = (shipWeaponsScript.getRailLoading() * 100.0f) / 5.0f;
      if (this.menu_enable)
        GUI.Window(0, menuRect, displayMenu, "Menu");

      //this.lifeBarBackgroundRect.width = LifeBarWidth;
      //this.lifeBarBackgroundRect.height= 20;

      GUI.DrawTexture(lifeBarRect, lifeBar);
      GUI.DrawTexture(energieBarRect, energieBar);
      GUI.DrawTexture(railBarRect, railBar);
      //GUI.DrawTexture(lifeBarBackgroundRect, lifeBarBackground);
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (this.menu_enable)
      {
        Screen.showCursor = false;
        Screen.lockCursor = true;
        this.menu_enable = false;
      }
      else
      {
        Screen.showCursor = true;
        Screen.lockCursor = false;
        this.menu_enable = true;
      }
    }
  }

  void displayMenu(int winId)
  {
    if (GUI.Button(new Rect(this.menuRect.width / 4, this.menuRect.height / 10, this.menuRect.width / 2, this.menuRect.height / 5), "Quit"))
      Application.Quit();
    if (GUI.Button(new Rect(this.menuRect.width / 4, this.menuRect.height / 3, this.menuRect.width / 2, this.menuRect.height / 5), "Resum"))
        this.menu_enable = false;
  }

  public void setScriptLife(shipLife script)
  {
    this.shipLifeScript = script;
  }

  public void setScriptMovements(ShipMovements script)
  {
    this.shipMovementsScript = script;
  }

  public void setScriptWeapons(ShipWeapons script)
  {
    this.shipWeaponsScript = script;
  }
}
