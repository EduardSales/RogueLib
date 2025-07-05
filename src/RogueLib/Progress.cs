using System.Runtime.InteropServices;

namespace RogueLib
{
    /// <summary>
    /// Represents a record of the player's progress choices,
    /// acting as a log of the path taken through the game.
    /// </summary>
    public struct Progress
    {
        public string description;

        public Progress(string _description)
        {
            description = _description;
        }
        public string GetProgress()
        {
            return description;
        }
    }
}
