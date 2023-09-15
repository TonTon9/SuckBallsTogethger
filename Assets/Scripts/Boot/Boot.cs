using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class Boot : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}
