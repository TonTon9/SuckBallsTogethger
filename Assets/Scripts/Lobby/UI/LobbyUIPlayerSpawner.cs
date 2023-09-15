using Game.Networking;
using Game.Networking.Player;
using Mirror;
using UnityEngine;

namespace Game.Lobby
{
    public class LobbyUIPlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _lobbyUIPlayer;

        private void Awake()
        {
            foreach (var player in GameNetworkManager.Instance.NetworkPlayers)
            {
                SpawnPlayer(player);
            }
            GameNetworkManager.Instance.ServerAddPlayer += SpawnPlayer;
        }

        private void SpawnPlayer(GameNetworkPlayer player)
        {
            var connection = player.connectionToClient;
            var instance = Instantiate(_lobbyUIPlayer);
            NetworkServer.Spawn(instance, connection);
            instance.GetComponent<LobbyUIPlayer>().Init(player.playerName, player);
        }
    }
}