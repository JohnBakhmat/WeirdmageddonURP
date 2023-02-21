using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "Blind Effect", menuName = "ScriptablObjects/Effects/BlindEffect", order = 1)]
public class BlindEffect : Effect
{
  public BlindEffect(Effectable target, float duration) : base(target, duration)
  {
  }



  private Vignette vignette;

  public override void EffectTick()
  {

    GameObject.Find("PostProcessingVolume")
    .GetComponent<Volume>()
    .profile.TryGet<Vignette>(out vignette);

    var intensity = timeLeft / duration;
    vignette.intensity.value = Mathf.Lerp(0, 0.9f, intensity);

  }
}
