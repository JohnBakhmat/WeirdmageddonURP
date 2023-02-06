using UnityEngine;

class DialogueDemoButton : Interactable
{

  public DialogueTrigger dialogueTrigger;
  private bool isTriggered = false;

  public override void Interact(Player player)
  {
    dialogueTrigger.Trigger();
  }

  void DetectPlayer()
  {
    var playerIsInRange = base.IsPlayerInRadius();

    interactionBar.SetActive(playerIsInRange);

    if (playerIsInRange)
    {
      Interact(GameObject.Find("Player").GetComponent<Player>());
      isTriggered = true;
    }
    else
    {
      isTriggered = false;
    }
  }


  void Update()
  {
    DetectPlayer();
  }


}