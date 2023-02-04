using UnityEngine;

public class ResolutionSetting : Slider
{
  public override void SetOption(string option)
  {
    curOption = option;
    label.text = curOption;
    var res = option.Split('x');
    Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), Screen.fullScreen);
  }

  private void Start()
  {
    var resolutions = Screen.resolutions;
    foreach (var res in resolutions)
    {
      options.Add(res.width + "x" + res.height);
    }
    SetOption(Screen.currentResolution.width + "x" + Screen.currentResolution.height);
  }
}

