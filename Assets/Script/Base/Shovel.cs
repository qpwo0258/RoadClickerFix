[System.Serializable]
public class Shovel
{
    public int amount;
    public int price;
    public bool isMax;
        public Shovel(int amount, int price, bool isMax)
        {
            this.amount = amount;
            this.price = price;
            this.isMax = isMax;
        }
}