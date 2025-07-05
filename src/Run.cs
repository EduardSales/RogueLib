using System.Collections.Generic;

//find in list by name
//https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.list-1.find?view=net-8.0

namespace RogueLib
{
    /// <summary>
    /// Defines what  a run is, and stores some data
    /// </summary>
    public class Run
    {
        public string player;

        public string seed;
        public static int numberOfRuns = 0;
        public int runID;

        public int numberOfDeaths = 0;

        public List<RunSpec> specs;
        public List<PickUp> pickUps;
        public List<Item> itemList;
        public List<Highlight> highlights;
        public List<Error> errors;
        public List<Progress> progress;

        public string endMessage;


        //the character, deck of cards, the think that the player plays as, if there is any
        public Run(string _player = null) 
        {
            player = _player;
            runID = numberOfRuns;
            numberOfRuns++;

            specs = new List<RunSpec>();
            pickUps = new List<PickUp>();
            itemList = new List<Item>();
            highlights = new List<Highlight>();
            errors = new List<Error>();
            progress = new List<Progress>();
        }

        #region Specs
        public void AddRunSpecs(string _spec, string _description)
        {
            specs.Add(new RunSpec(_spec, _description));
        }
        #endregion Specs
        #region Hightlight
        public void AddRunHightlight(string _name, string _description)
        {
            highlights.Add(new Highlight(_name, _description));
        }
        public void AddRunHightlight(string _name)
        {
            highlights.Add(new Highlight(_name));
        }
        #endregion Hightlight
        #region Items
        public void AddItem(Item item)
        {
            itemList.Add(item);
        }
        public void RemoveItem(Item item) 
        { 
            itemList.Remove(item);
        }
        #endregion Items
        #region Seed
        public void SetSeed(string _seed)
        {
            seed = _seed;
        }
        #endregion Seed
        #region PickUps
        public PickUp GetPickUp(string _name)
        {
            //if it doesnt exist we create it
            if(!pickUps.Exists(x => x.name == _name))
            {
                pickUps.Add(new PickUp(_name));
            }
            //then we return it
            return pickUps.Find(x => x.name == _name); //return null if not found
        }
        #endregion PickUps
        #region EndMessage
        public void SetEndMessage(string _message)
        {
            endMessage = _message;
        }
        #endregion EndMessage
        #region Error
        public void AddError(string message)
        {
             errors.Add(new Error(message));
        }
        #endregion Error

        #region Progress
        public void AddProgress(string _description)
        {
            progress.Add(new Progress(_description));
        }
        #endregion Progress

        public void AddDeaths(int value)
        {
            numberOfDeaths+= value;
        }
    }
}
