using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildRaidReady : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _CloseButton;
    [SerializeField] private UILabel _CloseButtonLabel;

    [SerializeField] private UILabel _MyRankTitleLabel;

    [SerializeField] private GameObject _GuildRaidInfoButton;

    [SerializeField] private GameObject _EasyIcon;
    [SerializeField] private UILabel _EasyLabel;

    [SerializeField] private GameObject _NormalIcon;
    [SerializeField] private UILabel _NormalLabel;

    [SerializeField] private GameObject _HardIcon;
    [SerializeField] private UILabel _HardLabel;

    [SerializeField] private UI2DSprite _BossSprite;
    [SerializeField] private UILabel _BossNameLabel;

    [SerializeField] private UILabel _RemainTimeLabel;

    [SerializeField] private UILabel _EnterCountLabel;

    [SerializeField] private GameObject _BattleStartButton;
    [SerializeField] private GameObject _BattleStartButtonNew;
    [SerializeField] private UILabel _BattleStartButtonLabel;

    [SerializeField] private UIScrollView _uiScrollView;
    [SerializeField] private UIGrid _uiGrid;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildRaidWindow _Owner = null;

    private GuildRaidInfoPopup _GuildRaidInfoPopup = null;

    private CreatureTeamMenuElement _TeamMenu = null;

    private DATA_GUILDRAID _guildRaidTable = null;

    private List<GuildRaidMemberRankingItem> _guildRaidMemberRankingList = new List<GuildRaidMemberRankingItem>();

    private int _guildRaidEnterMax = 0;

    private DATA_GUILDRAID.Enum _guildRaidType = DATA_GUILDRAID.Enum.GUILDRAID_NONE;

    private ulong _myGuildRaidScore = 0;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick = OnClickBack;

        if (_GuildRaidInfoButton != null) UIEventListener.Get(_GuildRaidInfoButton).onClick = OnGuildRaidInfoPopup;
        if (_BattleStartButton != null) UIEventListener.Get(_BattleStartButton).onClick = OnGuildRaidBattleStart;
    }

    protected override void OnDestroy()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick -= OnClickBack;

        if (_GuildRaidInfoButton != null) UIEventListener.Get(_GuildRaidInfoButton).onClick -= OnGuildRaidInfoPopup;
        if (_BattleStartButton != null) UIEventListener.Get(_BattleStartButton).onClick -= OnGuildRaidBattleStart;

        ClearRankingItem();
    }

    protected override void Start()
    {
    }

    protected override void OnEnable()
    {
#if GMTOOLSHOP
        UIControlManager.instance.SetGuildRaidWealth(WEB_SHOP_CATEGORY._enWebUI_Category._enWebUI_Category_Max);
#else
        UIControlManager.instance.SetGuildRaidWealth(DATA_SHOP_NEW_CATEGORY._enCategory._enCategory_Max);
#endif
    }

    protected override void OnDisable()
    {
    }

    protected override void Update()
    {
        _BattleStartButtonNew.SetActive(UserInfo.Instance.otherNew.isGuildRaidTicketFull);
    }

    public override void Clear()
    {
    }

    public override void Init()
    {
    }

    public override void CreatureTeamUpdateRecv(int iRecvTeamIndex)
    {
        _TeamMenu.UITeamIndex = iRecvTeamIndex;
        _TeamMenu.SaveTeamIndex();
        _TeamMenu.UpdateTeamInfo(WindowType);
    }

    public override bool OnClickBack()
    {
        CNetManager.Instance.GuildRaidStub.OnGuildRaidInfo = _Owner.SetRecvData;
        _stGuildRaidInfoReq stGuildRaidInfoReq = new _stGuildRaidInfoReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidInfo, stGuildRaidInfoReq, typeof(_stGuildRaidInfoAck));

        return true;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(GuildRaidWindow owner)
    {
        _Owner = owner;

        _guildRaidEnterMax = Mathf.CeilToInt(CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.GuildRaid_DailyEnterMax).Value);

        _CloseButtonLabel.text = StringTableManager.GetData(6753);        // 6753	길드 레이드

        _EasyLabel.text = StringTableManager.GetData(81);     // 81 쉬움
        _NormalLabel.text = StringTableManager.GetData(82);     // 82 보통
        _HardLabel.text = StringTableManager.GetData(83);     // 83 어려움

        _BattleStartButtonLabel.text = StringTableManager.GetData(90);        // 90 전투 시작

        _MyRankTitleLabel.text = string.Format(StringTableManager.GetData(4917), 0);      // 4917 내 순위 : {0}위

        _uiGrid.sorting = UIGrid.Sorting.Custom;
        _uiGrid.onCustomSort = UtilFunc.SortByNumber;

        if (_TeamMenu == null)
        {
            _TeamMenu = UIResourceMgr.CreatePrefab<CreatureTeamMenuElement>(BUNDLELIST.PREFABS_UI_EDIT, transform, "CreatureTeamMenuElement");
            Vector3 vParentPos = transform.localPosition;
            Vector3 vChildPos = _TeamMenu.transform.localPosition;
            _TeamMenu.transform.localPosition = new Vector3(vChildPos.x - vParentPos.x, vChildPos.y - vParentPos.y);
            _TeamMenu.SetUI(WindowType, false, false, OnGoToTeamEditEvent);
        }

        _TeamMenu.UpdateTeamInfo(WindowType);
        _TeamMenu.OpenUI();
    }

    public void SetRecvData(DATA_GUILDRAID.Enum guildRaidType, _stGuildRaidGetMemberRankListAck guildRaidMemberRankList, DateTime raidEndTime)
    {
        CreateMemberRankList(guildRaidMemberRankList);

        _guildRaidType = guildRaidType;
        _guildRaidTable = CDATA_GUILDRAID.Get(_guildRaidType);

        _BossSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_GUILDRAID, _guildRaidTable.RaidBannerImage);
        _BossNameLabel.text = StringTableManager.GetData(_guildRaidTable.RaidBossName);

        TimeSpan ts = raidEndTime - TimeManager.Instance.GetServerTime();
        _RemainTimeLabel.text = string.Format(StringTableManager.GetData(4915), ts.Hours, ts.Minutes);     // 4915 {0}시간 {1}분
        _EnterCountLabel.text = string.Format(StringTableManager.GetData(4924), UserInfo.Instance.GuildRaidTicket, _guildRaidEnterMax);        // 4924 입장 가능 횟수 : {0} / {1}

        _EasyIcon.gameObject.SetActive(false);
        _NormalIcon.gameObject.SetActive(false);
        _HardIcon.gameObject.SetActive(false);

        if (_guildRaidTable.SetLevel == (int)enGuildRaidDifficulty.Easy)
        {
            _EasyIcon.gameObject.SetActive(true);
        }
        else if (_guildRaidTable.SetLevel == (int)enGuildRaidDifficulty.Normal)
        {
            _NormalIcon.gameObject.SetActive(true);
        }
        else if (_guildRaidTable.SetLevel == (int)enGuildRaidDifficulty.Hard)
        {
            _HardIcon.gameObject.SetActive(true);
        }
    }

    private void CreateMemberRankList(_stGuildRaidGetMemberRankListAck guildRaidMemberRankList)
    {
        ClearRankingItem();

        foreach(CGuildRaidUserRankInfo data in guildRaidMemberRankList.kRankList)
        {
            if (string.Equals(data.kCharName, UserInfo.Instance.NickName))
            {
                _MyRankTitleLabel.text = string.Format(StringTableManager.GetData(4917), data.kUserRank);      // 4917 내 순위 : {0}위
                _myGuildRaidScore = data.kGuildRaidScore;
            }

            GuildRaidMemberRankingItem item = UIResourceMgr.CreatePrefab<GuildRaidMemberRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, _uiGrid.transform, "GuildRaidMemberRankingItem");
            item.gameObject.SetActive(true);
            item.name = data.kUserRank.ToString();
            item.Init(data);

            _guildRaidMemberRankingList.Add(item);
        }

        //Transform parent;
        //foreach (CGuildRaidRankInfo data in stAck.kRankList)
        //{
        //    if (data.kGuildRaidRank == 1)
        //        parent = _no1Ranking;
        //    else
        //        parent = _uiGrid.transform;

        //    if (string.Equals(UserInfo.Instance.GuildName, data.kGuildName))
        //    {
        //        _myRankingItem = UIResourceMgr.CreatePrefab<GuildRaidRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, _myGuild, "GuildRaidRankingItem");
        //        _myRankingItem.gameObject.SetActive(true);
        //        _myRankingItem.name = data.kGuildRaidRank.ToString();
        //        _myRankingItem.Init(data, true);
        //    }

        //    GuildRaidRankingItem item = UIResourceMgr.CreatePrefab<GuildRaidRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, parent, "GuildRaidRankingItem");
        //    item.gameObject.SetActive(true);
        //    item.name = data.kGuildRaidRank.ToString();
        //    item.Init(data, false);

        //    _rankingItemList.Add(item);
        //}
    }

    private void RecvGuildRaidBattleStart(_stGuildRaidBattleStartAck stAck)
    {
        UserInfo.Instance.GuildRaidTicket = stAck.kUpdateTicket;
        //UserInfo.Instance.SetWealth(DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_FRIENDRAIDJOINTICKET, stAck.kCurrFriendRaidJoinTicket);

        GameSceneManager.Instance.GuildRaidBattleStart(_guildRaidTable);
    }

    private void ClearRankingItem()
    {
        for (int i = 0; i < _guildRaidMemberRankingList.Count; ++i)
        {
            DestroyImmediate(_guildRaidMemberRankingList[i].gameObject);
        }

        _guildRaidMemberRankingList.Clear();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnGoToTeamEditEvent(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPEN);

        UIControlManager.instance.ShowWindow<object>(enUIType.EDIT_TEAM, null);
    }

    private void OnGuildRaidInfoPopup(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if( _GuildRaidInfoPopup == null)
        {
            _GuildRaidInfoPopup = UIResourceMgr.CreatePrefab<GuildRaidInfoPopup>(BUNDLELIST.PREFABS_UI_GUILDRAID, transform, "GuildRaidInfoPopup");
            _GuildRaidInfoPopup.Init();

            UIControlManager.instance.AddWindow(enUIType.GUILDRAIDINFO, _GuildRaidInfoPopup);
        }

        _GuildRaidInfoPopup.OpenUI();
    }

    private void OnGuildRaidBattleStart(GameObject go)
    {
        SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        //if (UserInfo.Instance.InventoryInfo.InvenCreatureCount >= UserInfo.Instance.MaxCreatureInvenSize)
        //{
        //    SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3896), StringTableManager.GetData(3894));
        //    return;
        //}

        //if (UserInfo.Instance.InventoryInfo.listInvenItem.Count >= UserInfo.Instance.MaxItemInvenSize)
        //{
        //    SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3896), StringTableManager.GetData(3893));
        //    return;
        //}

        if (UserInfo.Instance.GuildRaidTicket < 1)
        {
            // 8748    입장 횟수가 부족합니다.
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), StringTableManager.GetData(8748));
            return;
        }

        // 결과 요청할때 필요한 정보.
        UserInfo.Instance.guildRaidInfo.guildRaidType = _guildRaidType;
        UserInfo.Instance.guildRaidInfo.guildRaidScore = _myGuildRaidScore;

        UserInfo.Instance.CreatureTeam.MyTeamIndex = _TeamMenu.UITeamIndex;
        //stBattleStartReq.kBattleTeamIndex = UtilFunc.ChangeTeamIdx2Enum(UserInfo.Instance.CreatureTeam.MyTeamIndex);

        CNetManager.Instance.GuildRaidStub.OnGuildRaidBattleStart = RecvGuildRaidBattleStart;

        _stGuildRaidBattleStartReq stGuildRaidBattleStartReq = new _stGuildRaidBattleStartReq();
        stGuildRaidBattleStartReq.kStageID = _guildRaidTable.MapStage;
        stGuildRaidBattleStartReq.kStageLevel = _guildRaidTable.GuildRaidID;
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidBattleStart, stGuildRaidBattleStartReq, typeof(_stGuildRaidBattleStartAck));
    }
}
