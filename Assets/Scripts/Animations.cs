using System;
using DG.Tweening;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private void Awake()
    {
        DOTween.Init();
    }

    public void PlaySpawnAnimation(SpriteRenderer sprite)
    {
        sprite.transform.DOScale(0.6f, 0.5f).SetEase(Ease.OutBack);
    }

    public void PlaySwapAnimation(SpriteRenderer firstSprite, SpriteRenderer secondSprite, Action onComplete)
    {
        var ta = firstSprite.transform;
        var tb = secondSprite.transform;

        Vector3 posA = ta.position;
        Vector3 posB = tb.position;

        // Убираем любые прошлые твины на этих трансформах
        ta.DOKill();
        tb.DOKill();

        var seq = DOTween.Sequence();
        seq.Join(ta.DOMove(posB, 0.3f).SetEase(Ease.InOutQuad));
        seq.Join(tb.DOMove(posA, 0.3f).SetEase(Ease.InOutQuad));

        // После завершения: сбрасываем локальные позиции спрайтов и вызываем коммит
        seq.OnComplete(() =>
        {
            ta.localPosition = Vector3.zero;
            tb.localPosition = Vector3.zero;
            onComplete?.Invoke();
        });
    }
}