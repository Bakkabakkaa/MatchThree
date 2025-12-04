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
}