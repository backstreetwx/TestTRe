using UnityEngine;
using System.Collections;
using Common;
using GameFlow.Battle.View;
using GameFlow.Battle.Common.Controller;
using ConstCollections.PJEnums.Battle;
using DataManagement.TableClass.BattleInfo;
using System.Linq;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData.FormatCollection;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass;
//using System.Collections.Generic;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.GameData.FormatCollection.Battle;
using DataManagement.GameData.FormatCollection.Skill.Effect;

namespace GameFlow.Battle.Controller
{
  public class BattleInfoManager : SingletonObject<BattleInfoManager> {

    public BattleTextView View;

    public Queue StringQueue;

    public int DefenderSpace = 2;

    public bool HasPosted;

    protected override void Awake()
    {
      base.Awake ();
      this.StringQueue = new Queue ();
      this.HasPosted = false;
    }

    public void ClearOneTurnBuffer()
    {
      this.StringQueue.Clear ();
    }

    public void Show(bool needSpaceLine = true)
    {
      if (this.StringQueue.Count == 0)
        return;

      if (needSpaceLine && this.HasPosted) 
      {
        var _id = BattleStringFormatTableReader.Instance.FindID (INFO_FORMAT_LABEL.BLANK_LINE);
        var _blankFormat = new MultiLangString<BattleStringFormatTable> (
          _id, 
          BattleStringFormatTableReader.Instance);
        this.View.BackMessageQueue.Enqueue (_blankFormat);
      }
      
      while (this.StringQueue.Count != 0) 
      {
        this.View.BackMessageQueue.Enqueue (this.StringQueue.Dequeue());

      }

      this.HasPosted = true;
    }

    public void EnqueueMessage(INFO_FORMAT_LABEL formatLabel)
    {
      IMultiLangString<AbsMultiLanguageTable> _strFormat = null;
      var _id = BattleStringFormatTableReader.Instance.FindID (formatLabel);

      _strFormat = new MultiLangString<BattleStringFormatTable> (
        _id, 
        BattleStringFormatTableReader.Instance);

      if(_strFormat != null)
        this.StringQueue.Enqueue(_strFormat);
    }

    public void EnqueueMessage(INFO_FORMAT_LABEL formatLabel, FightDataFormat attackerData, FightDataFormat defenderData = null, params object[] args)
    {
      IMultiLangString<AbsMultiLanguageTable> _strFormat = null;
      var _id = BattleStringFormatTableReader.Instance.FindID (formatLabel);

      switch (formatLabel) 
      {
      case INFO_FORMAT_LABEL.ACTIVE_ATTACK:
        {
          var _nameString = GenerateCharacterName (attackerData);
          _strFormat = new MultiLangString<BattleStringFormatTable> (
                           _id, 
                           BattleStringFormatTableReader.Instance,
                           _nameString);
          break;
        }

      case INFO_FORMAT_LABEL.AVOID_ATTACK:
        {
          var _nameString = GenerateCharacterName (defenderData);
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString);
          break;
        }
      case INFO_FORMAT_LABEL.GET_DAMAGE:
        {
          var _defenderNameString = GenerateCharacterName (defenderData);
          var _defenderDamageString = GenerateDamage (attackerData, defenderData);
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _defenderNameString, _defenderDamageString);
          break;
        }
      case INFO_FORMAT_LABEL.IS_DEAD:
        {
          var _defenderNameString = GenerateCharacterName (defenderData);
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _defenderNameString);
          break;
        }
      case INFO_FORMAT_LABEL.GOT_EXP:
        {
          //var _nameString = GenerateCharacterName (heroAttributes);
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            //_nameString,
            args);
          break;
        }
      case INFO_FORMAT_LABEL.LEVEL_UP:
        {
          var _nameString = GenerateCharacterName (attackerData);
          var _level = args [0];
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString,
            _level);
          break;
        }
      case INFO_FORMAT_LABEL.GOT_AURA:
        {
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            args);
          break;
        }
      case INFO_FORMAT_LABEL.GOT_DIMENSION_CHIP:
        {
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            args);
          break;
        }
      case INFO_FORMAT_LABEL.ACTIVE_SKILL:
        {
          var _nameString = GenerateCharacterName (attackerData);
          var _skill = args [0] as ICommonSkill;
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString, _skill.NameString);
          break;
        }
      case INFO_FORMAT_LABEL.SKILL_DOT_BURN_ATTACH:
        {
          var _nameString = GenerateCharacterName (defenderData);
          var _power = args [0];
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString, _power);
          break;
        }
      case INFO_FORMAT_LABEL.SKILL_DOT_BURN_DAMAGE:
        {
          var _nameString = GenerateCharacterName (attackerData);
          var _damage = args [0];
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString, _damage);
          break;
        }
      case INFO_FORMAT_LABEL.SKILL_MUST_FAILED:
        {
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance);
          break;
        }
      case INFO_FORMAT_LABEL.DEFENDER_AC_DOWN:
        {
          var _nameString = GenerateCharacterName (defenderData);
          var _AC = args [0];
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString,
            _AC);
          break;
        }
      case INFO_FORMAT_LABEL.DEFENDER_ATK_DOWN_DEF_DOWN:
        {
          var _nameString = GenerateCharacterName (defenderData);
          var _ATK = args [0];
          var _DEF = args [1];
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString,
            _ATK,_DEF);
          break;
        }
      case INFO_FORMAT_LABEL.DEFENDER_HIT_DOWN_AVD_DOWN:
        {
          var _nameString = GenerateCharacterName (defenderData);
          var _HIT = args [0];
          var _AVD = args [1];
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString,
            _HIT,_AVD);
          break;
        }
      case INFO_FORMAT_LABEL.DEFENDER_HIT_DOWN_AVD_DOWN_CRI_DOWN:
        {
          var _nameString = GenerateCharacterName (defenderData);
          var _HIT = args [0];
          var _AVD = args [1];
          var _CRI = args [1];
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString,
            _HIT,_AVD,_CRI);
          break;
        }

      case INFO_FORMAT_LABEL.HP_UP:
        {
          IMultiLangString<AbsMultiLanguageTable> _nameString = null;
          if (attackerData != null) 
          {
            _nameString = GenerateCharacterName (attackerData);
          } 
          else if (defenderData != null) 
          {
            _nameString = GenerateCharacterName (defenderData);
          }

          var _HP = args [0];
     
          _strFormat = new MultiLangString<BattleStringFormatTable> (
            _id, 
            BattleStringFormatTableReader.Instance,
            _nameString,
            _HP);
          break;
        }
      default:
        break;
      }

      if (_strFormat != null) 
      {
        _strFormat.DisattachLangUpdate ();
        this.StringQueue.Enqueue (_strFormat);
      }
    }

    IMultiLangString<AbsMultiLanguageTable> GenerateCharacterName(FightDataFormat data)
    {
      if (data == null)
        return null;
      
      IMultiLangString<AbsMultiLanguageTable> _strFormat = null;

      switch (data.Type) 
      {
      case CHARACTER_TYPE.HERO:
        {
          var _attributes = data.AttributeOriginCache as HeroAttributeFormat;
          var _id = BattleStringFormatTableReader.Instance.FindID (INFO_FORMAT_LABEL.HERO_NAME);
          _strFormat = new MultiLangString<BattleStringFormatTable> (_id, BattleStringFormatTableReader.Instance, _attributes.NameString);
          break;
        }
      case CHARACTER_TYPE.ENEMY:
        {
          var _attributes = data.AttributeOriginCache as EnemyAttributeFormat;
          var _id = BattleStringFormatTableReader.Instance.FindID (INFO_FORMAT_LABEL.ENEMY_NAME);
          _strFormat = new MultiLangString<BattleStringFormatTable> (_id, BattleStringFormatTableReader.Instance, _attributes.NameString);
          break;
        }
      default:
        break;
      }

      if (_strFormat != null) 
      {
        _strFormat.DisattachLangUpdate ();
        if (data.FightType == BATTLE_FIGHT_TYPE.DEFENDER)
          _strFormat.PrefixSpaceNumber = this.DefenderSpace;
      }

      return _strFormat;
    }

    IMultiLangString<AbsMultiLanguageTable> GenerateDamage(FightDataFormat attackerData, FightDataFormat defenderData)
    {
      IMultiLangString<AbsMultiLanguageTable> _damageString = null;
      if (attackerData.OneTurnFightData.IsBlast.Value) 
      {
        var _id = BattleStringFormatTableReader.Instance.FindID (INFO_FORMAT_LABEL.BLAST_DAMAGE);
        _damageString = new MultiLangString<BattleStringFormatTable> (_id, BattleStringFormatTableReader.Instance, defenderData.OneTurnFightData.GetDamageFinal);
      } 
      else 
      {
        var _id = BattleStringFormatTableReader.Instance.FindID (INFO_FORMAT_LABEL.NORMAL_DAMAGE);
        _damageString = new MultiLangString<BattleStringFormatTable> (_id, BattleStringFormatTableReader.Instance, defenderData.OneTurnFightData.GetDamageFinal);
      }

      if (_damageString != null) 
      {
        _damageString.DisattachLangUpdate ();
      }

      return _damageString;
    }

  }
}

