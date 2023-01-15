using UnityEngine;
using TMPro;

public class ButtonPress : MonoBehaviour
{

  [SerializeField] private TextMeshPro textMesh;
  [SerializeField] private Color baseColor = Color.white;
  //Teal color
  [SerializeField] private Color hoverColor = new Color() { r = 0.0f, g = 128f / 225f, b = 128f / 225f, a = 1.0f };

  protected void OnMouseEnter()
  {
    textMesh.color = hoverColor;
  }

  protected void OnMouseExit()
  {
    textMesh.color = baseColor;
  }

  protected virtual void OnMouseDown()
  {
    Debug.Log("Button Pressed");
  }
}

