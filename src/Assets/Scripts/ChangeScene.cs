using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static PermManager;

public class ChangeScene : MonoBehaviour {

    public static ChangeScene cScene;


    public void Start()
    {
        cScene = this;
    }

    public static void loadNextScene(string sceneName) 
    {
        
		SceneManager.LoadScene(sceneName);
	}
}
