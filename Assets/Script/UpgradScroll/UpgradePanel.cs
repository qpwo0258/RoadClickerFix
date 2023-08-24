using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Image roadImage = null;
    [SerializeField]
    private Text roadNameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text explanationText = null;
    [SerializeField]
    private Text purchaseButtonText = null;

    [SerializeField]
    private Sprite[] roadSpriteUI = null;
    [SerializeField]
    private Sprite[] roadSprites = null;

    private RoadObject roadComponent = null;
    private Text accidentText = null;

    private Road road = null;

    private AudioSource audioSource = null;
    private bool isMax = false;



    public void SetValue(Road road)
    {
        this.road = road;
        roadComponent = GameManager.Instance.UI.roadObjects[road.roadObjectNum - 1].GetComponent<RoadObject>();
        accidentText = GameManager.Instance.UI.accidentTextObjects[road.roadObjectNum - 1].GetComponent<Text>();
        audioSource = gameObject.GetComponent<AudioSource>();
        UpdateUpgradePanelUI();
    }
    public void UpdateUpgradePanelUI()
    {
        long nextGps = 0;
        roadImage.sprite = roadSpriteUI[road.roadNumber];
        roadNameText.text = road.roadName;
        priceText.text = string.Format("{0} 골드", road.price);
        amountText.text = string.Format("{0} 단계", road.amount);
        accidentText.text = string.Format("사고확률: {0:N1}%", (float)(road.accident * GameManager.Instance.AccidentBuff));
        nextGps = (long)(road.gPs * 1.15);
        if (road.amount == 0)
        {
            nextGps = 5;
        }
        explanationText.text = string.Format("{0}G/s => {1}G/s", road.gPs, nextGps);
    }
    public void OnClickPurchase()
    {
        if (GameManager.Instance.currentUser.gold < road.price || isMax) return;
        audioSource.Play();
        GameManager.Instance.currentUser.gold -= road.price;
        road.amount++;
        switch (road.amount)
        {
            case 1:
                road.gPs = 60;
                GameManager.Instance.UI.accidentTextObjects[road.roadObjectNum - 1].SetActive(true);
                GameManager.Instance.UI.roadObjects[road.roadObjectNum - 1].SetActive(true);
                roadComponent.StartSpawnCar(road);
                road.roadName = string.Format("{0}번도로 개선", road.roadObjectNum);
                break;
            case 30:
                road.roadNumber++;
                SpriteRenderer spriteRenderer = roadComponent.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = roadSprites[road.roadNumber];
                goto default;
            case 60:
                road.roadNumber++;
                spriteRenderer = roadComponent.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = roadSprites[road.roadNumber];
                goto default;
            case 90:
                UpdateUpgradePanelUI();
                purchaseButtonText.color = Color.red;
                purchaseButtonText.text = string.Format("MAX");
                amountText.color = Color.green;
                priceText.text = string.Format("");
                explanationText.text = string.Format("");
                isMax = true;
                return;
            default:
                road.gPs = (long)(road.gPs * 1.17);
                break;
        }
        GameManager.Instance.SetCarSpeed(road.roadObjectNum - 1);
        if (road.amount != 1) road.accident -= (float)(road.accident / 30);
        road.price = (long)(road.gPs * 30);
        UpdateUpgradePanelUI();
        GameManager.Instance.UI.UpdateGoldUI();
    }
}
