namespace RogueLib
{
    /// <summary>
    /// Collectables you get in the run like coins, keys, bombs...
    /// </summary>
    public class PickUp
    {
        public string name;
        int maxAmmount = 0;
        int currentAmmount = 0;
        
        public PickUp(string _name) 
        { 
            name = _name;
        }
        public void AddToPickUp(int _ammount)
        {
            currentAmmount+=_ammount;
            if (currentAmmount>maxAmmount)
            {
                maxAmmount=currentAmmount;
            }
        }
        public void RemoveFromPickUp(int _ammount) 
        { 
            currentAmmount-=_ammount;
        }
        public int GetAmmount() 
        {
            return currentAmmount;
        }
        public int GetMaxAmmount() 
        {
            return maxAmmount;
        }
    }
}
