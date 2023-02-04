using UnityEngine;

public class FullscreenSetting : Slider
{

  public override void SetOption(string option)
  {
    curOption = option;
    label.text = curOption;

    switch (option)
    {
      case "FullScreen":
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        break;
      case "Borderless":
        Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        break;
      case "Windowed":
        Screen.fullScreenMode = FullScreenMode.Windowed;
        break;
    }
  }

  private void Start()
  {
    options.Add("FullScreen");
    options.Add("Borderless");
    options.Add("Windowed");
    SetOption(options[0]);
  }
}
