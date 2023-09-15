using Game.Networking;
using Game.Networking.Player;
using Mirror;
using UnityEngine;

namespace Game.Game.CharacterSpawner
{
    public class GameCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnerCharacterPosition[] _placesForSpawn;
        [SerializeField] private GameObject _gamePlayPlayer;

        private void Awake()
        {
            if (NetworkServer.active)
            {
                SpawnPlayers();
            }
        }

        private void SpawnPlayers()
        {
            foreach (var player in GameNetworkManager.Instance.NetworkPlayers)
            {
                var connection = player.connectionToClient;
                var posToSpawn = GetFreePosition();
                posToSpawn.IsFree = false;
                var instance = Instantiate(_gamePlayPlayer, posToSpawn.Position.position, posToSpawn.Position.rotation);
                NetworkServer.Spawn(instance, connection);
            }
        }
        
        [Server]
        private SpawnerCharacterPosition GetFreePosition()
        {
            SpawnerCharacterPosition position = null;
            foreach (var pos in _placesForSpawn)
            {
                if (pos.IsFree)
                {
                    position = pos;
                }
            }
            return position;
        }
    }

}
