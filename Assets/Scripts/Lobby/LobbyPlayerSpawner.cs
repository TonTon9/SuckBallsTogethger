using System;
using Game.Networking;
using Game.Networking.Player;
using Mirror;
using UnityEngine;

namespace Game.Lobby
{
    public class LobbyPlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _lobbyPlayer;

        [SerializeField]
        private GameObject _lobbyUI;
        
        [SerializeField]
        private StartSelectCharactersInButton _startSelectCharactersInButton;

        private void Awake()
        {
            GameNetworkPlayer.StartChoosingCharacters += StartChoosingCharacters;
        }

        private void StartChoosingCharacters()
        {
            Destroy(_lobbyUI);
            if (NetworkServer.active)
            {
                SpawnPlayers();
            }
            
        }

        [Server]
        public void SpawnPlayers()
        {
            foreach (var player in GameNetworkManager.Instance.NetworkPlayers)
            {
                SpawnPlayer(player);
            }
        }

        private void SpawnPlayer(GameNetworkPlayer player)
        {
            var connection = player.connectionToClient;
            var instance = Instantiate(_lobbyPlayer, player.transform.position, player.transform.rotation);
            NetworkServer.Spawn(instance, connection);
            instance.GetComponent<LobbyPlayer>().Init(player.playerName, player);
        }

        private void OnDestroy()
        {
            GameNetworkPlayer.StartChoosingCharacters -= StartChoosingCharacters;
        }
    }

}

