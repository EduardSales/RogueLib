using System.Collections.Generic;
using System.Linq;

//https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters#params-modifier
//https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0

namespace RogueLib
{
    /// <summary>
    /// Adds atributes to item types you define
    /// </summary>
    public static class ItemDefinition
    {
        private static Dictionary<string, List<string>> Schemas = new Dictionary<string, List<string>>();

        public static void DefineItemTypes(string _itemType, params string[] _attributes)
        {
            Schemas[_itemType] = _attributes.ToList();
        }

        public static List<string> GetItemTypes(string _itemType)
        {
            if (Schemas.TryGetValue(_itemType, out var _attributes))
            {
                return _attributes;
            }
            else 
            {
                return new List<string>();
            }
        }
    }
}
