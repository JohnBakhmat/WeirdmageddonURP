using UnityEngine;

public class BlindEffect : Effect
{
  public BlindEffect(Effectable target, float duration) : base(target, duration)
  {
  }

  public override void EffectTick()
  {
    Debug.Log($"Blind effect tick {timeLeft}");
  }
}