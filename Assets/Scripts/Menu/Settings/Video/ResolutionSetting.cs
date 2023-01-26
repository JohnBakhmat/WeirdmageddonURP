using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionSetting : MonoBehaviour
{

  [SerializeField] private List<Resolution> resolutions;
  [SerializeField] private Resolution curRes;
  [SerializeField] private TextMeshPro resolutionText;


  void Start(){
    
    var currentRes = Screen.currentResolution;
    SetResolution(new Resolution(currentRes.width, currentRes.height));

    resolutions = new List<Resolution>();
    foreach (var res in Screen.resolutions)
    {
      resolutions.Add(new Resolution(res.width, res.height));
    }
  }

  private void SetResolution(Resolution res)
  {
    curRes = res;
    resolutionText.text = curRes.ToString();
    Screen.SetResolution(res.Width, res.Height, Screen.fullScreen);
  }


  void Update(){

    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      var index = resolutions.IndexOf(curRes);
      if (index > 0)
      {
        SetResolution(resolutions[index - 1]);
      }
    }

    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      var index = resolutions.IndexOf(curRes);
      if (index < resolutions.Count - 1)
      {
        SetResolution(resolutions[index + 1]);
      }
    }
  }
}

class Resolution
{
  public int Width { get; set; }
  public int Height { get; set; }

  public Resolution(int width, int height)
  {
    Width = width;
    Height = height;
  }

  public override string ToString()
  {
    return Width + "x" + Height;
  }
}
