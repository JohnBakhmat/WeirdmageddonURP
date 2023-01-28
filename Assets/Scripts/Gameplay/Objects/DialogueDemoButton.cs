using UnityEngine;

class DialogueDemoButton : Interactable
{
  [SerializeField] GameObject _interactionBar;
  [SerializeField] float _detectRadius = 0.5f;

  public DialogueTrigger dialogueTrigger;


  public override void Interact()
  {
    Debug.Log("Interacted with DialogueDemoButton");
    dialogueTrigger.Trigger();
  }

  void DetectPlayer()
  {
    var playerIsInRange = base.IsPlayerInRadius(_detectRadius);

    _interactionBar.SetActive(playerIsInRange);
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, _detectRadius);
  }


  void Update()
  {
    DetectPlayer();
  }


}