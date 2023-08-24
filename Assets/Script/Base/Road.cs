[System.Serializable]

public class Road
{
    public int roadObjectNum;
    public string roadName;
    public int roadNumber;
    public int amount;
    public long price;
    public long gPs;
    public float accident;
    public bool isAccident;
    
    public Road(int roadObjectNum, string roadName, int roadNumber, int amount, long price, long gPs, float accident, bool isAccident)
    {
        this.roadObjectNum = roadObjectNum;
        this.roadName = roadName;
        this.roadNumber = roadNumber;
        this.amount = amount;
        this.price = price;
        this.gPs = gPs;
        this.accident = accident;
        this.isAccident = isAccident;
    }
}
