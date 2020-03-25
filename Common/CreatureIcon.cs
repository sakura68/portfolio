using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;
using CodeStage.AntiCheat.ObscuredTypes;

public enum enCreatureIcon_Type
{
    None = -1,
    TeamEdit,                       // 팀편성창에서 내가 보유한 크리쳐 아이콘.
    TeamEditDisplay,                // 팀편성 되어있는 크리쳐 아이콘.
    CreatureTooltip,                // 팀편성창 크리쳐 툴팁 아이콘.
    CreatureReinForce,              // 크리쳐 강화 아이콘.
    CreatureReinForceResult,        // 크리쳐 강화 후 결과창 아이콘.
    CreatureEvolution,              // 크리쳐 진화 아이콘.
    CreatureSynthesis,              // 크리쳐 합성 아이콘.
    CreatureDecomposition,          // 크리쳐 분해 아이콘.
    DailyDungeon,                   // 일일던전 스테이지 클릭시 보여지는 적정보 아이콘.
    DailyDungeonBoss,
    EnemyNormal,                    // 월드맵에서 스테이지 클릭시 보여지는 적정보 아이콘.
    EnemyNormalBoss,
    EnemyNormalPopup,               // 월드맵에서 스테이지 클릭후 보여지는 적정보를 클릭하면 보여지는 팝업 아이콘.
    EnemyBossPopup,
    CreatureInfoPopup,              // 팀편성 되어있는 크리쳐를 팀편성창으로 들어가지 않고 클릭했을때 팝업에서 표시되는 아이콘.
    EnemyAbyss,                     // 심연의 고리 클릭시 보여지는 적정보 아이콘.
    EnemyAbyssPopup,                // 심연의 고리 클릭후 보여지는 적정보를 클릭하면 보여지는 팝업 아이콘.
    Dispatch,                       // 파견에서 내가 보유한 크리쳐 아이콘.
    DispatchDisplay,                // 파견보낼 크리쳐 아이콘.
    DispatchDisplayFriend,          // 파견보낼 친구 크리쳐 아이콘.
    PvpReady,                       // pvp준비창 내 크리쳐 아이콘.
    RealTimePVP,
    VipGift,                        // vip 등급에 따른 크리쳐 아이콘.
    Mail,
    Book,
    BookStory,
    RealtimePvPSelectMemberUI,      // 매칭 이후 UI에서 쓰는 아이콘.
    Shop,
    BattleToolTipPlayer,
    BattleToolTipEnemy,
    CreatureReinforceMaterial,      // 크리쳐 강화할때 재료로 쓰이는 아이콘.
    CreatureReinforceResultMaterial,        // 크리쳐 강화 후 결과창에서 쓰이는 아이콘.
    GuildRaidReward,
    PvpDetail,
    Transcendence,
}

public class CreatureIcon : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] protected UI2DSprite m_Creature2DSprite;                 // [기본포함] 크리쳐 이미지
    [SerializeField] protected List<GameObject> _CreatureBgList = new List<GameObject>();                // [기본포함] 크리쳐 BG    1.노말  2.희귀  3.전설
    [SerializeField] protected GameObject _CreatureBgEmpty;                   // 이미지 없을때

    [SerializeField] protected UISprite m_ClassSprite;                        // [기본포함] 직업(전사, 마법사 등)
    [SerializeField] protected UISprite m_StartSprite;                        // [기본포함] 등급
    [SerializeField] protected UISprite _transcendnceStarSprite;              // [기본포함] 초월등급

    [SerializeField] protected UILabel m_LevelLabel;                          // 레벨 라벨
    [SerializeField] protected UILabel m_ReinforceLevelLabel;                 // 강화 라벨
    [SerializeField] protected UILabel m_CreatureNameLabel;                   // 이름 라벨

    [SerializeField] private UISprite m_SelectSprite;                       // 선택 됐는지
    [SerializeField] private UISprite m_RingFrameSprite;                    // ?
    [SerializeField] private UISprite m_TeamSprite;                         // 팀에 속했는지
    [SerializeField] private UISprite m_LockSprite;                         // 잠금 이미지
    [SerializeField] private UISprite m_NewSprite;                          // New 이미지
    [SerializeField] private UISprite m_CheckSprite;                        // 체크 이미지
    [SerializeField] private UISprite m_CrownSprite;                        // 대표크리쳐 이미지
    [SerializeField] private UISprite m_NoClickSprite;                      // 클릭 안되게 막는 이미지

    [SerializeField] private GameObject m_DispatchParentObj;                // 파견 이미지들 최상단 오브젝트

    [SerializeField] private GameObject m_DispatchingObj;                   // 파견중 이미지
    [SerializeField] private UILabel m_DispatchingObjLabel;                 // 파견중 라벨.

    [SerializeField] private GameObject m_DispatchImpossibleObj;            // 파견불가 이미지

    [SerializeField] private GameObject m_DispatchSelectObj;                // 파견선택 이미지
    [SerializeField] private UILabel m_DispatchSelectNumberLabel;           // 파견선택된 순서 라벨.

    [SerializeField] private GameObject m_DispatchImpossibleTeamObj;        // 파견 팀편성중인 크리쳐 선택 불가 이미지


    [SerializeField] private GameObject m_CardBg;                           // 팀편성에서 쓰임.
    [SerializeField] private GameObject m_CardEmptyImg;                     // 팀편성에서 쓰임.

    [SerializeField] private UILabel m_OrderNumberLabel;                    // 팀편성에서 쓰임.
    [SerializeField] private UISprite m_OrderNumberSprite;                  // 팀편성에서 쓰임.

    [SerializeField] private GameObject m_EjectButton;                      // 팀편성에서 쓰임.
    public GameObject EjectButton { get { return m_EjectButton; } }

    [SerializeField] private UILabel m_OrderNumberRealTimePvPLabel;             // RealPvP에서 쓰임.
    [SerializeField] private UISprite m_OrderNumberRealTimePvPSprite;           // RealPvP에서 쓰임.

    [SerializeField] private GameObject _BookBgParentObj;
    [SerializeField] private GameObject _BookBg;                            // 도감에서 없는 크리쳐 이미지에 쓰임.
    [SerializeField] private GameObject _BookLockBg;                        // 도감에서 크리쳐는 있으나 내가 소유하지 않았을때.
    [SerializeField] private GameObject _BookHeroCoinBg;                    // 도감에서 확정영웅 소환할수 있을때 영웅코인 보여줌.

    [SerializeField] private GameObject _EnemyBoss;                         // 크리쳐 아이콘이 적일때 보스인지

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private enCreatureIcon_Type m_CreatureIconType = enCreatureIcon_Type.None;

    private bool _IsDispatchSelect = false;
    public bool IsDispatchSelect { get { return _IsDispatchSelect; } }

    private ulong _CreatureKey;
    public ulong CreatureKey { get { return _CreatureKey; } set { _CreatureKey = value; } }

    private DATA_CREATURE_NEWVER _CreatureTableData = null;
    public DATA_CREATURE_NEWVER CreatureTableData { get { return _CreatureTableData; } }

    public int m_iCreatureTID;
    public int m_iGrade;
    public bool m_SellCheck;
    public int m_Reinforce;
    //public int m_iTeamIndex;
    public string m_szName;
    public string m_szIcon;
    public int m_iLevel;
    public DATA_CREATURE_ARMY_TYPE._enCreatureArmyType m_enCreatureArmy;
    public int m_iSellCost;
    public bool m_IsNew;
    public byte m_kLock;

    public delegate void OnClickEvent(CreatureIcon icon);
    public event OnClickEvent OnClickevt;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public bool SetIcon(ulong kCreatureUniqueIdx, enCreatureIcon_Type type)
    {
        ItemCreature myCreature = UserInfo.Instance.InventoryInfo.GetMyCreature(kCreatureUniqueIdx);
        if (myCreature == null)
        {
#if DEBUG_LOG
            Debug.Log(string.Format("<color=red>크리쳐 키 : {0} 에 해당하는 크리쳐가 인벤에 없다.</color>", kCreatureUniqueIdx));
#endif
            return false;
        }

        _CreatureTableData = UtilFunc.GetCreatureDataByTID(myCreature.Index);
        if (_CreatureTableData == null)
        {
#if DEBUG_LOG
            Debug.Log("<color=red>DATA_CREATURE_NEWVER 테이블 정보 오류</color>");
#endif
            return false;
        }

        m_CreatureIconType = type;
        _CreatureKey = kCreatureUniqueIdx;

        if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.text = string.Format("{0}{1}", "+", myCreature.ForceCount);
        if (m_LevelLabel != null) m_LevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), myCreature.Level);

        SetDefaultIcon(_CreatureTableData.iCreatureName, _CreatureTableData.m_szIcon, _CreatureTableData.m_enCreatureArmy.ToString(), _CreatureTableData.m_iGrade, myCreature.Awake, _CreatureTableData.m_iQuality);

        return true;
    }

    public bool SetIcon(ObscuredInt level, int force, int awake, DATA_CREATURE_NEWVER CreatureTableData, enCreatureIcon_Type type)
    {
        m_CreatureIconType = type;

        _CreatureTableData = CreatureTableData;

        if (m_LevelLabel != null && level > 0)
        {
            m_LevelLabel.gameObject.SetActive(true);
            m_LevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), level);
        }

        if (m_ReinforceLevelLabel != null && force >= 0)
        {
            m_ReinforceLevelLabel.gameObject.SetActive(true);
            m_ReinforceLevelLabel.text = string.Format("{0}{1}", "+", force);
        }

        SetDefaultIcon(CreatureTableData.iCreatureName, CreatureTableData.m_szIcon, CreatureTableData.m_enCreatureArmy.ToString(), CreatureTableData.m_iGrade, awake, CreatureTableData.m_iQuality);

        return true;
    }
        
    public bool SetIcon(DATA_CREATURE_NEWVER._enIndex iCreatureID, enCreatureIcon_Type type)
    {
        _CreatureTableData = CDATA_CREATURE_NEWVER.Get(iCreatureID);
        if (_CreatureTableData == null)
        {
            // error
            return false;
        }

        m_CreatureIconType = type;

        SetDefaultIcon(_CreatureTableData.iCreatureName, _CreatureTableData.m_szIcon, _CreatureTableData.m_enCreatureArmy.ToString(), _CreatureTableData.m_iGrade, 0, _CreatureTableData.m_iQuality);

        return true;
    }

    /// <summary>
    /// 강화정보와 레벨정보를 기본값으로 셋팅하려고 사용한다.
    /// </summary>
    /// <param name="iCreatureID"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool SetIcon(int iCreatureID, enCreatureIcon_Type type)
    {
        _CreatureTableData = UtilFunc.GetCreatureDataByTID(iCreatureID);
        if (_CreatureTableData == null)
        {
            // error
            return false;
        }

        m_CreatureIconType = type;

        if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.text = "+ 0";
        if (m_LevelLabel != null) m_LevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), " 1");

        SetDefaultIcon(_CreatureTableData.iCreatureName, _CreatureTableData.m_szIcon, _CreatureTableData.m_enCreatureArmy.ToString(), _CreatureTableData.m_iGrade, 0, _CreatureTableData.m_iQuality);

        return true;
    }

    /// <summary>
    /// 팀편성창에서 쓰임.
    /// </summary>
    /// <param name="ItemInfo"></param>
    /// <param name="type"></param>
    public void SetIcon(CreatureItemInfo ItemInfo, enCreatureIcon_Type type)
    {
        if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.text = string.Format("{0}{1}", "+", ItemInfo.ForceCount);
        if (m_LevelLabel != null) m_LevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), ItemInfo.Level);

        _CreatureTableData = UtilFunc.GetCreatureDataByTID(ItemInfo.Tid);

        //SetDefaultIcon(ItemInfo.m_Name, ItemInfo.m_icon, UtilFunc.IconSpriteName(ItemInfo.m_enCreatureArmy), ItemInfo.m_Grade);
        SetDefaultIcon(_CreatureTableData.iCreatureName, ItemInfo.IconName, ItemInfo.CreatureArmyType.ToString(), ItemInfo.Grade, ItemInfo.awake, ItemInfo.Quality);

        _CreatureKey = ItemInfo.CreatureKey;
        m_iCreatureTID = ItemInfo.Tid;
        m_iGrade = ItemInfo.Grade;
        m_SellCheck = ItemInfo.IsSell;
        m_Reinforce = ItemInfo.ForceCount;        
        m_szName = ItemInfo.Name;
        m_szIcon = ItemInfo.IconName;
        m_iLevel = ItemInfo.Level;
        m_enCreatureArmy = ItemInfo.CreatureArmyType;
        m_iSellCost = ItemInfo.SellCost;
        m_IsNew = ItemInfo.IsNew;
        m_kLock = ItemInfo.Lock;
    }

    public void SetIcon(BookCreatureInfo bookInfo, enCreatureIcon_Type type)
    {
        if (m_Creature2DSprite != null) m_Creature2DSprite.gameObject.SetActive(false);
        if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
        if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(false);

        if (_BookBgParentObj != null) _BookBgParentObj.SetActive(true);

        if (bookInfo == default(BookCreatureInfo))
        {
            if (m_CreatureNameLabel != null) m_CreatureNameLabel.gameObject.SetActive(false);
            if (m_ClassSprite != null) m_ClassSprite.gameObject.SetActive(false);
            if (m_StartSprite != null) m_StartSprite.gameObject.SetActive(false);
            if (_transcendnceStarSprite != null) _transcendnceStarSprite.gameObject.SetActive(false);

            EnableBoxCollider(false);
            SetActiveAll(false);
            SetActiveBookBg(true);
        }
        else
        {
            _CreatureTableData = CDATA_CREATURE_NEWVER.Get(bookInfo.m_enCreature);
            if (_CreatureTableData == null)
            {
                // error
                return;
            }

            EnableBoxCollider(true);
            SetDefaultIcon(_CreatureTableData.iCreatureName, _CreatureTableData.m_szIcon, _CreatureTableData.m_enCreatureArmy.ToString(), _CreatureTableData.m_iGrade, 0, _CreatureTableData.m_iQuality);
        }
    }

    public bool SetIcon(ItemCreature myCreature, DATA_CREATURE_NEWVER CreatureTableData, enCreatureIcon_Type type)
    {
        if (myCreature == null)
        {
#if DEBUG_LOG
            Debug.Log("<color=red>크리쳐가 인벤에 없다.</color>");
#endif
            return false;
        }

        if (CreatureTableData == null)
        {
#if DEBUG_LOG
            Debug.Log("<color=red>DATA_CREATURE_NEWVER 테이블 정보 오류</color>");
#endif
            return false;
        }

        _CreatureTableData = CreatureTableData;

        m_CreatureIconType = type;
        _CreatureKey = myCreature.UniqueIndex;

        if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.text = string.Format("{0}{1}", "+", myCreature.ForceCount);
        if (m_LevelLabel != null) m_LevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), myCreature.Level);

        SetDefaultIcon(_CreatureTableData.iCreatureName, _CreatureTableData.m_szIcon, _CreatureTableData.m_enCreatureArmy.ToString(), _CreatureTableData.m_iGrade, myCreature.Awake, _CreatureTableData.m_iQuality);

        return true;
    }

    private void SetDefaultIcon(int CreatureName, string szIcon, string szClass, int iGrade, int awake, int iQuality)
    {
        if (m_CreatureNameLabel != null)
        {
            m_CreatureNameLabel.text = StringTableManager.GetData((int)CreatureName);
            m_CreatureNameLabel.gameObject.SetActive(true);
        }

        // 2018-01-16 영웅 백배경 추가
        {
            for(int i = 0; i < _CreatureBgList.Count; ++i)
            {
                _CreatureBgList[i].gameObject.SetActive(false);
            }

            if (_CreatureBgList.Count >= iQuality)
            {
                _CreatureBgList[iQuality - 1].gameObject.SetActive(true);
            }
        }

        if (m_Creature2DSprite != null)
        {
            m_Creature2DSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_CREATUREHEAD, szIcon);
            m_Creature2DSprite.gameObject.SetActive(true);
        }
        if (m_ClassSprite != null)
        {
            m_ClassSprite.spriteName = string.Format("{0}{1}", "UI_", szClass);
            m_ClassSprite.gameObject.SetActive(true);
        }

        if (iGrade >= (int)DATA_ENCHANT_GRADE_TYPE._enEnchantGradeType.Grade6 && awake > 0)
        {
            if (m_LevelLabel != null)
            {
                m_LevelLabel.color = GameRule.TranscendenceGageColor;
            }

            if (m_StartSprite != null)
            {
                m_StartSprite.gameObject.SetActive(false);
            }

            if (_transcendnceStarSprite != null)
            {
                _transcendnceStarSprite.gameObject.SetActive(true);
                _transcendnceStarSprite.spriteName = string.Format(GameRule.TranscendenceThumbnailStarFormat, awake);
            }
        }
        else
        {
            if (m_LevelLabel != null)
            {
                m_LevelLabel.color = GameRule.NormalGageColor;
            }

            if (_transcendnceStarSprite != null)
            {
                _transcendnceStarSprite.gameObject.SetActive(false);
            }

            if (m_StartSprite != null)
            {
                m_StartSprite.gameObject.SetActive(true);
                m_StartSprite.spriteName = string.Format(GameRule.NormalThumbnailStarFormat, iGrade);
            }
        }

        if (m_RingFrameSprite != null)
        {
            m_RingFrameSprite.spriteName = string.Format("comm_thumbnailFrame0{0}", iGrade);
            m_RingFrameSprite.gameObject.SetActive(true);
        }

        if (m_DispatchingObjLabel != null)
        {
            m_DispatchingObjLabel.text = StringTableManager.GetData(6722);       // 6722	파견
            m_DispatchingObjLabel.gameObject.SetActive(true);
        }

        // 파견 이미지 최상단 오브젝트라서 어떤 아이콘이 됐든 항상 켜놓는다.
        if (m_DispatchParentObj != null) m_DispatchParentObj.SetActive(true);

        SetActiveAll(false);

        if (m_CreatureIconType == enCreatureIcon_Type.Dispatch)
        {
            UIEventListener.Get(gameObject).onClick = OnDispatchSelectIcon;
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.DispatchDisplayFriend)
        {
            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
            if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(false);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.TeamEditDisplay)
        {
            SetActiveCardBg(true);
            SetActiveOrderNumber(true);
            //SetActiveDefaultInfo(true);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.EnemyNormal || m_CreatureIconType == enCreatureIcon_Type.DailyDungeon)
        {
            SetEnemyIconBG("comm_BGEnemyRed");

            if (m_CreatureNameLabel != null) m_CreatureNameLabel.gameObject.SetActive(false);
            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
            if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(false);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.EnemyNormalBoss || m_CreatureIconType == enCreatureIcon_Type.DailyDungeonBoss
            || m_CreatureIconType == enCreatureIcon_Type.EnemyBossPopup)
        {
            SetEnemyIconBG("comm_BGEnemyRed");

            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
            if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(false);

            if (_EnemyBoss != null) _EnemyBoss.SetActive(true);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.EnemyNormalPopup )            
        {
            //SetEnemyIconBG("comm_BGEnemyRed");

            if (m_ClassSprite != null) { m_ClassSprite.transform.localPosition = new Vector3(33.1f, 32.5f, 0.0f); }
            if (m_StartSprite != null) { m_StartSprite.transform.localPosition = new Vector3(0.0f, -57.3f, 0.0f); }

            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
            if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(false);
        }
        else if (m_CreatureIconType == enCreatureIcon_Type.CreatureInfoPopup)
        {
            //SetEnemyIconBG("comm_creatureSlotBG");

            //if (m_ClassSprite != null) { m_ClassSprite.transform.localPosition = new Vector3(33.1f, 32.5f, 0.0f); }
            //if (m_StartSprite != null) { m_StartSprite.transform.localPosition = new Vector3(0.0f, -57.3f, 0.0f); }

            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(true);
            if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(true);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.PvpReady)
        {
            SetEnemyIconBG("comm_BGEnemy");

            if (m_StartSprite != null) { m_StartSprite.transform.localPosition = new Vector3(0.0f, -50.0f, 0.0f); }
            if (_transcendnceStarSprite != null) { _transcendnceStarSprite.transform.localPosition = new Vector3(0.0f, -50.0f, 0.0f); }

            //if (m_StartSprite != null)
            //{
            //    m_StartSprite.gameObject.SetActive(true);
            //    m_StartSprite.transform.localPosition = new Vector3(0.0f, -50.0f, 0.0f);
            //}

            if (m_CreatureNameLabel != null) m_CreatureNameLabel.gameObject.SetActive(false);
            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
            if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(false);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.EnemyAbyss)
        {
            SetEnemyIconBG("comm_BGEnemyRed");

            if (m_CreatureNameLabel != null) m_CreatureNameLabel.gameObject.SetActive(false);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.EnemyAbyssPopup)
        {
            SetEnemyIconBG("comm_BGEnemyRed");

            if (m_ClassSprite != null) { m_ClassSprite.transform.localPosition = new Vector3(33.1f, 32.5f, 0.0f); }
            if (m_StartSprite != null) { m_StartSprite.transform.localPosition = new Vector3(0.0f, -57.3f, 0.0f); }
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.BookStory)
        {
            m_LevelLabel.gameObject.SetActive(false);
            m_ReinforceLevelLabel.gameObject.SetActive(false);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.RealTimePVP)
        {
            SetActiveOrderNumberRealTimePvP(true);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.RealtimePvPSelectMemberUI)
        {
            SetActiveOrderNumberRealTimePvP(true);
        }
        else if(m_CreatureIconType == enCreatureIcon_Type.BattleToolTipEnemy)
        {
            if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(false);
            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 적 아이콘 표시할때만 BG 가 달라져야 하기때문에.
    /// </summary>
    public void SetEnemyIconBG(string bgName)
    {
        for (int i = 0; i < _CreatureBgList.Count; ++i)
        {
            if (i == 0)
            {
                _CreatureBgList[i].SetActive(true);

                UISprite uiSprite = _CreatureBgList[i].GetComponent<UISprite>();
                if (uiSprite == null)
                    continue;

                uiSprite.spriteName = bgName;
            }
            else
            {
                _CreatureBgList[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 크리쳐 아이콘 프리팹에는 패널을 붙이지 않아서 패널을 붙이고 싶을때 쓰임.
    /// iParentPanelDepth 인자로 부모 패널의 뎁스를 꼭 넣어주자...
    /// </summary>
    /// <param name="iPanelDepth"></param>
    public void AddPanel(int iParentPanelDepth)
    {
        UIPanel uiParentPanel = UtilFunc.GetParentPanel(transform);
        if(uiParentPanel != null)
        {
            iParentPanelDepth = uiParentPanel.depth;
        }

        UIPanel uiPanel = gameObject.GetComponent<UIPanel>();
        if (uiPanel == null)
        {
            uiPanel = gameObject.AddComponent<UIPanel>();
        }

        UtilFunc.SetPanelDepth(uiPanel, iParentPanelDepth + 10);
    }

    /// <summary>
    /// 박스콜리더가 기본적으로 붙어있는데 클릭을 막기위해 제거할때 사용.
    /// </summary>
    public void RemoveBoxCollider()
    {
        BoxCollider boxcol = gameObject.GetComponent<BoxCollider>();
        if (boxcol != null)
        {
            Destroy(boxcol);
        }
    }

    private void EnableBoxCollider(bool IsActive)
    {
        BoxCollider boxcol = gameObject.GetComponent<BoxCollider>();
        if (boxcol != null)
        {
            boxcol.enabled = IsActive;
        }
    }

    public void RemoveDragScrollView()
    {
        UIDragScrollView uiDragScrollView = gameObject.GetComponent<UIDragScrollView>();
        if (uiDragScrollView != null)
        {
            Destroy(uiDragScrollView);
        }
    }

    public void SetActiveAll(bool bIsActive)
    {
        SetActiveSelect(bIsActive);
        SetActiveRing(bIsActive);
        SetActiveTeam(bIsActive);
        SetActiveLock(bIsActive);
        SetActiveNew(bIsActive);
        SetActiveCheck(bIsActive);
        SetActiveCrown(bIsActive);
        SetActiveNoClick(bIsActive);
        SetActiveDispatching(bIsActive);
        SetActiveCardBg(bIsActive);
        SetActiveCardEmpty(bIsActive);
        SetActiveOrderNumber(bIsActive);
        SetActiveOrderNumberRealTimePvP(bIsActive);
        SetActiveEjectButton(bIsActive);
        SetActiveBookBg(bIsActive);
        SetActiveBookLockBg(bIsActive);
        SetActiveBoss(bIsActive);
        SetActiveBookHeroCoinBg(bIsActive);
    }

    public bool GetActiveSelect()
    {
        return m_SelectSprite.gameObject.activeSelf;
    }

    public void SetActiveSelect(bool bIsActive)
    {
        if (m_SelectSprite != null)
        {
            m_SelectSprite.gameObject.SetActive(bIsActive);
            TweenAlpha tweenAlpha = m_SelectSprite.GetComponent<TweenAlpha>();
            if(tweenAlpha != null)
            {
                tweenAlpha.ResetToBeginning();
                tweenAlpha.to = 0;
            }
        }
    }

    public void SetActiveRing(bool bIsActive)
    {
        if (m_RingFrameSprite != null)
            m_RingFrameSprite.gameObject.SetActive(bIsActive);
    }

    public void SetActiveTeam(bool bIsActive)
    {
        if(m_TeamSprite != null)
            m_TeamSprite.gameObject.SetActive(bIsActive);
    }

    public void SetActiveLock(bool bIsActive)
    {
        if (m_LockSprite != null)
            m_LockSprite.gameObject.SetActive(bIsActive);
    }

    public void SetActiveNew(bool bIsActive)
    {
        if (m_NewSprite != null)
            m_NewSprite.gameObject.SetActive(bIsActive);
    }

    public bool GetActiveCheck()
    {
        if (m_CheckSprite != null)
            return m_CheckSprite.gameObject.activeSelf;
        return false;
    }

    public void SetActiveCheck(bool bIsActive)
    {
        if (m_CheckSprite != null)
            m_CheckSprite.gameObject.SetActive(bIsActive);
    }

    public void SetActiveCrown(bool bIsActive)
    {
        if (m_CrownSprite != null)
            m_CrownSprite.gameObject.SetActive(bIsActive);
    }

    public void SetActiveNoClick(bool bIsActive)
    {
        if (m_NoClickSprite != null)
            m_NoClickSprite.gameObject.SetActive(bIsActive);
    }

    /// <summary>
    /// 파견시 파견숫자 표시 1,2,3...
    /// </summary>
    /// <param name="bIsActive"></param>
    public void SetActiveDispatchSelect(bool bIsActive)
    {
        _IsDispatchSelect = bIsActive;
        m_DispatchSelectObj.SetActive(bIsActive);
    }

    /// <summary>
    /// enCreatureIcon_Type.TeamEditDisplay 일때만 true
    /// </summary>
    /// <param name="bIsActive"></param>
    public void SetActiveCardBg(bool bIsActive)
    {
        if (m_CardBg != null)
        {
            m_CardBg.SetActive(bIsActive);
        }
    }

    public void SetActiveCardEmpty(bool bIsActive)
    {
        if (m_CardEmptyImg != null)
        {
            m_CardEmptyImg.SetActive(bIsActive);
        }
    }

    public void SetActiveOrderNumber(bool bIsActive)
    {
        if (m_OrderNumberLabel != null)
        {
            m_OrderNumberLabel.gameObject.SetActive(bIsActive);
        }
    }

    public void SetActiveOrderNumberRealTimePvP(bool bIsActive)
    {
        if(m_OrderNumberRealTimePvPLabel != null)
        {
            m_OrderNumberRealTimePvPLabel.gameObject.SetActive(bIsActive);
        }
    }

    public void SetActiveEjectButton(bool bIsActive)
    {
        if (m_EjectButton != null)
        {
            m_EjectButton.SetActive(bIsActive);
        }
    }

    private void SetActiveBookBg(bool bIsActive)
    {
        if(_BookBg != null)
        {
            _BookBg.SetActive(bIsActive);
        }
    }

    public void SetActiveBookLockBg(bool bIsActive)
    {
        if(_BookLockBg != null)
        {
            _BookLockBg.SetActive(bIsActive);
        }
    }

    public void SetActiveBookNoHaveBg(bool bIsActive)
    {

    }

    /// <summary>
    /// 팀편성창에서 파견중인지 아닌지 표시하는 부분.
    /// </summary>
    /// <param name="bIsActive"></param>
    public void SetActiveDispatching(bool bIsActive)
    {
        if(m_DispatchingObj != null)
        {
            m_DispatchingObj.SetActive(bIsActive);
        }

        if (m_DispatchImpossibleObj != null) m_DispatchImpossibleObj.SetActive(false);
        if (m_DispatchSelectObj != null) m_DispatchSelectObj.SetActive(false);
        if (m_DispatchImpossibleTeamObj != null) m_DispatchImpossibleTeamObj.SetActive(false);
    }

    /// <summary>
    /// 아이콘의 기본정보 Active 셋팅.
    /// </summary>
    /// <param name="bIsActive"></param>
    public void SetActiveDefaultInfo(bool bIsActive)
    {
        if (m_CreatureNameLabel != null) m_CreatureNameLabel.gameObject.SetActive(bIsActive);
        if (m_Creature2DSprite != null) m_Creature2DSprite.gameObject.SetActive(bIsActive);
        if (m_ClassSprite != null) m_ClassSprite.gameObject.SetActive(bIsActive);
        if (m_StartSprite != null) m_StartSprite.gameObject.SetActive(bIsActive);
        if (m_RingFrameSprite != null) m_RingFrameSprite.gameObject.SetActive(bIsActive);
        if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(bIsActive);
        if (m_LevelLabel != null) m_LevelLabel.gameObject.SetActive(bIsActive);
        if (_transcendnceStarSprite != null) _transcendnceStarSprite.gameObject.SetActive(bIsActive);
    }

    /// <summary>
    /// PvpReady 창에서 빈 크리쳐 보여줄때 쓰임.
    /// </summary>
    public void SetIconPvpEmpty()
    {
        m_CreatureIconType = enCreatureIcon_Type.PvpReady;

        SetActiveAll(false);
        SetActiveDefaultInfo(false);

        if (m_Creature2DSprite != null)
        {
            m_Creature2DSprite.sprite2D = null;
            m_Creature2DSprite.gameObject.SetActive(true);
        }

        SetEnemyIconBG("comm_BGEnemy");
    }

    /// <summary>
    /// 보스인지
    /// </summary>
    public void SetActiveBoss(bool bIsActive)
    {
        if (_EnemyBoss != null)
        {
            _EnemyBoss.SetActive(bIsActive);
        }
    }

    /// <summary>
    /// // 도감에서 확정영웅 소환할수 있을때 영웅코인 보여줌.
    /// </summary>
    public void SetActiveBookHeroCoinBg(bool bIsActive)
    {
        if (_BookHeroCoinBg != null)
        {
            _BookHeroCoinBg.SetActive(bIsActive);
        }
    }

    /// <summary>
    /// 적 아이콘 표시할때 비어있는 이미지.
    /// </summary>
    public void ActiveEmptyBg()
    {
        if (_CreatureBgEmpty != null)
        {
            for (int i = 0; i < _CreatureBgList.Count; ++i)
                _CreatureBgList[i].SetActive(false);

            if (m_Creature2DSprite != null) m_Creature2DSprite.gameObject.SetActive(false);
            if (m_ClassSprite != null) m_ClassSprite.gameObject.SetActive(false);
            if (m_StartSprite != null) m_StartSprite.gameObject.SetActive(false);
            if (m_ReinforceLevelLabel != null) m_ReinforceLevelLabel.gameObject.SetActive(false);
            if (m_CreatureNameLabel != null) m_CreatureNameLabel.gameObject.SetActive(false);
            if (m_RingFrameSprite != null) m_RingFrameSprite.gameObject.SetActive(false);

            if(_CreatureBgEmpty != null) _CreatureBgEmpty.SetActive(true);
        }
    }

    // dispatch
    public void SetDispatchSelectNumberLabel(string text)
    {
        if (m_DispatchSelectNumberLabel != null)
        {
            //m_DispatchSelectNumberLabel.gameObject.SetActive(true);
            m_DispatchSelectNumberLabel.text = text;
        }
    }

    public string GetDispatchSelectNumberLabel()
    {
        string str = string.Empty;
        if (m_DispatchSelectNumberLabel != null)
        {
            //m_DispatchSelectNumberLabel.gameObject.SetActive(true);
            str = m_DispatchSelectNumberLabel.text;
        }

        return str;
    }

    public void SetOrderNumberLabel(string text)
    {
        if (m_OrderNumberLabel != null)
        {
            //m_OrderNumberLabel.gameObject.SetActive(true);
            m_OrderNumberLabel.text = text;
        }
    }

    public void SetOrderNumberRealTimePvPLabel(string text)
    {
        if(m_OrderNumberRealTimePvPLabel != null)
        {
            m_OrderNumberRealTimePvPLabel.text = text;
        }
    }

    public void SetOrderNumberSprite(string spriteName)
    {
        if(m_OrderNumberSprite != null)
        {
            m_OrderNumberSprite.spriteName = spriteName;
        }
    }

    public void SetOrderNumberRealPvPSprite(string spriteName)
    {
        if (m_OrderNumberRealTimePvPSprite != null)
        {
            m_OrderNumberRealTimePvPSprite.spriteName = spriteName;
        }
    }

    //===================================================================================
    //
    // Packet
    //
    //===================================================================================

    //===================================================================================
    //
    // Event
    //
    //===================================================================================   
    private void OnDispatchSelectIcon(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        //SetActiveDispatchSelect(m_bDispatchSelect = !m_bDispatchSelect);

        if(OnClickevt != null)
        {
            OnClickevt(this);
        }
    } 
}
