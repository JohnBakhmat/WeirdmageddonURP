using UnityEngine;

public class SettingsButtonPress : ButtonPress
{
  [SerializeField] private string _sceneName = "SettingsMenu";


  protected override void OnMouseDown()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
  }
}

