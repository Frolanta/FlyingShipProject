using UnityEngine;
using System.Collections;

public class Networkmanager : MonoBehaviour {

  private const string typeName = "shipproject";
  private const string gameName = "alpharoom";
  private HostData[] hostList;

  void Awake()
  {
    DontDestroyOnLoad(this.gameObject);
  }

  private void StartServer()
  {
      Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
      MasterServer.RegisterHost(typeName, gameName);
  }


  void OnGUI()
  {
    if (!Network.isClient && !Network.isServer)
    {
        if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            StartServer();

        if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
            RefreshHostList();

        if (hostList != null)
        {
            for (int i = 0; i < hostList.Length; i++)
            {
                if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                    JoinServer(hostList[i]);
            }
        }
    }
  }

  private void RefreshHostList()
  {
      MasterServer.RequestHostList(typeName);
  }

  void OnMasterServerEvent(MasterServerEvent msEvent)
  {
      if (msEvent == MasterServerEvent.HostListReceived)
          hostList = MasterServer.PollHostList();
  }

  private void JoinServer(HostData hostData)
  {
      Debug.Log(Network.Connect(hostData));
  }

  void OnServerInitialized()
  {
      networkView.RPC("ClientLoadLevel", RPCMode.OthersBuffered);
      Application.LoadLevel(1);
  }

  [RPC]
  private void ClientLoadLevel()
  {
      //Stop the message queue and load the game level
      Network.isMessageQueueRunning = false;
      Network.SetLevelPrefix(1);
      Application.LoadLevel(1);
  }

  private void OnLevelWasLoaded(int level)
  {
      //Resume the message queue upon loading the game scene
      Network.isMessageQueueRunning = true;
  }

  internal static void Disconnect()
  {
      //Disconnect from the server
      Network.Disconnect();

      //Return to the menu if neccesairy
      if (Application.loadedLevel != 0)
          Application.LoadLevel(0);
  }

  void OnPlayerDisconnected(NetworkPlayer player)
  {
      //When a player disconnects, destroy his objects
      Network.RemoveRPCs(player, 0);
      Network.DestroyPlayerObjects(player);
  }

  private void OnDisconnectedFromServer()
  {
      //Return to the menu on disconnect
      if (Application.loadedLevel != 1)
          Application.LoadLevel(1);
  }
}
