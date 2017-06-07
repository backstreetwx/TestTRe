using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Character;

namespace DataManagement.GameData.FormatCollection.Battle
{
  [System.Serializable]
  public class CharacterSimpleDataFormat
  {
    public static string NAME = "Character Simple Data";
    public static string MEMORY_SPACE = "Battle Area";

    public CHARACTER_TYPE Type;
    public short SlotID;

    public CharacterSimpleDataFormat(CHARACTER_TYPE type, short slotID)
    {
      this.Type = type;
      this.SlotID = slotID;
    }
  }
}
