using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;
using DataManagement.Common;
using ConstCollections.PJEnums.Character;
using ConstCollections.PJPaths;
using DataManagement.SaveData;

namespace DataManagement.TableClass.Hero.TableReaderBase
{
  public class HeroNameTableReaderBase : AbsMulitiLanguageTableReaderBase<HeroNameTable>
  {
    public string GetString(HERO_NAME_PART heroNamePart, ushort ID, SystemLanguage? lang = null)
    {
      AbsMultiLanguageTable _row =  this.FindDefaultUnique (heroNamePart, ID);
      SystemLanguage _lang = lang ?? ConfigDataManager.Instance.UserLanguage;
      return GetString (_row, _lang);
    }

    public string GetString(HeroNameTable heroName, SystemLanguage? lang = null)
    {
      AbsMultiLanguageTable _row = heroName;
      SystemLanguage _lang = lang ?? ConfigDataManager.Instance.UserLanguage;
      return GetString (_row, _lang);
    }

    public override string GetString(ushort ID, SystemLanguage? lang = null)
    {
      throw new System.Exception ("Can not read table without HERO_NAME_PART");
    }

    public override HeroNameTable FindDefaultUnique(ushort ID)
    {
      throw new System.Exception ("Can not read table without HERO_NAME_PART");
    }

    public override HeroNameTable FindDefaultFirst(ushort ID)
    {
      throw new System.Exception ("Can not read table without HERO_NAME_PART");
    }

    public override List<HeroNameTable> FindFromDefault(ushort ID)
    {
      throw new System.Exception ("Can not read table without HERO_NAME_PART");
    }

    public HeroNameTable FindDefaultUnique(HERO_NAME_PART heroNamePart, ushort ID)
    {
      return FindDefaultUnique (HeroNameCombination.TablePathDic[heroNamePart] , ID);
    }

    public HeroNameTable FindDefaultFirst(HERO_NAME_PART heroNamePart, ushort ID)
    {
      return FindFirstByID (HeroNameCombination.TablePathDic[heroNamePart], ID);
    }

    public List<HeroNameTable> FindFromDefault(HERO_NAME_PART heroNamePart, ushort ID)
    {
      return FindByID (HeroNameCombination.TablePathDic[heroNamePart], ID);
    }

    #region NONE_PUBLIC_METHOD
    protected override List<HeroNameTable> GetDefaultCachedList()
    {
      throw new System.Exception ("Can not read table without HERO_NAME_PART");
    }

    protected override List<HeroNameTable> GetDefaultList()
    {
      throw new System.Exception ("Can not read table without HERO_NAME_PART");
    }

    protected override CSVReader<HeroNameTable> GetDefaultRawTable()
    {
      throw new System.Exception ("Can not read table without HERO_NAME_PART");
    }
    #endregion
  }
}
