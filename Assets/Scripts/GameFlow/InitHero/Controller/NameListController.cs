using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InitHero.Views;
using DataManagement.TableClass.Hero;
using ConstCollections.PJEnums.Character;
using DataManagement.SaveData;
using DataManagement.TableClass.TableReaderBase;
using ConstCollections.PJPaths;

namespace InitHero.Controllers{

  public class NameListController : MonoBehaviour {

    public int NameSegments = 3;
    public NameOptionView NameView;
    public ushort[] NameIDArray;
    public string PresentName;

    public void Init(ushort nameFormatId)
    {
      NameIDArray = new ushort[NameSegments];
      nameStringFormat = HeroNameFormatTableReader.Instance.GetString(nameFormatId);

      nameListPart0 = new List<HeroNameTable> ();
      nameListPart1 = new List<HeroNameTable> ();
      nameListPart2 = new List<HeroNameTable> ();

      nameListPart0 = HeroNameTableReader.Instance.GetCachedList (HeroNameCombination.TablePathDic[HERO_NAME_PART.PART_0]);
      nameListPart1 = HeroNameTableReader.Instance.GetCachedList (HeroNameCombination.TablePathDic[HERO_NAME_PART.PART_1]);
      nameListPart2 = HeroNameTableReader.Instance.GetCachedList (HeroNameCombination.TablePathDic[HERO_NAME_PART.PART_2]);

      NameView.Init ();
      GetAndShowRandomName ();
    }

    public void GetAndShowRandomName()
    {
      int _namePart0 = Random.Range (0, this.nameListPart0.Count);
      int _namePart1 = Random.Range (0, this.nameListPart1.Count);
      int _namePart2 = Random.Range (0, this.nameListPart2.Count);
      NameIDArray [0] = (ushort)_namePart0;
      NameIDArray [1] = (ushort)_namePart1;
      NameIDArray [2] = (ushort)_namePart2;

      PresentName = GetNameById(NameIDArray);
      NameView.ShowName(PresentName);
    }

    public string GetNameById(ushort[] nameIDArray)
    {
      string _part0 = HeroNameTableReader.Instance.GetString(nameListPart0[(int)nameIDArray[0]]);
      string _part1 = HeroNameTableReader.Instance.GetString(nameListPart1[(int)nameIDArray[1]]);
      string _part2 = HeroNameTableReader.Instance.GetString(nameListPart2[(int)nameIDArray[2]]);

      return string.Format(nameStringFormat,_part0,_part1,_part2);
    }

    List<HeroNameTable> nameListPart0;
    List<HeroNameTable> nameListPart1;
    List<HeroNameTable> nameListPart2;
    string nameStringFormat;


  }
}
