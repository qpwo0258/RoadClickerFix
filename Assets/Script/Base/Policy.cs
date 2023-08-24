[System.Serializable]

public class Policy
{
    public string policyType;
    public string policyName;
    public int policyNumber;
    public bool isBoost;
    public long price;
    public float goldBuff;
    public float accidentBuff;

    public Policy(string type, string name, int Number, bool isBoost, long price, float goldBuff, float accidentBuff)
    {
        this.policyType = type;
        this.policyName = name;
        this.policyNumber = Number;
        this.isBoost = isBoost;
        this.price = price;
        this.goldBuff = goldBuff;
        this.accidentBuff = accidentBuff;
    }
}

