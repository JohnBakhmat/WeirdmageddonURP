using UnityEngine;
using TMPro;

public class ButtonPress : MonoBehaviour
{

  [SerializeField] private TextMeshPro textMesh;
  [SerializeField] private Color baseColor = Color.white;
  [SerializeField] private Color hoverColor = Color.red;


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

