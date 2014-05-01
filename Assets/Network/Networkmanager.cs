﻿using UnityEngine;
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
      Application.LoadLevel("ship");
  }

  void OnConnectedToServer()
  {
      print("connect");
      Application.LoadLevel("ship");
  }
}