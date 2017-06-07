using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection;
using HeroInfo.Views;
using HeroInfo.Controllers;
using DataManagement.GameData;
using GameFlow.Battle.Common.Controller;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData.FormatCollection.Battle;

namespace BattleCharacterInfo.Controllers{

  public class BattleCharacterAttributeController : MonoBehaviour {

    public HeroInfoView NameView;
    public HeroLevelView LevelView;
    public HeroInfoView HPView;
    public HeroInfoView RESView;
    public HeroInfoView ATKView;
    public HeroInfoView MAGView;
    public HeroInfoView DEFView;
    public HeroInfoView ACView;
    public HeroInfoView CRIView;
    public HeroInfoView PENView;
    public HeroInfoView HITView;
    public HeroInfoView AVDView;
    public HeroIconView IconView;
   

    public void Init(AbsCharacterController controller)
    {
      if (controller.Type == CHARACTER_TYPE.ENEMY) 
      {
        this.enemyController = (EnemyController)controller;
      }
      else if (controller.Type == CHARACTER_TYPE.HERO) 
      {
        this.heroController = (HeroController)controller;
      }
      NameView.Init();
      LevelView.Init();
      HPView.Init ();
      RESView.Init ();
      ATKView.Init ();
      MAGView.Init ();
      DEFView.Init ();
      ACView.Init ();
      CRIView.Init ();
      PENView.Init ();
      HITView.Init ();
      AVDView.Init ();
      IconView.Init ();
    }
  	
    public void DataDisplay()
    {
      if (this.enemyController != null) 
      {
        
        NameView.DataStringDisplay ((this.enemyController.FightData.AttributeOriginCache as EnemyAttributeFormat).NameString.ToString());
        var _enemyAnimationFormat = this.enemyController.GetEnemyAnimationInfo ();
        IconView.SetHeroIcon (new HeroIconDataFormat(_enemyAnimationFormat.TexturePath,_enemyAnimationFormat.IconID));
        LevelView.HeroLevelDisplay (this.enemyController.FightData.FinalAttributesCache.Level);
        HPView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.HP);
        RESView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.RES);
        ATKView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.ATK);
        MAGView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.MAG);
        DEFView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.DEF);
        ACView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.AC);
        CRIView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.CRI);
        PENView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.PEN);
        HITView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.HIT);
        AVDView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.AVD);
      }

      if (this.heroController != null) 
      {
        NameView.DataStringDisplay ((this.heroController.FightData.AttributeOriginCache as HeroAttributeFormat).NameString.ToString());
        IconView.SetHeroIcon (new HeroIconDataFormat(this.heroController.HeroDataCache.AnimationInfo.TexturePath,this.heroController.HeroDataCache.AnimationInfo.IconID));
        LevelView.HeroLevelDisplay (this.heroController.FightData.FinalAttributesCache.Level);
        HPView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.HP);
        RESView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.RES);
        ATKView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.ATK);
        MAGView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.MAG);
        DEFView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.DEF);
        ACView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.AC);
        CRIView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.CRI);
        PENView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.PEN);
        HITView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.HIT);
        AVDView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.AVD);
      }

    }

    void Update()
    {
      //FIXME : yangzhi-wang,use event
      if (this.enemyController != null) 
      {
        LevelView.HeroLevelDisplay (this.enemyController.FightData.FinalAttributesCache.Level);
        //if hp < 0 ,display hp = 0
        HPView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.HP < 0? 0:this.enemyController.FightData.FinalAttributesCache.HP);
        RESView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.RES);
        ATKView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.ATK);
        MAGView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.MAG);
        DEFView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.DEF);
        ACView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.AC);
        CRIView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.CRI);
        PENView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.PEN);
        HITView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.HIT);
        AVDView.DataIntDisplay (this.enemyController.FightData.FinalAttributesCache.AVD);
      }

      if (this.heroController != null) 
      {
        LevelView.HeroLevelDisplay (this.heroController.FightData.FinalAttributesCache.Level);
        //if hp < 0 ,display hp = 0
        HPView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.HP < 0? 0:this.heroController.FightData.FinalAttributesCache.HP);
        RESView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.RES);
        ATKView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.ATK);
        MAGView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.MAG);
        DEFView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.DEF);
        ACView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.AC);
        CRIView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.CRI);
        PENView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.PEN);
        HITView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.HIT);
        AVDView.DataIntDisplay (this.heroController.FightData.FinalAttributesCache.AVD);
      }

    }

    HeroController heroController;
    EnemyController enemyController;
  }
}
