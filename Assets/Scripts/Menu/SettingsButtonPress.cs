public class SettingsButtonPress : ButtonPress
{
  protected override void OnMouseDown()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsMenu");
  }
}

