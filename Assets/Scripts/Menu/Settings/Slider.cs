using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Slider : MonoBehaviour
{
  [SerializeField] protected List<string> options = new List<string>();
  [SerializeField] protected TextMeshPro label;

  protected string curOption;

  public void OnArrowClick(Arrow.Direction direction)
  {
    var index = options.IndexOf(curOption);
    if (direction == Arrow.Direction.Left)
    {
      var nextIndex = (index - 1) < 0 ? options.Count - 1 : index - 1;
      SetOption(options[nextIndex]);
    }

    if (direction == Arrow.Direction.Right)
    {
      var nextIndex = (index + 1) > options.Count - 1 ? 0 : index + 1;
      SetOption(options[nextIndex]);
    }
  }

  public abstract void SetOption(string option);

}