using UnityEngine;

[CreateAssetMenu(fileName = "Blind Effect", menuName = "ScriptablObjects/Effects/BlindEffect", order = 1)]
public class BlindEffect : Effect
{
  public BlindEffect(Effectable target, float duration) : base(target, duration)
  {
  }

  public override void EffectTick()
  {
    Debug.Log($"Blind effect tick {((int)timeLeft).ToString()}");
  }
}