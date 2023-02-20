using UnityEngine;

public class EffectTrigger : Interactable
{

  private bool isTriggered = false;

  public Effect effect;

  public override void Interact(Player player)
  {
    effect.target = player;
    effect.Apply();
  }

  protected override void DetectPlayer()
  {
    base.DetectPlayer();

    var playerIsInRange = base.IsPlayerInRadius();

    if (playerIsInRange)
    {
      if (!isTriggered)
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

