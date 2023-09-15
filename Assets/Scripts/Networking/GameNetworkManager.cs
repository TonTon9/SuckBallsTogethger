using System;
using System.Collections.Generic;
using Game.Networking.Player;
using Mirror;
using UnityEngine;

namespace Game.Networking
{
    public class GameNetworkManager : NetworkManager
    {
        public static GameNetworkManager Instance { get; private set; }
        
        public event Action PlayerRemoved = delegate { };
        public event Action<GameNetworkPlayer> ServerAddPlayer = delegate {  };
        public List<GameNetworkPlayer> NetworkPlayers = new List<GameNetworkPlayer>();

        public override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        public void StartLobbyAsHost()
        {
            StartHost();
            ServerChangeScene("Lobby");
        }
        
        public void StartGame()
        {
            ServerChangeScene("Game");
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            var player = conn.identity.GetComponent<GameNetworkPlayer>();
            player.playerName = ($"Player {UnityEngine.Random.Range(0, 1000)}");

            ServerAddPlayer?.Invoke(player);
            Debug.Log("Server add player");
        }
        
        public override void OnStopServer() {
            base.OnStopServer();
            NetworkPlayers.Clear();
            PlayerRemoved?.Invoke();
        }
        
        public override void OnStopClient() {
            base.OnStopClient();
            NetworkPlayers.Clear();
            PlayerRemoved?.Invoke();
        }
    }
}
