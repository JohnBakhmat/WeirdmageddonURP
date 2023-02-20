using UnityEngine;


public class Effect : ScriptableObject
{
  public Effectable target = null;
  public float duration = 0f;
  protected float timeLeft = 0f;
  public bool isExpired => timeLeft <= 0;

  public Effect(Effectable target, float duration)
  {
    this.target = target;
    this.duration = duration;
    this.timeLeft = duration;
  }

  public void Apply()
  {
    if (target == null)
      throw new System.Exception("Effectable target is null");

    target.ApplyEffect(this);
    timeLeft = duration;
  }


  public void Update()
  {
    if (!isExpired)
    {
      timeLeft -= Time.deltaTime;
      EffectTick();
    }

    if (timeLeft <= 0)
    {
      Remove();
      return;
    }
  }
  public void Remove() => target.RemoveEffect(this);

  public virtual void EffectTick() => throw new System.NotImplementedException();
}
