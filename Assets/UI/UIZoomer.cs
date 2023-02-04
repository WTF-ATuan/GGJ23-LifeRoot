using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class UIZoomer : MonoBehaviour
{
    public UIZoomer[] Others;

    public bool Alpha;
    public bool Scale;
    public bool Canvas;
    public bool Height;
    public bool Weight;
    public float Duration = 0.2f;

    private Image Image;
    private Color BaseColor;
    private CanvasGroup Canvass;
    private float HeightSave;
    private float WeightSave;
    private RectTransform Rect;

    private bool HaveInit = false;
    
    private void Init()
    {
        if (Alpha)
        {
            Image = GetComponent<Image>();
            BaseColor = Image.color;
            Color color = BaseColor;
            color.a = 0;
            Image.color = color;
        }

        if (Canvas)
        {
            Canvass = GetComponent<CanvasGroup>();
            Canvass.alpha = 0;
        }
        if (Scale) transform.localScale = Vector3.zero;

        SetHeight();

        HaveInit = true;

    }

    public void SetHeight()
    {
        if (Height)
        {
            Rect = (RectTransform)transform;
            HeightSave = Rect.rect.height;
        }
        if (Weight)
        {
            Rect = (RectTransform)transform;
            WeightSave = Rect.rect.width;
        }
    }

    public void Zoom(bool zoom)
    {
        if (zoom)
        { if (!gameObject.activeSelf) ZoomIn(); }
        else
        { if (gameObject.activeSelf) ZoomOut(); }
    }

    public void ZoomIn()
    {
        if (!HaveInit) Init();
        gameObject.SetActive(true);
        if (Counter != null)
        {
            StopCoroutine(Counter);
            Counter = null;
        }
        mySequence.Kill();
        mySequence = DOTween.Sequence();
        if (Scale)
        {
            transform.localScale = Vector3.one * 0.8f;
            mySequence.Insert(0, transform.DOScale(Vector3.one, Duration).SetEase(Ease.OutBack));
        }
        if (Alpha) mySequence.Insert(0, GetComponent<Image>().DOColor(BaseColor, Duration));
        if (Canvas) mySequence.Insert(0, DOTween.To(() => Canvass.alpha, x => Canvass.alpha = x, 1, Duration));
        if (Height)
        {
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            mySequence.Insert(0, DOTween.To(() => Rect.rect.height, x => Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x), HeightSave, Duration));
        }
        if (Weight)
        {
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mySequence.Insert(0, DOTween.To(() => Rect.rect.width, x => Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x), WeightSave, Duration));
        }

        foreach (var item in Others)
        {
            if (item == null)
            {
                Debug.LogError($"UIZoomer is null on {gameObject.name}");
                continue;
            }
            item.ZoomIn();
        }
        if (gameObject.activeInHierarchy) Counter = StartCoroutine(OnZoomInFinish());
    }

    public bool ZoomInFinish { private set; get; }

    IEnumerator OnZoomInFinish()
    {
        ZoomInFinish = false;
        yield return new WaitForSeconds(Duration);
        ZoomInFinish = true;
    }

    public void ZoomOut()
    {
        ZoomOut(Duration);
    }

    public void ZoomOut(float duration)
    {
        ZoomOut(duration, null);
    }

    public void ZoomOut(Action task)
    {
        ZoomOut(Duration, task);
    }

    public void ZoomOut(float duration, Action task)
    {
        foreach (var item in Others)
        {
            item?.ZoomOut(duration);
        }

        if (Counter != null)
        {
            StopCoroutine(Counter);
            Counter = null;
        }

        gameObject.SetActive(true);
        if (gameObject.activeInHierarchy)
        {
            mySequence.Kill();
            mySequence = DOTween.Sequence();
            if (Scale) mySequence.Insert(0, transform.DOScale(Vector3.zero, duration));
            if (Alpha) mySequence.Insert(0, Image.DOColor(new Color(BaseColor.r, BaseColor.g, BaseColor.b, 0), duration));
            if (Canvas) mySequence.Insert(0, DOTween.To(() => Canvass.alpha, x => Canvass.alpha = x, 0, duration));
            if (Height) mySequence.Insert(0, DOTween.To(() => Rect.rect.height, x => Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x), 0, Duration));
            if (Weight) mySequence.Insert(0, DOTween.To(() => Rect.rect.width, x => Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x), 0, Duration));
            Counter = StartCoroutine(UnActiveCounter(duration, task));
        }
        else
        {
            task?.Invoke();
        }
    }

    public void Reset()
    {
        Init();
    }

    public Sequence mySequence;

    private Coroutine Counter;
    IEnumerator UnActiveCounter(float duration, Action task)
    {
        yield return new WaitForSeconds(duration);
        task?.Invoke();
        gameObject.SetActive(false);
        Counter = null;
    }
}

