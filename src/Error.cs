namespace RogueLib
{
    /// <summary>
    /// A struct to add errors that you encounter in the run.
    /// </summary>
    public struct Error
    {
        public string message;

        public Error(string _message)
        {
            message = _message;
        }
        public string GetErrorMessage()
        {
            return message;
        }
    }
}
