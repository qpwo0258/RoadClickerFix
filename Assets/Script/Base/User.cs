using System.Collections.Generic;

[System.Serializable]

public class User
{
    public string userName;
    public long gold;
    public long gPc;
    public List<Road> roadList = new List<Road>();
    public List<Policy> policyList = new List<Policy>();
    public List<Shovel> shovelList = new List<Shovel>();
}
