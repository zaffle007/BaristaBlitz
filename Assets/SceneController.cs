using UnityEngine;
using UnityEngine.SceneManagement;

//code from https://www.youtube.com/watch?v=nwFxyY95Lls
public class SceneController : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
