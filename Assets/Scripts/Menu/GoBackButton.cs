using UnityEngine;


public class GoBackButton : ButtonPress
{
  [SerializeField] private string previousSceneName;

  protected override void OnMouseDown()
  {
    base.OnMouseDown();
    Debug.Log("Go Back Button Pressed");

    if (previousSceneName != null)
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene(previousSceneName);
    }
  }
}