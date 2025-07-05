using System;
using System.Collections.Generic;

namespace RogueLib
{
    /// <summary>
    /// Items you encounter in the run, to add atributes use ItemDefinition
    /// </summary>
    public class Item
    {
        public string name { get;}
        public string type { get; private set; }
        public Dictionary<string, string> attributes { get; private set; } = new Dictionary<string, string>();
        public Item(string _type, string _name, params object[] _attributesValues)
        {
            type = _type;
            name = _name;

            List<string> attributesTypes = ItemDefinition.GetItemTypes(_type);

            if (_attributesValues.Length != attributesTypes.Count)
            {
                throw new ArgumentException("Error");
            }

            for (int i = 0; i<attributesTypes.Count; i++)
            {
                attributes[attributesTypes[i]] = _attributesValues[i].ToString();
            }

        }
        public Item(string _name) 
        {
            name = _name;
        }
    }
}