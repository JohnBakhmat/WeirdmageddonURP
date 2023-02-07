using UnityEngine;

class DialogueDemoButton : Interactable
{

  public DialogueTrigger dialogueTrigger;
  private bool isTriggered = false;

  public override void Interact(Player player)
  {
    dialogueTrigger.Trigger();
  }

  protected override void DetectPlayer()
  {
    base.DetectPlayer();

    var playerIsInRange = base.IsPlayerInRadius();

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


  protected override void Update()
  {
    base.Update();
    DetectPlayer();
  }


}