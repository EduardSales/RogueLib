namespace RogueLib
{
    /// <summary>
    /// A struct to add important moments or achievements.
    /// </summary>
    public struct Highlight
    {
        public string name;
    
        public string description;

        public Highlight(string _name, string _description)
        {
            name = _name;
            description = _description;
        }
        public Highlight(string _name)
        {
            name = _name;
            description= null;
        }

        public string GetHighlight() 
        {
            string highlight;
            if (description==null)
            {
                highlight = $"{name}";
            }
            else
            {
                highlight = $"{name}: {description}";
            }
            return highlight;
        }
    }
}
