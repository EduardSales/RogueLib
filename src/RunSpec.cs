namespace RogueLib
{
    /// <summary>
    /// A struct designed to add specifications for the run, like dificulty, mode etc...
    /// </summary>
    public struct RunSpec
    {
        public string Specification;
        public string Description;

        public RunSpec(string _specification, string _description)
        {
            Specification = _specification;
            Description = _description;
        }

        public string GetSpec()
        {
            string specs = $"{Specification}: {Description}";
            return specs;
        }
    }
}
