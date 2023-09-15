using Game.Networking;
using Mirror;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class StartGameFromLobbyButton : MonoBehaviour
    {
        [SerializeField]
        private Button _startGameButton;

        private void Awake()
        {
            if (NetworkServer.active)
            {
                _startGameButton.OnClickAsObservable().Subscribe(_ => StartGame());
            } else
            {
                gameObject.SetActive(false);
            }
        }

        private void StartGame()
        {
            GameNetworkManager.Instance.StartGame();
        }
    }

}
