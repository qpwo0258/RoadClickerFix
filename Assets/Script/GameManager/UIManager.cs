using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text goldText = null;
    [SerializeField]
    private GoldText goldTextTemplate = null;
    [SerializeField]
    private GameObject upgradePanelTemplate = null;
    [SerializeField]
    private GameObject policyPanelTemplate = null;
    [SerializeField]
    public GameObject[] roadObjects = null;
    [SerializeField]
    public GameObject[] accidentMarks = null;
    [SerializeField]
    public GameObject[] accidentTextObjects = null;
    private AudioSource audioSource = null;

    private List<GameObject> policyPanelList = new List<GameObject>();

    private bool isClicked = false;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        UpdateGoldUI();
        CreateUpgradePanels();
        CreatePolicyPanels();
    }

    public void CheckRoadActive()
    {
        foreach (Road road in GameManager.Instance.currentUser.roadList)
        {
            if (road.amount >= 1) roadObjects[road.roadObjectNum - 1].SetActive(true);
        }
    }
    public void CreateUpgradePanels()
    {
        GameObject newPanel = null;
        UpgradePanel newPanelComponent = null;

        foreach (Road road in GameManager.Instance.currentUser.roadList)
        {
            newPanel = Instantiate(upgradePanelTemplate, upgradePanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgradePanel>();
            newPanelComponent.SetValue(road);
            newPanel.SetActive(true);
        }
    }
    public void CreatePolicyPanels()
    {
        PolicyPanel newPanelComponent = null;
        foreach (Policy policy in GameManager.Instance.currentUser.policyList)
        {
            policyPanelList.Insert(policy.policyNumber, Instantiate(policyPanelTemplate, policyPanelTemplate.transform.parent));
            newPanelComponent = policyPanelList[policy.policyNumber].GetComponent<PolicyPanel>();
            newPanelComponent.SetValue(policy);
            policyPanelList[policy.policyNumber].SetActive(true);
        }
    }

    public void UpdateAllPolicy()//정책 패널들의 isBoost를 체크한 다음 같은 타입이면 false한다
    {
        PolicyPanel PanelComponent = null;
        foreach (Policy policy in GameManager.Instance.currentUser.policyList)
        {
            PanelComponent = policyPanelList[policy.policyNumber].GetComponent<PolicyPanel>();
            PanelComponent.SetValue(policy);
        }
    }

    public void OnClickScreen()
    {
        if (isClicked) return;
        StartCoroutine(CheckIsClicked());
        GameManager.Instance.currentUser.gold += (long)(GameManager.Instance.currentUser.gPc * GameManager.Instance.GoldBuff);
        audioSource.Play();
        UpdateGoldUI();

        GoldText newText = null;

        if (GameManager.Instance.Pool.childCount > 0)
        {
            newText = GameManager.Instance.Pool.GetChild(0).GetComponent<GoldText>();
            newText.transform.SetParent(goldTextTemplate.transform.parent);
        }
        else
        {
            newText = Instantiate(goldTextTemplate, goldTextTemplate.transform.parent);
        }
        newText.Show(Input.mousePosition);
    }

    private IEnumerator CheckIsClicked()
    {
        isClicked = true;
        yield return new WaitForSeconds(0.09f);
        isClicked = false;
    }
    public void UpdateGoldUI()
    {
        goldText.text = string.Format("{0} 골드", GameManager.Instance.currentUser.gold);
    }
}
