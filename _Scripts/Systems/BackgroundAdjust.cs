using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundAdjust : CustomMonoBehaviour
{
    [SerializeField]
    private RectTransform _canvas;

    [SerializeField]
    private Rect _spriteRect;

    private Coroutine _backgroundAdjustCoroutine;

    protected override void LoadComponents()
    {
        _canvas = GetComponentInParent<RectTransform>();
        _spriteRect = GetComponent<SpriteRenderer>().sprite.rect;
    }

    protected override void LoadDynamicData()
    {
        _backgroundAdjustCoroutine = StartCoroutine(Adjust());
    }

    IEnumerator Adjust()
    {
        float canvasWidth = _canvas.sizeDelta.x;
        float canvasHeight = _canvas.sizeDelta.y;

        float spriteWidth = _spriteRect.width;
        float spriteHeight = _spriteRect.height;

        transform.localScale = new Vector3(
            canvasWidth / spriteWidth,
            canvasHeight / spriteHeight,
            1
        );
        _backgroundAdjustCoroutine = null;
        yield break;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (_backgroundAdjustCoroutine == null)
            _backgroundAdjustCoroutine = StartCoroutine(Adjust());
    }
#endif
}
