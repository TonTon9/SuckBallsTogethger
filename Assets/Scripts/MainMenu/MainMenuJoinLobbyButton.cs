using Mirror;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuJoinLobbyButton : MonoBehaviour
    {
        [SerializeField]
        private Button _joinLobbyButton;

        private void Awake()
        {
            _joinLobbyButton.OnClickAsObservable().Subscribe(_ => joinLobby());
        }

        private void joinLobby()
        {
            NetworkManager.singleton.networkAddress = "localhost";
            NetworkManager.singleton.StartClient();
        }
    }
}
