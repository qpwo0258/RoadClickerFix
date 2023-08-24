using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyPanel : MonoBehaviour
{
    [SerializeField]
    private Image policyImage = null;
    [SerializeField]
    private Text policyNameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text boostText = null;
    [SerializeField]
    private Text explanationText = null;
    [SerializeField]
    private Sprite[] policySprite = null;

    private Policy policy = null;

    private AudioSource audioSource = null;

    public void SetValue(Policy policy)
    {
        this.policy = policy;
        audioSource = gameObject.GetComponent<AudioSource>();
        UpdatePolicyPanelUI();
    }
    public void UpdatePolicyPanelUI()
    {
        policyImage.sprite = policySprite[policy.policyNumber];
        policyNameText.text = policy.policyName;
        priceText.text = string.Format("{0} ���", policy.price);
        foreach(Road road in GameManager.Instance.currentUser.roadList)
        {
            GameManager.Instance.SetCarSpeed(road.roadObjectNum - 1);
            Text accidentText = GameManager.Instance.UI.accidentTextObjects[road.roadObjectNum - 1].GetComponent<Text>();
            accidentText.text = string.Format("���Ȯ��: {0:N1}%", (float)(road.accident * GameManager.Instance.AccidentBuff));
        }
        explanationText.text = string.Format("����: {0}�� ���Ȯ��: {0}��", policy.goldBuff, policy.accidentBuff);
        if (policy.isBoost == true)
        {
            boostText.color = Color.green;
            boostText.text = string.Format("������");
        }
        else
        {
            boostText.color = Color.gray;
            boostText.text = string.Format("����");
        }
    }
    public void OnClickSelectButton()
    {
        if (policy.price > GameManager.Instance.currentUser.gold || policy.isBoost == true) return;
        audioSource.Play();
        GameManager.Instance.CheckIsBoost(policy.policyType);
        GameManager.Instance.currentUser.gold -= policy.price;
        policy.isBoost = true;
        GameManager.Instance.GetGoldBuff();
        UpdatePolicyPanelUI();
    }
}
