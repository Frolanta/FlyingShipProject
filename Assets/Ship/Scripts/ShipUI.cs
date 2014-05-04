using UnityEngine;
using System.Collections;

public class ShipUI : MonoBehaviour
{
  public Rect lifeBarRect;
  public Rect energieBarRect;
  //public Rect lifeBarBackgroundRect;
  //public Texture2D lifeBarBackground;
  public Texture2D lifeBar;
  public Texture2D energieBar;

  private shipLife shipLifeScript;
  private ShipMovements shipMovementsScript;

  void OnGUI()
  {
    if (shipLifeScript)
    {
      this.lifeBarRect.width = shipLifeScript.getLife();
      this.lifeBarRect.y = Screen.height - 20;
      this.lifeBarRect.height = 20;

      this.energieBarRect.width = shipMovementsScript.getEnergie();
      this.energieBarRect.y = Screen.height - 50;
      this.energieBarRect.height = 20;

      //this.lifeBarBackgroundRect.width = LifeBarWidth;
      //this.lifeBarBackgroundRect.height= 20;

      GUI.DrawTexture(lifeBarRect, lifeBar);
      GUI.DrawTexture(energieBarRect, energieBar);
      //GUI.DrawTexture(lifeBarBackgroundRect, lifeBarBackground);
    }
  }

  public void setScriptLife(shipLife script)
  {
    this.shipLifeScript = script;
  }

  public void setScriptMovements(ShipMovements script)
  {
    this.shipMovementsScript = script;
  }
}
