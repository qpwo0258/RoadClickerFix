using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    public User currentUser { get; private set; }

    private UIManager uiManager = null;
    public UIManager UI { get { return uiManager; } }

    [SerializeField]
    private Transform pool = null;
    public Transform Pool { get { return pool; } }

    private float refairDelay = 10f;
    public float RefairDelay { get { return refairDelay; } }

    private float goldBuff = 1f;
    public float GoldBuff { get { return goldBuff; } }

    private float accidentBuff = 1f;
    public float AccidentBuff { get { return accidentBuff; } }

    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField]
    private float[] carSpeed = new float[4];
    public float[] CarSpeed { get { return carSpeed; } }

    private void Awake()
    {
        SAVE_PATH = Application.persistentDataPath;
        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();
        uiManager = GetComponent<UIManager>();
        uiManager.CheckRoadActive();
        InvokeRepeating("SaveToJson", 1f, 5f);
        InvokeRepeating("EarnGoldPerSecond", 0f, 1f);
        StartCoroutine(CheckAccident());
    }
    public void LoadFromJson()
    {


        Debug.Log("LoadFromJson");
        if (File.Exists(string.Concat(SAVE_PATH, SAVE_FILENAME)))
        {
            Debug.Log("LoadFromJsonIf");
            string jsonString = File.ReadAllText(string.Concat(SAVE_PATH, SAVE_FILENAME));
            currentUser = JsonUtility.FromJson<User>(jsonString);
        }
        // 파일이 존재하지 않을 경우 새로 생성
        else
        {
            Debug.Log("LoadFromJsonElse");
            currentUser = new User();
            currentUser.userName = "최원빈";
            currentUser.gold = 0;
            currentUser.gPc = 10;

            currentUser.roadList = new List<Road>();
            currentUser.roadList.Add(new Road(1, "1번도로 건설", 0, 0, 5000, 0, 10f, false));
            currentUser.roadList.Add(new Road(2, "2번도로 건설", 0, 0, 50000, 0, 10f, false));
            currentUser.roadList.Add(new Road(3, "3번도로 건설", 0, 0, 1000000, 0, 10f, false));
            currentUser.roadList.Add(new Road(3, "3번도로 건설", 0, 0, 100000000, 0, 10f, false));

            currentUser.policyList = new List<Policy>();
            currentUser.policyList.Add(new Policy("SpeedLimit", "속도제한 60", 0, false, 5000, 0.7f, 0.7f));
            currentUser.policyList.Add(new Policy("SpeedLimit", "속도제한 100", 0, true, 5000, 1f, 1f));
            currentUser.policyList.Add(new Policy("SpeedLimit", "속도제한 없음", 0, false, 5000, 1.5f, 1.5f));
            currentUser.policyList.Add(new Policy("LawLimit", "평화약속", 0, true, 5000, 1f, 1f));
            currentUser.policyList.Add(new Policy("LawLimit", "무법지대", 0, false, 5000, 2f, 4f));

            currentUser.shovelList.Add(new Shovel(0, 1050, false));
            SaveToJson();
        }
    }

    private void SaveToJson()
    {
        string json = JsonUtility.ToJson(currentUser, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    public void EarnGoldPerSecond()
    {
        foreach (Road road in currentUser.roadList)
        {
            if (road.isAccident) continue;
            currentUser.gold += (long)(road.gPs * goldBuff);
        }
        UI.UpdateGoldUI();
    }

    public void OnApplicationQuit()
    {
        SaveToJson();
    }

    public void GetGoldBuff()
    {
        goldBuff = 1f;
        accidentBuff = 1f;
        foreach (Policy policy in currentUser.policyList)
        {
            if (policy.isBoost)
            {
                goldBuff *= policy.goldBuff;
                accidentBuff *= policy.accidentBuff;
            }
        };
    }
    public IEnumerator CheckAccident()
    {
        int happen;
        while (true)
        {
            foreach (Road road in currentUser.roadList)
            {
                if (road.isAccident || road.amount == 0) continue;
                happen = Random.Range(1, 100);
                if (happen <= road.accident * accidentBuff)
                {
                    road.isAccident = true;
                    uiManager.accidentMarks[road.roadObjectNum - 1].SetActive(true);
                    yield return new WaitForSeconds(refairDelay);
                    road.isAccident = false;
                    uiManager.accidentMarks[road.roadObjectNum - 1].SetActive(false);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void CheckIsBoost(string policyType)
    {
        foreach (Policy policy in currentUser.policyList)
        {
            if (policy.isBoost == true && policy.policyType == policyType)
            {
                policy.isBoost = false;
            }
        }
        UI.UpdateAllPolicy();
    }

    public void SetCarSpeed(int objectNum)
    {
        carSpeed[objectNum] = (((float)(currentUser.roadList[objectNum].amount)) * 0.158f * currentUser.policyList[objectNum].goldBuff) + 1f;
    }
}