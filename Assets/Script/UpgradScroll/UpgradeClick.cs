using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeClick : MonoBehaviour
{
    [SerializeField]
    private Text purchaseText = null;
    [SerializeField]
    private GameObject nextPanel = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text explanationText = null;
    private AudioSource audioSource = null;

    private void Start()
    {
        UpdateUpgradePanelUI();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnClickPurchaseButton()
    {
        if (GameManager.Instance.currentUser.gold < GameManager.currentUser.shovelList.price || isMax) return;
        audioSource.Play();
        GameManager.Instance.currentUser.gold -= price;
        GameManager.Instance.currentUser.gPc = (long)(GameManager.Instance.currentUser.gPc * 1.5f);
        price = GameManager.Instance.currentUser.gPc * 105 * amount;
        amount++;
        if (amount == 30)
        {
            isMax = true;
            purchaseText.color = Color.red;
            purchaseText.text = string.Format("MAX");
            if (nextPanel != null) nextPanel.SetActive(true);
        }
        UpdateUpgradePanelUI();
    }
    public void UpdateUpgradePanelUI()
    {
        priceText.text = string.Format("{0} °ñµå", price);
        amountText.text = string.Format("{0} ´Ü°è", amount);
        explanationText.text = string.Format("{0}G/c => {1}G/c", GameManager.Instance.currentUser.gPc, GameManager.Instance.currentUser.gPc * 1.15f);
    }
}
