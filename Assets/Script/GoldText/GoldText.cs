using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GoldText : MonoBehaviour
{
    private Text goldText = null;

    public void Show(Vector2 mousePosition)
    {
        goldText = GetComponent<Text>();
        goldText.text = string.Format("+{0}", GameManager.Instance.currentUser.gPc * GameManager.Instance.GoldBuff);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        gameObject.SetActive(true);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 50f;

        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
        goldText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
    }

    private void Despawn()
    {
        goldText.DOFade(1f, 0f);
        transform.SetParent(GameManager.Instance.Pool);
        gameObject.SetActive(false);
    }
}
