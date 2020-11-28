using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class BgGraphicsManager : MonoBehaviour {

    [SerializeField] [SceneObjectsOnly] [BoxGroup("Movement Positions")]
    private RectTransform
        startPoint,
        endPoint;

    [SerializeField] [PropertyRange(1f, 60f)] [BoxGroup("Durations")]
    private float animationDuration;

    [SerializeField] [PropertyRange(0.5f, 5f)] [BoxGroup("Durations")]
    private float fadeDuration;

    [SerializeField]
    private Vector2 fadePosition;

    private GameObject
        _bgGraphic1,
        _bgGraphic2;

    private void Start() {
        SetComponents();
    }

    private void SetComponents() { 
        DOTween.Init(false, true, LogBehaviour.Default);

        
        var startPosition = startPoint.transform.localPosition;
        var endPosition = endPoint.transform.localPosition;
        
        _bgGraphic1 = transform.GetChild(0).gameObject;
        var bgColour1 = _bgGraphic1.GetComponent<Image>().color;
        _bgGraphic1.GetComponent<Image>().color = new Vector4(bgColour1.r, bgColour1.g, bgColour1.b, 0.5f);
        _bgGraphic1.transform.localPosition = startPosition;

        _bgGraphic2 = transform.GetChild(1).gameObject;
        var bgColour2 = _bgGraphic2.GetComponent<Image>().color;
        _bgGraphic2.GetComponent<Image>().color = new Vector4(bgColour2.r, bgColour2.g, bgColour2.b, 0f);
        _bgGraphic2.transform.localPosition = CalculateNewStartPosition(startPosition, endPosition);

        var menuAnimation = DOTween.Sequence();
        menuAnimation.SetLoops(-1);
        PlayAnimation(menuAnimation);
    }

    private void PlayAnimation(Sequence sequence) {
        sequence.Append(_bgGraphic1.transform.DOLocalMove(endPoint.localPosition, animationDuration)
            .SetEase(Ease.Linear));
        FadeToStart(sequence);
    }
    private void FadeToStart(Sequence sequence) {
        sequence.Insert(
            animationDuration - fadeDuration, 
            _bgGraphic1.GetComponent<Image>().DOFade(0, fadeDuration)
                .SetEase(Ease.Linear));
        sequence.Insert(
            animationDuration - fadeDuration, 
            _bgGraphic2.GetComponent<Image>().DOFade(0.5f, fadeDuration)
                .SetEase(Ease.Linear));
        PrepareForLoop(sequence); 
    }

    private void PrepareForLoop(Sequence sequence) {
        sequence.Insert(
            animationDuration - fadeDuration,
            _bgGraphic2.transform.DOLocalMove(startPoint.localPosition, fadeDuration)
                .SetEase(Ease.Linear));
    }

    private Vector2 CalculateNewStartPosition(Vector2 startPosition, Vector2 endPosition) {
        var distance = Vector2.Distance(startPosition, endPosition);
        var time = animationDuration;
        var speed = distance / time;

        var newTime = fadeDuration;
        var fadeDistance = speed * newTime;

        var sign1 = endPosition.y >= 0 ? 1 : -1;
        var offset1 = sign1 >= 0 ? 0 : 360;
        var angle = Vector2.Angle(endPosition, startPosition) * sign1 + offset1;

        var xOffset = Mathf.Tan(angle) * fadeDistance;
        var yOffset = Mathf.Sin(angle) * fadeDistance;

        var calculatedPosition = new Vector2(startPosition.x - xOffset, startPosition.y - yOffset);
        
        var sign2 = startPosition.y >= 0 ? 1 : -1;
        var offset2 = sign2 >= 0 ? 0 : 360;
        
        Debug.Log("Angle from StartPos to FadePos: (" + (Vector2.Angle(startPosition, calculatedPosition) * sign2 + offset2) + ")");
        Debug.Log("Angle from EndPos to StartPos: (" + angle + ")");

        Debug.Log("Fade Position: " + calculatedPosition);
        Debug.Log("Start Position: " + startPosition);
        Debug.Log("End Position: " + endPosition); 
        return calculatedPosition;
    }
}