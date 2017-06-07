using UnityEngine;
using Common;
using System;
using System.Collections;
using ConstCollections.PJEnums;
using DataManagement.Common;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using DataManagement.SaveData;
using DataManagement.TableClass.TableReaderBase;
using ConstCollections.PJEnums.Skill;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJConstStrings;
using DataManagement.TableClass.Skill.Effect;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Trigger;

namespace DataManagement.TableClass.Skill.TableReaderBase
{
  [System.Serializable]
  public class SkillStringsTableReaderBase : AbsMulitiLanguageTableReaderBase<SkillStringsTable> {
    public static string ColumnSkillIDName = "SkillID";
    public static string ColumnLabelName = "Label";

    public override string TablePath {
      get {
        return "Data/Skill/skill_strings.csv";
      }
    }

    public ushort FindID(ushort skillID, SKILL_STRINGS_LABEL label, SystemLanguage? lang = null)
    {
      using(var _reader = FileIO.Instance.CSVReader<SkillStringsTable>(TablePath))
      {
        List<SkillStringsTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _skillIDInfo = _row.GetType ().GetField (ColumnSkillIDName, BindingFlags.Instance | BindingFlags.Public);
          if(_skillIDInfo == null)
            return false;

          FieldInfo _labelInfo = _row.GetType ().GetField (ColumnLabelName, BindingFlags.Instance | BindingFlags.Public);
          if(_labelInfo == null)
            return false;

          return  ((short)_skillIDInfo.GetValue (_row) == skillID) && 
            ((SKILL_STRINGS_LABEL)_labelInfo.GetValue (_row) == label);
        }).ToList ();

        if (_rows.Count == 0)
          throw new System.NullReferenceException ();

        if (_rows.Count > 1)
          throw new System.Exception ("Find Unique but got duplicated!");

        return _rows[0].ID;
      }
    }

    public string GetString(short skillID, SKILL_STRINGS_LABEL label, SystemLanguage? lang = null)
    {

      using(var _reader = FileIO.Instance.CSVReader<SkillStringsTable>(TablePath))
      {
        List<SkillStringsTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _skillIDInfo = _row.GetType ().GetField (ColumnSkillIDName, BindingFlags.Instance | BindingFlags.Public);
          if(_skillIDInfo == null)
            return false;
          
          FieldInfo _labelInfo = _row.GetType ().GetField (ColumnLabelName, BindingFlags.Instance | BindingFlags.Public);
          if(_labelInfo == null)
            return false;

          return  ((short)_skillIDInfo.GetValue (_row) == skillID) && 
            ((SKILL_STRINGS_LABEL)_labelInfo.GetValue (_row) == label);
        }).ToList ();

        if (_rows.Count == 0)
          throw new System.NullReferenceException ();

        if (_rows.Count > 1)
          throw new System.Exception ("Find Unique but got duplicated!");

        SystemLanguage _lang = lang ?? ConfigDataManager.Instance.UserLanguage;
        return GetString (_rows[0], _lang);
      }
    }

    public MultiLangString<SkillStringsTable> GetMultiLangStringWithoutParam(ushort skillID,SKILL_STRINGS_LABEL label)
    {
      ushort _tableID = FindID(skillID,label);

      MultiLangString<SkillStringsTable> _multiLang = new MultiLangString<SkillStringsTable> (_tableID, SkillStringsTableReader.Instance);

      return _multiLang;
    }

    public MultiLangString<SkillStringsTable> GetMultiLangStringWithParam(ushort skillID,int level,SKILL_STRINGS_LABEL label)
    {
      ushort _tableID = FindID(skillID,label);
      MultiLangString<SkillStringsTable> _multiLang;
      var _parameterList = GetSkillDescriptionParameter (skillID,level);
      if (_parameterList.Count > 0) {
        var _parameterArray = _parameterList.Cast<object> ().ToArray ();

        _multiLang = new MultiLangString<SkillStringsTable> (_tableID, SkillStringsTableReader.Instance, _parameterArray);
      }
      else 
      {
        _multiLang = new MultiLangString<SkillStringsTable> (_tableID, SkillStringsTableReader.Instance);
      }

      return _multiLang;
    }


    public List<float> GetSkillDescriptionParameter(ushort skillID,int level)
    {
      
      List<float> _parameterList = new List<float>();
      var _dbSkill = SkillTableReader.Instance.FindDefaultUnique (skillID);
//      SKILL_CODE_NAME _codeName = _dbSkill.CodeName;
      var _skillID = _dbSkill.ID;
      switch (_skillID) 
      {
//        case SKILL_CODE_NAME.Skill0:
      case 0:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float NormCp = _dataOfSkillForDisplay.NormCp;
          float NormSp = _dataOfSkillForDisplay.NormSp;
          _parameterList.Add(NormCp + NormSp * level);
          break;
        }
//        case SKILL_CODE_NAME.Skill1:
      case 1:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float ATKCp = _dataOfSkillForDisplay.ATKCp;
          float ATKSp = _dataOfSkillForDisplay.ATKSp;
          _parameterList.Add((ATKCp + ATKSp * level) * 100);
          break;
        }
//        case SKILL_CODE_NAME.Skill2:
      case 2:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float ATKCp = _dataOfSkillForDisplay.ATKCp;
          float ATKSp = _dataOfSkillForDisplay.ATKSp;
          _parameterList.Add((ATKCp + ATKSp * level) * 100);
          break;
        }
//        case SKILL_CODE_NAME.Skill3:
      case 3:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float ATKCp = _dataOfSkillForDisplay.ATKCp;
          float ATKSp = _dataOfSkillForDisplay.ATKSp;
          _parameterList.Add((ATKCp + ATKSp * level) * 100);
          break;
        }
      case 4:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float ATKCp = _dataOfSkillForDisplay.ATKCp;
          float ATKSp = _dataOfSkillForDisplay.ATKSp;
          _parameterList.Add((ATKCp + ATKSp * level) * 100);
          break;
        }
      case 5:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float VITCp = _dataOfSkillForDisplay.VITCp;
          float VITSp = _dataOfSkillForDisplay.VITSp;
          _parameterList.Add((VITCp + VITSp * level) * 100);
          break;
        }
      case 6:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float VITCp = _dataOfSkillForDisplay.VITCp;
          float VITSp = _dataOfSkillForDisplay.VITSp;
          _parameterList.Add((VITCp + VITSp * level) * 100);
          break;
        }
      case 7:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float VITCp = _dataOfSkillForDisplay.VITCp;
          float VITSp = _dataOfSkillForDisplay.VITSp;
          _parameterList.Add((VITCp + VITSp * level) * 100);
          break;
        }
      case 8:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float MAGCp = _dataOfSkillForDisplay.MAGCp;
          float MAGSp = _dataOfSkillForDisplay.MAGSp;
          _parameterList.Add((MAGCp + MAGSp * level) * 100);
          break;
        }
      case 9:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float MAGCp = _dataOfSkillForDisplay.MAGCp;
          float MAGSp = _dataOfSkillForDisplay.MAGSp;
          _parameterList.Add((MAGCp + MAGSp * level) * 100);

          var _effectDot = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.DOT;
          });
          DotTable _dotData = DotTableReader.Instance.FindDefaultFirst ((ushort)_effectDot.EffectTableID);
          float INTCp = _dotData.INTCp;
          float INTSp = _dotData.INTSp;
          _parameterList.Add((INTCp + INTSp * level) * 100);

          break;
        }
      case 10:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _skillEffect = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });
          StandardAttackPowerTable _dataOfSkillForDisplay = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_skillEffect.EffectTableID);
          float MAGCp = _dataOfSkillForDisplay.MAGCp;
          float MAGSp = _dataOfSkillForDisplay.MAGSp;
          _parameterList.Add((MAGCp + MAGSp * level) * 100);

          var _effectDot = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.DOT;
          });
          DotTable _dotData = DotTableReader.Instance.FindDefaultFirst ((ushort)_effectDot.EffectTableID);
          float INTCp = _dotData.INTCp;
          float INTSp = _dotData.INTSp;
          _parameterList.Add((INTCp + INTSp * level) * 100);

          break;
        }
      case 11:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectDot = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.DOT;
          });

          DotTable _dotData = DotTableReader.Instance.FindDefaultFirst ((ushort)_effectDot.EffectTableID);
          float INTCp = _dotData.INTCp;
          float INTSp = _dotData.INTSp;
          _parameterList.Add((INTCp + INTSp * level) * 100);
      
          break;
        }
      case 12:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectDot = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_HP;
          });

          ChangeHPTable _changeHPData = ChangeHPTableReader.Instance.FindDefaultFirst ((ushort)_effectDot.EffectTableID);
          float INTCp = _changeHPData.INTCp;
          float INTSp = _changeHPData.INTSp;
          _parameterList.Add((INTCp + INTSp * level) * 100);

          break;
        }
      case 13:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectDot = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_HP;
          });

          ChangeHPTable _changeHPData = ChangeHPTableReader.Instance.FindDefaultFirst ((ushort)_effectDot.EffectTableID);
          float INTCp = _changeHPData.INTCp;
          float INTSp = _changeHPData.INTSp;
          _parameterList.Add((INTCp + INTSp * level) * 100);

          break;
        }
      case 14:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectDot = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_HP;
          });

          ChangeHPTable _changeHPData = ChangeHPTableReader.Instance.FindDefaultFirst ((ushort)_effectDot.EffectTableID);
          float INTCp = _changeHPData.INTCp;
          float INTSp = _changeHPData.INTSp;
          _parameterList.Add((INTCp + INTSp * level) * 100);

          break;
        }
      case 15:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectDot = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.REINFORCE;
          });

          ReinforceTable _reinforceData = ReinforceTableReader.Instance.FindDefaultFirst ((ushort)_effectDot.EffectTableID);
          float CRISp = _reinforceData.CRISp;
          _parameterList.Add(CRISp * level);

          break;
        }
      case 16:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectChangeHP = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_HP;
          });

          ChangeHPTable _changeHPData = ChangeHPTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeHP.EffectTableID);
          float NormCp = _changeHPData.NormCp;
          float NormSp = _changeHPData.NormSp;
          _parameterList.Add(NormCp +  NormSp * level);

          var _effectChangeAttribute = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_ATTRIBUTE;
          });

          ChangeAttributeTable _changeAttributeData = ChangeAttributeTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeAttribute.EffectTableID);
          float ATKCp = _changeAttributeData.ATKCp;
          float ATKSp = _changeAttributeData.ATKSp;
          _parameterList.Add(ATKCp +  ATKSp * level);

          float DEFCp = _changeAttributeData.DEFCp;
          float DEFSp = _changeAttributeData.DEFSp;
          _parameterList.Add(DEFCp +  DEFSp * level);

          break;
        }
      case 17:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectChangeHP = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_HP;
          });

          ChangeHPTable _changeHPData = ChangeHPTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeHP.EffectTableID);
          float NormCp = _changeHPData.NormCp;
          float NormSp = _changeHPData.NormSp;
          _parameterList.Add(NormCp +  NormSp * level);

          var _effectChangeAttribute = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_ATTRIBUTE;
          });

          ChangeAttributeTable _changeAttributeData = ChangeAttributeTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeAttribute.EffectTableID);
          float ATKCp = _changeAttributeData.ATKCp;
          float ATKSp = _changeAttributeData.ATKSp;
          _parameterList.Add(ATKCp +  ATKSp * level);

          float DEFCp = _changeAttributeData.DEFCp;
          float DEFSp = _changeAttributeData.DEFSp;
          _parameterList.Add(DEFCp +  DEFSp * level);

          break;
        }

      case 18:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectChangeHP = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_HP;
          });

          ChangeHPTable _changeHPData = ChangeHPTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeHP.EffectTableID);
          float NormCp = _changeHPData.NormCp;
          float NormSp = _changeHPData.NormSp;
          _parameterList.Add(NormCp +  NormSp * level);

          var _effectChangeAttribute = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_ATTRIBUTE;
          });

          ChangeAttributeTable _changeAttributeData = ChangeAttributeTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeAttribute.EffectTableID);
          float ATKCp = _changeAttributeData.ATKCp;
          float ATKSp = _changeAttributeData.ATKSp;
          _parameterList.Add(ATKCp +  ATKSp * level);

          float DEFCp = _changeAttributeData.DEFCp;
          float DEFSp = _changeAttributeData.DEFSp;
          _parameterList.Add(DEFCp +  DEFSp * level);

          break;
        }
      case 19:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectStandPower = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });

          var _standAttackPowerData = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_effectStandPower.EffectTableID);
          float ATKCp = _standAttackPowerData.ATKCp;
          float ATKSp = _standAttackPowerData.ATKSp;
          _parameterList.Add((ATKCp +  ATKSp * level) * 100);

          var _effectChangeAttribute = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_ATTRIBUTE;
          });

          ChangeAttributeTable _changeAttributeData = ChangeAttributeTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeAttribute.EffectTableID);
          float ACCP = _changeAttributeData.ACCp;
          float ACSP = _changeAttributeData.ACSp;
          _parameterList.Add(ACCP +  ACSP * level);

          break;
        }
      case 20:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectChangeDamage = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_DAMAGE;
          });

          var _changeDamageData = ChangeDamageTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeDamage.EffectTableID);
          float NormCp = _changeDamageData.NormCp;
          float NormSp = _changeDamageData.NormSp;
          _parameterList.Add(NormCp +  NormSp * level);
          break;
        }
      case 21:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectChangeDamage = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_DAMAGE;
          });

          var _changeDamageData = ChangeDamageTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeDamage.EffectTableID);
          float NormCp = _changeDamageData.NormCp;
          float NormSp = _changeDamageData.NormSp;
          _parameterList.Add(NormCp +  NormSp * level);
          break;
        }
      case 22:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectLastStandAttackPower = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.LAST_STAND_ATTACK_POWER;
          });

          var _lastStandAttackData = LastStandAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_effectLastStandAttackPower.EffectTableID);
          float NormCp = _lastStandAttackData.NormCp;
          float NormSp = _lastStandAttackData.NormSp;
          _parameterList.Add(NormCp +  NormSp * level);
          break;
        }
      case 23:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectStandAttackPower = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });

          var _standAttackPowerData = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_effectStandAttackPower.EffectTableID);
          float ATKCp = _standAttackPowerData.ATKCp;
          float ATKSp = _standAttackPowerData.ATKSp;
          _parameterList.Add((ATKCp + ATKSp * level) * 100);
          break;
        }
      case 24:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectStandAttackPower = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });

          var _standAttackPowerData = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_effectStandAttackPower.EffectTableID);
          float ATKCp = _standAttackPowerData.ATKCp;
          float ATKSp = _standAttackPowerData.ATKSp;

          _parameterList.Add((ATKCp + ATKSp * level) * 100);
          break;
        }
      case 25:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectStandAttackPower = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });

          var _standAttackPowerData = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_effectStandAttackPower.EffectTableID);
          float ATKCp = _standAttackPowerData.ATKCp;
          float ATKSp = _standAttackPowerData.ATKSp;

          _parameterList.Add((ATKCp + ATKSp * level) * 100);
          break;
        }
      case 26:
        {
          //FIXME: the skill is not implemented
          break;
        }
      case 27:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectStandAttackPower = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });

          var _standAttackPowerData = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_effectStandAttackPower.EffectTableID);
          float MAGCp = _standAttackPowerData.MAGCp;
          float MAGSp = _standAttackPowerData.MAGSp;

          _parameterList.Add((MAGCp + MAGSp * level) * 100);

          var _effectChangeAttribute = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_ATTRIBUTE;
          });
          var _changeAttributeData = ChangeAttributeTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeAttribute.EffectTableID);
          float AVDCp = _changeAttributeData.AVDCp;
          float AVDSp = _changeAttributeData.AVDSp;
          float HITCp = _changeAttributeData.HITCp;
          float HITSp = _changeAttributeData.HITSp;
          _parameterList.Add(AVDCp + AVDSp * level);
          _parameterList.Add(HITCp + HITSp * level);
          break;
        }
      case 28:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectStandAttackPower = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER;
          });

          var _standAttackPowerData = StandardAttackPowerTableReader.Instance.FindDefaultFirst ((ushort)_effectStandAttackPower.EffectTableID);
          float MAGCp = _standAttackPowerData.MAGCp;
          float MAGSp = _standAttackPowerData.MAGSp;

          _parameterList.Add((MAGCp + MAGSp * level) * 100);

          var _effectChangeAttribute = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.CHANGE_ATTRIBUTE;
          });
          var _changeAttributeData = ChangeAttributeTableReader.Instance.FindDefaultFirst ((ushort)_effectChangeAttribute.EffectTableID);
          float AVDCp = _changeAttributeData.AVDCp;
          float AVDSp = _changeAttributeData.AVDSp;
          float HITCp = _changeAttributeData.HITCp;
          float HITSp = _changeAttributeData.HITSp;
          float CRICp = _changeAttributeData.CRICp;
          float CRISp = _changeAttributeData.CRISp;
          _parameterList.Add(AVDCp + AVDSp * level);
          _parameterList.Add(HITCp + HITSp * level);
          _parameterList.Add(CRICp + CRISp * level);
          break;
        }
      case 29:
        {
          //FIXME : the skill is not implemented
          break;
        }
      case 30:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectReinforce = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.REINFORCE;
          });

          var _effectReinforceData = ReinforceTableReader.Instance.FindDefaultFirst ((ushort)_effectReinforce.EffectTableID);
          float ACSp = _effectReinforceData.ACSp;

          _parameterList.Add(ACSp * level);

          break;
        }
      case 31:
        {
          var _skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _effectReinforce = _skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.REINFORCE;
          });

          var _effectReinforceData = ReinforceTableReader.Instance.FindDefaultFirst ((ushort)_effectReinforce.EffectTableID);
          float RESSp = _effectReinforceData.RESSp;

          _parameterList.Add(RESSp * level);

          break;
        }
      case 32:
        {
          var _skillTriggerList = SkillTriggerTableReader.Instance.FindBySkillID (skillID);
          var _trigger = _skillTriggerList.Find(row => {
            return row.TriggerType == SKILL_TRIGGER_TYPE.PROBABILITY;
          });

          var _probability = ProbabilityTableReader.Instance.FindDefaultFirst ((ushort)_trigger.TriggerTableID);
          float _Cp0 = _probability.Cp_0;
          float _Cp1 = _probability.Cp_1;

          _parameterList.Add(_Cp0 +  _Cp1 * level);

          break;
        }
      case 33:
        {
          var _skillTriggerList = SkillTriggerTableReader.Instance.FindBySkillID (skillID);
          var _trigger = _skillTriggerList.Find(row => {
            return row.TriggerType == SKILL_TRIGGER_TYPE.PROBABILITY;
          });

          var _probability = ProbabilityTableReader.Instance.FindDefaultFirst ((ushort)_trigger.TriggerTableID);
          float _Cp0 = _probability.Cp_0;
          float _Cp1 = _probability.Cp_1;

          _parameterList.Add(_Cp0 +  _Cp1 * level);

          break;
        }
      case 34:
        {
          //FIXME : the skill is not implemented,do it late

          break;
        }
      case 35:
        {
          var _skillTriggerList = SkillTriggerTableReader.Instance.FindBySkillID (skillID);
          var _trigger = _skillTriggerList.Find(row => {
            return row.TriggerType == SKILL_TRIGGER_TYPE.PROBABILITY;
          });

          var _probability = ProbabilityTableReader.Instance.FindDefaultFirst ((ushort)_trigger.TriggerTableID);
          float _Cp0 = _probability.Cp_0;
          float _Cp1 = _probability.Cp_1;

          _parameterList.Add(_Cp0 +  _Cp1 * level);

          break;
        }
      case 36:
        {
          var _skillTriggerList = SkillTriggerTableReader.Instance.FindBySkillID (skillID);
          var _trigger = _skillTriggerList.Find(row => {
            return row.TriggerType == SKILL_TRIGGER_TYPE.PROBABILITY;
          });

          var _probability = ProbabilityTableReader.Instance.FindDefaultFirst ((ushort)_trigger.TriggerTableID);
          float _Cp0 = _probability.Cp_0;
          float _Cp1 = _probability.Cp_1;

          _parameterList.Add(_Cp0 +  _Cp1 * level);

          break;
        }
      case 37:
        {
          
          //FIXME : the skill is not implemented,do it late
          break;
        }
      case 38:
        {
          var skillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
          var _reinforce = skillEffectList.Find(row => {
            return row.EffectType == SKILL_EFFECT_TYPE.REINFORCE;
          });
          var _reinforceData = ReinforceTableReader.Instance.FindDefaultFirst ((ushort)_reinforce.EffectTableID);

          float _HPMaxSp = _reinforceData.HPMaxSp;

          _parameterList.Add(_HPMaxSp * level);

          break;
        }
      case 39:
        {
          var _skillTriggerList = SkillTriggerTableReader.Instance.FindBySkillID (skillID);
          var _trigger = _skillTriggerList.Find(row => {
            return row.TriggerType == SKILL_TRIGGER_TYPE.PROBABILITY;
          });

          var _probability = ProbabilityTableReader.Instance.FindDefaultFirst ((ushort)_trigger.TriggerTableID);
          float _Cp0 = _probability.Cp_0;
          float _Cp1 = _probability.Cp_1;

          _parameterList.Add(_Cp0 +  _Cp1 * level);

          break;
        }
      case 40:
        {
          var _skillTriggerList = SkillTriggerTableReader.Instance.FindBySkillID (skillID);
          var _trigger = _skillTriggerList.Find(row => {
            return row.TriggerType == SKILL_TRIGGER_TYPE.PROBABILITY;
          });

          var _probability = ProbabilityTableReader.Instance.FindDefaultFirst ((ushort)_trigger.TriggerTableID);
          float _Cp0 = _probability.Cp_0;
          float _Cp1 = _probability.Cp_1;

          _parameterList.Add(_Cp0 +  _Cp1 * level);

          break;
        }
      case 41:
        {
          //FIXME : the skill is not implemented,do it late

          break;
        }
      case 42:
        {
          //FIXME : the skill is not implemented,do it late

          break;
        }
      case 43:
        {

          //FIXME : the skill is not implemented,do it late
          break;
        }
      case 44:
        {

          var _trickLearning = TrickLearningTableReader.Instance.FindDefaultFirst ((ushort)0);
          float _Cp0 = _trickLearning.Cp_0;

          _parameterList.Add(_Cp0 * 100);

          break;
        }

      default:
        {
          break;
        }
      }

      return _parameterList;
    }

  }
}
