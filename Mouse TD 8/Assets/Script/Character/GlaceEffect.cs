using System.Collections;
using UnityEngine;

public class GlaceEffect : MonoBehaviour
{
    [SerializeField] private float _speedAlpha;
    [SerializeField] private SpriteRenderer _spriteEffect;

    public void StartEffect(float radius)
    {
        _spriteEffect.color += new Color(0, 0, 0, 0.8f);
        _spriteEffect.transform.localScale = Vector3.one * radius;
        StartCoroutine(EffectAlpha());
    }
    private IEnumerator EffectAlpha()
    {
        _spriteEffect.color -= new Color(0, 0, 0, _speedAlpha);
        yield return null;
        if (_spriteEffect.color.a > 0)
        {
            StartCoroutine(EffectAlpha());
        }
    }
}
