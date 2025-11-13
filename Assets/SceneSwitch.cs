using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class SceneSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string sceneName;


    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Home button clicked");
        SceneManager.LoadScene(sceneName);
    }
}
