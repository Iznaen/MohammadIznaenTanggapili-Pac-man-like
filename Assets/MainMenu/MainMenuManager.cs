using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // use OnClick() method in a button (UI game object) inspector to call this method
    public void Play()
    {
        // File > New Scene (CTRL + N) to create a new scene
        // don't forget to save it each time you create a new scene

        // each time you make a scene in Unity, you need to add the scene into the
        // game build setting (File > Build Setting), otherwise SceneManager() will
        // not recognize the scene
        SceneManager.LoadScene("Gameplay");

        // each scene added into build have index, the lower the index the higher the
        // priority of the scene to be loaded

        // if the scene open but its too dark
        // Open a Scene (Gameplay) > Window > Rendering > Lighting > Scene (Menu Tab)
        // Generate Lighting (Lightmapping Setting)
    }

    // use OnClick() method in a button (UI game object) inspector to call this method
    public void Exit()
    {
        Debug.Log("Exiting application ...");
        Application.Quit();
    }
}