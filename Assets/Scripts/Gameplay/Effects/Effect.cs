using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObjects/Effect")]
public class Effect : ScriptableObject
{
  public Effectable target = null;
  public float duration = 0;
  protected float timeLeft = 0;

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
  }


  public void Update()
  {
    if (timeLeft <= 0)
    {
      Remove();
      return;
    }

    timeLeft -= Time.deltaTime;
    EffectTick();


  }
  public void Remove() => target.RemoveEffect(this);

  public virtual void EffectTick() => throw new System.NotImplementedException();
}
