using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, Effectable
{
  public List<Effect> effects = new List<Effect>();

  public void ApplyEffect(Effect effect) => effects.Add(effect);

  public void RemoveEffect(Effect effect) => effects.Remove(effect);
}
