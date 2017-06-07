using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;
using System.Collections.Generic;
using DataManagement.TableClass.Skill;
using DataManagement.GameData.FormatCollection.Skill.Effect;
using Common;
using GameFlow.Battle.Controller;

namespace DataManagement.GameData.FormatCollection.Common.Skill
{
  public interface ICommonSkill 
  {
    int SlotID { get;}
    ushort DBSkillID { get;}
    int Level { get; set;}
    MultiLangString<SkillStringsTable> NameString{ get;}
    SKILL_TYPE Type{ get;}
    ConditionController SkillCondition{ get;}
    List<AbsSkillEffectBase> EffectList { get;}
    BattleInfoManager BattleInfoManagerScript{ get;}
    string TexturePath{ get;}
    int TextureIconID{ get;}
    void Reset ();
  }

  [System.Serializable]
  public class CommonSkillFormat:ICommonSkill
  {
    #region ICommonSkill implementation

    public int SlotID {
      get {
        return this.slotID;
      }
    }

    public ushort DBSkillID {
      get {
        return this.dbSkillID;
      }
    }

    public int Level {
      get {
        return this.level;
      }
      set{ 
        this.level = value;
      }
    }

    public MultiLangString<SkillStringsTable> NameString {
      get {
        if (this.nameStringCache == null) 
        {
          var _id = SkillStringsTableReader.Instance.FindID (this.DBSkillID, SKILL_STRINGS_LABEL.NAME);
          this.nameStringCache = new MultiLangString<SkillStringsTable>(_id, SkillStringsTableReader.Instance);
        }
        return this.nameStringCache;
      }
    }

    public SKILL_TYPE Type {
      get {
        return this.type;
      }
    }

    public ConditionController SkillCondition {
      get {
        return this.skillCondition;
      }
    }

    public List<AbsSkillEffectBase> EffectList {
      get {
        return this.effectList;
      }
    }

    public BattleInfoManager BattleInfoManagerScript{ 
      get{ 
        if (this.battleInfoManagerScript == null)
          this.battleInfoManagerScript = GameObject.FindObjectOfType<BattleInfoManager> ();

        return this.battleInfoManagerScript;
      }
    }

    public string TexturePath
    {
      get {
        return this.texturePath;
      }
    }

    public int TextureIconID
    {
      get {
        return this.textureIconID;
      }
    }

    #endregion


    public CommonSkillFormat(ushort skillID, int slotID = -1, int level = 0)
    {
      this.dbSkillID = skillID;
      this.slotID = slotID;
      this.level = level;

      var _dbSkill = SkillTableReader.Instance.FindDefaultUnique (skillID);
      this.type = _dbSkill.SkillType;
      this.texturePath = _dbSkill.TexturePath;
      this.textureIconID = _dbSkill.TextureIconID;
      // Init skill triggers
      var _dbSkillTriggerList = SkillTriggerTableReader.Instance.FindBySkillID (skillID);
      if (_dbSkillTriggerList != null) 
      {
        List<TriggerTypeIDMapFormat> _skillTriggerMapList = new List<TriggerTypeIDMapFormat> ();
        _dbSkillTriggerList.ForEach (skillTrigger => {
          _skillTriggerMapList.Add(new TriggerTypeIDMapFormat(skillTrigger));
        });

        this.skillCondition = new ConditionController (this, _skillTriggerMapList);
      }

      // Init skill effects
      var _dbSkillEffectList = SkillEffectTableReader.Instance.FindBySkillID (skillID);
      if (_dbSkillEffectList != null) 
      {
        this.effectList = new List<AbsSkillEffectBase> ();
        _dbSkillEffectList.ForEach (skillEffectData => {
          switch (skillEffectData.EffectType) {
          case SKILL_EFFECT_TYPE.STANDARD_ATTACK_POWER:
            {
              var _effect = new StandardAttackPowerFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.LAST_STAND_ATTACK_POWER:
            {
              var _effect = new LastStandAttackPowerFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.MUST_HIT:
            {
              var _effect = new MustHitFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.CHANGE_ATTRIBUTE:
            {
              var _effect = new ChangeAttributeFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.CHANGE_DAMAGE: //FIXME: yang-zhang rename to CHANGE_BLAST_POWER
            {
              var _effect = new ChangeDamageFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.CHANGE_HP:
            {
              var _effect = new ChangeHPFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.CHANGE_AFFECT_RANGE:
            {
              var _effect = new ChangeAffectRangeFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.CHANGE_HIT_COUNT:
            {
              var _effect = new ChangeHitCountFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.SKILL_MUST_FAILED:
            {
              var _effect = new SkillMustFaildFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.IGNORE_AC:
            {
              var _effect = new IgnoreDefenderACFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.IGNORE_DEF:
            {
              var _effect = new IgnoreDefenderDEFFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.SNEER:
            {
              var _effect = new SneerFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.DOT:
            {
              var _effect = new DotFomart(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.TRICK_LEARNING:
            {
              var _effect = new TrickLearningFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          case SKILL_EFFECT_TYPE.REINFORCE:
            {
              var _effect = new ReinforceFormat(this, skillEffectData);
              this.effectList.Add(_effect);
              break;
            }
          default:
            break;
          }
        });
      }
    }

    public virtual void Reset()
    {
      this.skillCondition.Reset();
      this.effectList.ForEach (effect => {
        effect.Reset ();
      });
    }

    [SerializeField, ReadOnly]
    int slotID;
    [SerializeField, ReadOnly]
    ushort dbSkillID;
    [SerializeField, ReadOnly]
    int level;
    [SerializeField, ReadOnly]
    SKILL_TYPE type;
    [SerializeField, ReadOnly]
    ConditionController skillCondition;
    [SerializeField, ReadOnly]
    List<AbsSkillEffectBase> effectList;
    [SerializeField, ReadOnly]
    MultiLangString<SkillStringsTable> nameStringCache;
    [System.NonSerialized]
    BattleInfoManager battleInfoManagerScript;
    [SerializeField, ReadOnly]
    string texturePath;
    [SerializeField, ReadOnly]
    int textureIconID;
  }

  [System.Serializable]
  public class SortCommonSkillBySlotID : IComparer<ICommonSkill>
  {
    #region IComparer implementation
    public int Compare (ICommonSkill x, ICommonSkill y)
    {
      return x.SlotID.CompareTo (y.SlotID);
    }
    #endregion
  }
}
