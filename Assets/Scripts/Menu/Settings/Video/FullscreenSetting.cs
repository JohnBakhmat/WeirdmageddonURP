using UnityEngine;

public class FullscreenSetting : Slider
{

  public override void SetOption(string option)
  {
    curOption = option;
    label.text = curOption;
    Screen.fullScreenMode = (FullScreenMode)options.IndexOf(option);
  }

  private void Start()
  {
    options.Add("Windowed");
    options.Add("Borderless");
    options.Add("Fullscreen");
    SetOption(options[(int)Screen.fullScreenMode]);
  }
}
