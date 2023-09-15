using UnityEngine;

namespace Game.Lobby
{
    public class LobbyUIPlayersHolder : MonoBehaviour
    {
        public static LobbyUIPlayersHolder Instance;
        private void Awake()
        {
            Instance = this;
        }
    }
}
