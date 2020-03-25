using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public enum enGuildRaidDifficulty
{
    None = 0,
    Easy,
    Normal,
    Hard,
}

/// <summary>
/// 진척도 나타내는 보상아이콘, 보상갯수, 진척도 포인트
/// </summary>
[System.Serializable]
public class GuildRaidProgress
{
    [SerializeField] private UISprite _JewelrySprite;
    [SerializeField] private UISprite _CheckSprite;
    [SerializeField] private UILabel _RewardCountLabel;
    [SerializeField] private UILabel _ProgressPointLabel;

    public void Init(DATA_ITEM_NEW.enIndex RaidRewardItemIndex, int score, int rewardCount, float progressValue)
    {
        //if(CDATA_ITEM_NEW.GetCount() < 1)
        //    CDATA_ITEM_NEW.Load();

        //DATA_ITEM_NEW RaidRewardItem = CDATA_ITEM_NEW.Get(RaidRewardItemIndex);

        _JewelrySprite.spriteName = "GRaid_card01";

        _ProgressPointLabel.text = string.Format(StringTableManager.GetData(8679), score);
        _RewardCountLabel.text = string.Format(StringTableManager.GetData(8665), rewardCount);

        _CheckSprite.gameObject.SetActive(progressValue >= score);
    }
}

public class GuildRaidWindow : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _CloseButton;
    [SerializeField] private UILabel _CloseButtonLabel;

    [SerializeField] private List<GuildRaidProgress> _ProgressList = new List<GuildRaidProgress>();

    [SerializeField] private UISprite _Gage;

    [SerializeField] private UILabel _PointTitleLabel;
    [SerializeField] private UILabel _PointLabel;

    [SerializeField] private GameObject _HelpTipButton;

    [SerializeField] private GameObject _RankingButton;

    /////////////////////////////////// 오픈중 ///////////////////////////////////
    [SerializeField] private GameObject _RaidOpen;                      // 오픈중.

    [SerializeField] private GameObject _RaidEasyButton;
    [SerializeField] private GameObject _RaidEasyButtonNew;
    [SerializeField] private UILabel _RaidEasyButtonLabel;
    [SerializeField] private UI2DSprite _RaidEasyButtonSprite;

    [SerializeField] private GameObject _RaidNormalButton;
    [SerializeField] private GameObject _RaidNormalButtonNew;
    [SerializeField] private UILabel _RaidNormalButtonLabel;
    [SerializeField] private UI2DSprite _RaidNormalButtonSprite;
    [SerializeField] private UILabel _RaidNormalText1;
    [SerializeField] private UILabel _RaidNormalText2;

    [SerializeField] private GameObject _RaidHardButton;
    [SerializeField] private GameObject _RaidHardButtonNew;
    [SerializeField] private UILabel _RaidHardButtonLabel;
    [SerializeField] private UI2DSprite _RaidHardButtonSprite;
    [SerializeField] private UILabel _RaidHardText1;
    [SerializeField] private UILabel _RaidHardText2;
    [SerializeField] private UILabel _RaidHardText3;
    /////////////////////////////////// 오픈중 ///////////////////////////////////


    /////////////////////////////////// 정산중 ///////////////////////////////////
    [SerializeField] private GameObject _RaidCalcurate;                 // 정산중.

    [SerializeField] private UILabel _RaidCalcurateLabel;
    /////////////////////////////////// 정산중 ///////////////////////////////////


    /////////////////////////////////// 보상중 ///////////////////////////////////
    [SerializeField] private GameObject _RaidReward;                    // 보상중.

    [SerializeField] private UILabel _RaidRewardLabel;

    [SerializeField] private GameObject _RaidRewardButton;
    [SerializeField] private UILabel _RaidRewardButtonLabel;

    [SerializeField] private UILabel _RaidRewardTimeLabel;
    /////////////////////////////////// 보상중 ///////////////////////////////////

    [SerializeField] private UILabel _BottomNoticeLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildRaidReady _GuildRaidReady = null;

    private GuildRaidRankingPopup _GuildRaidRankingPopup = null;

    private GuildRaidReward _GuildRaidReward = null;

    private SimpleHelpTip _SimpleHelpTip = null;

    private _stGuildRaidInfoAck _guildRaidInfoAck = null;       // 서버데이터.

    List<DATA_GUILDRAID> _todayGuildRaidTableList = new List<DATA_GUILDRAID>();

    private DATA_GUILDRAID.Enum _currentGuildRaidType = DATA_GUILDRAID.Enum.GUILDRAID_NONE;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick = OnClickBack;
        if (_RankingButton != null) UIEventListener.Get(_RankingButton).onClick = OnRanking;

        if (_RaidEasyButton != null) UIEventListener.Get(_RaidEasyButton).onClick = OnEasyOpen;
        if (_RaidNormalButton != null) UIEventListener.Get(_RaidNormalButton).onClick = OnNormalOpen;
        if (_RaidHardButton != null) UIEventListener.Get(_RaidHardButton).onClick = OnHardOpen;

        if (_RaidRewardButton != null) UIEventListener.Get(_RaidRewardButton).onClick = OnReward;

        if (_HelpTipButton != null) UIEventListener.Get(_HelpTipButton).onPress = OnHelpTooltip;
    }

    protected override void OnDestroy()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick -= OnClickBack;
        if (_RankingButton != null) UIEventListener.Get(_RankingButton).onClick -= OnRanking;

        if (_RaidEasyButton != null) UIEventListener.Get(_RaidEasyButton).onClick -= OnEasyOpen;
        if (_RaidNormalButton != null) UIEventListener.Get(_RaidNormalButton).onClick -= OnNormalOpen;
        if (_RaidHardButton != null) UIEventListener.Get(_RaidHardButton).onClick -= OnHardOpen;

        if (_RaidRewardButton != null) UIEventListener.Get(_RaidRewardButton).onClick -= OnReward;

        if (_HelpTipButton != null) UIEventListener.Get(_HelpTipButton).onPress -= OnHelpTooltip;

        _todayGuildRaidTableList.Clear();
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
        bool isGuildRaidTicketFull = UserInfo.Instance.otherNew.isGuildRaidTicketFull;
        _RaidEasyButtonNew.SetActive(isGuildRaidTicketFull);
        _RaidNormalButtonNew.SetActive(isGuildRaidTicketFull);
        _RaidHardButtonNew.SetActive(isGuildRaidTicketFull);
    }

    public override void Clear()
    {
    }

    public override void Init()
    {
        _CloseButtonLabel.text = StringTableManager.GetData(6753);        // 6753	길드 레이드

        _PointTitleLabel.text = StringTableManager.GetData(8666);       // 8666 현재 점수

        _RaidEasyButtonLabel.text = StringTableManager.GetData(81);       // 81 쉬움
        _RaidNormalButtonLabel.text = StringTableManager.GetData(82);       // 82 보통
        _RaidHardButtonLabel.text = StringTableManager.GetData(83);       // 83 어려움

        _RaidRewardLabel.text = StringTableManager.GetData(8669);     // 8669 현재 진행중인 레이드가 아닌 이전 레이드 결과의 보상입니다.
        _RaidRewardButtonLabel.text = StringTableManager.GetData(8670);     // 8670 보상 받기

        _BottomNoticeLabel.text = StringTableManager.GetData(8668);         // 8668 진척도 보상은 모든 길드원이 공유하며 매일 0시에 초기화 됩니다.

        _RaidNormalText1.text = StringTableManager.GetData(8766);       // 8766    몬스터의 피해량이 증가합니다.
        _RaidNormalText2.text = StringTableManager.GetData(8767);       // 8767    증원되는 몬스터의 수가 증가합니다.

        _RaidHardText1.text = StringTableManager.GetData(8768);     // 8768    몬스터의 피해량이 크게 증가합니다.
        _RaidHardText2.text = StringTableManager.GetData(8769);     // 8769    보스 몬스터의 상태이상 확률이 크게 증가합니다.
        _RaidHardText3.text = StringTableManager.GetData(8770);     // 8770    아군을 방해하는 강력한 몬스터가 추가로 등장합니다.

        _RaidEasyButtonNew.SetActive(false);
        _RaidNormalButtonNew.SetActive(false);
        _RaidHardButtonNew.SetActive(false);
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void SetRecvData(_stGuildRaidInfoAck GuildRaidInfoAck)
    {
        _guildRaidInfoAck = GuildRaidInfoAck;

        UserInfo.Instance.guildRaidInfo.guildRaidDay = _guildRaidInfoAck.kGuildRaidDay;

        if (CDATA_GUILDRAID.GetCount() < 1)
            CDATA_GUILDRAID.Load();

        _todayGuildRaidTableList.Clear();
        for (int i = 0; i < CDATA_GUILDRAID.GetCount(); ++i)
        {
            DATA_GUILDRAID GuildRaidTable = CDATA_GUILDRAID.GetByIndex(i);
            if((int)GuildRaidTable.SetDay == _guildRaidInfoAck.kGuildRaidDay)
            {
                _todayGuildRaidTableList.Add(GuildRaidTable);
            }
        }

        DATA_GUILDRAID_PROGRESS._ProgressEnum ProgressEnum = DATA_GUILDRAID_PROGRESS._ProgressEnum._ProgressEnum_Max;

        foreach (DATA_GUILDRAID data in _todayGuildRaidTableList)
        {
            if (data.SetLevel == (int)enGuildRaidDifficulty.Easy)
            {
                _RaidEasyButtonSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_GUILDRAID, data.RaidCardImage);
            }
            else if (data.SetLevel == (int)enGuildRaidDifficulty.Normal)
            {
                _RaidNormalButtonSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_GUILDRAID, data.RaidCardImage);
            }
            else if (data.SetLevel == (int)enGuildRaidDifficulty.Hard)
            {
                _RaidHardButtonSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_GUILDRAID, data.RaidCardImage);
            }

            ProgressEnum = data.ProgressEnum;
        }

        DATA_GUILDRAID_PROGRESS GuildRaidProgressTable = CDATA_GUILDRAID_PROGRESS.Get(ProgressEnum);

        ulong kGuildRaidCurrDailyScore = _guildRaidInfoAck.kGuildRaidInfo.kGuildRaidCurrDailyScore;
        SetProgress(GuildRaidProgressTable, kGuildRaidCurrDailyScore);
        SetProgressGage(GuildRaidProgressTable.MaxScore, kGuildRaidCurrDailyScore);

        _PointLabel.text = string.Format(StringTableManager.GetData(8667), kGuildRaidCurrDailyScore);        // 8667 {0} 점

        // 길드레이드 오픈상태 설정. (오픈중, 정산중, 보상중)
        {
            _RaidOpen.SetActive(false);
            _RaidCalcurate.SetActive(false);
            _RaidReward.SetActive(false);

            //GameObject ActiveObj = null;

            if (UserInfo.Instance.GuildRaidRewardState == _enGuildRaidRewardState.enGuildRaidRewardState_Recv)      // 보상중
            {
                _RaidReward.SetActive(true);
                //ActiveObj = _RaidReward;

                TimeSpan ts = _guildRaidInfoAck.kEndDate.GetDateTime() - TimeManager.Instance.GetServerTime();
                _RaidRewardTimeLabel.text = string.Format(StringTableManager.GetData(4915), ts.Hours, ts.Minutes);       // 4915 {0}시간 {1}분 남음
            }
            else
            {
                TimeSpan ts = TimeManager.Instance.GetServerTime() - _guildRaidInfoAck.kStartDate.GetDateTime();

                if (ts.TotalSeconds > 0)       // 오픈중
                {
                    _RaidOpen.SetActive(true);
                    //ActiveObj = _RaidOpen;
                }
                else                                // 정산중
                {
                    _RaidCalcurate.SetActive(true);
                    //ActiveObj = _RaidCalcurate;

                    TimeSpan calcurateTimeSpan = _guildRaidInfoAck.kStartDate.GetDateTime() - TimeManager.Instance.GetServerTime();

                    _RaidCalcurateLabel.text = string.Format(StringTableManager.GetData(8673), calcurateTimeSpan.Minutes);   // 8673 정산 중입니다. 정산에는 n분이 소요되며 해당 레이드에 참여 했다면 정산 후 보상을 받을 수 있습니다.
                }
            }

            //Transform[] childs = ActiveObj.GetComponentsInChildren<Transform>(true);
            //foreach (Transform child in childs)
            //{
            //    child.gameObject.SetActive(true);
            //}
        }
    }

    private void SetProgress(DATA_GUILDRAID_PROGRESS GuildRaidProgressTable, float progressValue)
    {
        if (_ProgressList.Count < 1 || _ProgressList.Count != 4)
        {
#if DEBUG_LOG
            Debug.LogError("프리팹에 있는 Progress가 연결끊어짐.");
#endif
            return;
        }

        _ProgressList[0].Init(GuildRaidProgressTable.RaidRewardItemIndex, GuildRaidProgressTable.ScoreLevel1, GuildRaidProgressTable.RaidRewardValue1, progressValue);
        _ProgressList[1].Init(GuildRaidProgressTable.RaidRewardItemIndex, GuildRaidProgressTable.ScoreLevel2, GuildRaidProgressTable.RaidRewardValue2, progressValue);
        _ProgressList[2].Init(GuildRaidProgressTable.RaidRewardItemIndex, GuildRaidProgressTable.ScoreLevel3, GuildRaidProgressTable.RaidRewardValue3, progressValue);
        _ProgressList[3].Init(GuildRaidProgressTable.RaidRewardItemIndex, GuildRaidProgressTable.ScoreLevel4, GuildRaidProgressTable.RaidRewardValue4, progressValue);
    }

    private void SetProgressGage(int MaxScore, float progressValue)
    {
        float gageFillAmount = progressValue;

        if (gageFillAmount < 0.0f)
            gageFillAmount = 0.0f;

        if (gageFillAmount > 0.0f)
        {
            gageFillAmount *= (1.0f / MaxScore);        // MaxScore == 0 error.
            if (gageFillAmount > 1.0f)
                gageFillAmount = 1.0f;
        }

        _Gage.fillAmount = gageFillAmount;
    }

    private void RecvGuildRaidReward(_stGuildRaidGetRewardAck stAck)
    {
        UserInfo.Instance.SetWealth(DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_GUILDTOKEN, stAck.kUpdateToken);

        // 8747    보상으로 마의 파편 {0}개를 얻었습니다.
        SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), string.Format(StringTableManager.GetData(8747), stAck.kAddToken));

        // 정보를 다시 요청해서 시간값과 버튼상태 갱신.
        CNetManager.Instance.GuildRaidStub.OnGuildRaidInfo = SetRecvData;
        _stGuildRaidInfoReq stGuildRaidInfoReq = new _stGuildRaidInfoReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidInfo, stGuildRaidInfoReq, typeof(_stGuildRaidInfoAck));
    }

    private void RecvGuildRaidRankList(_stGuildRaidGetRankListAck stAck)
    {
        if (_GuildRaidRankingPopup == null)
        {
            _GuildRaidRankingPopup = UIResourceMgr.CreatePrefab<GuildRaidRankingPopup>(BUNDLELIST.PREFABS_UI_GUILDRAID, transform, "GuildRaidRankingPopup");
            _GuildRaidRankingPopup.Init();

            UIControlManager.instance.AddWindow(enUIType.GUILDRAIDRANKING, _GuildRaidRankingPopup);
        }

        _GuildRaidRankingPopup.OpenUI();
        _GuildRaidRankingPopup.SetRecvData(stAck);
    }

    public void RequestGuildRaidMemberRankList(_enGuildRaidRankType kRankType)
    {
        UserInfo.Instance.guildRaidInfo.guildRaidRankType = kRankType;

        CNetManager.Instance.GuildRaidStub.OnGuildRaidMemberRankList = RecvGuildRaidMemberRankList;

        _stGuildRaidGetMemberRankListReq stGuildRaidGetMemberRankListReq = new _stGuildRaidGetMemberRankListReq();
        stGuildRaidGetMemberRankListReq.kRankType = kRankType;
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidGetMemberRankList, stGuildRaidGetMemberRankListReq, typeof(_stGuildRaidGetMemberRankListAck));
    }

    private void RecvGuildRaidMemberRankList(_stGuildRaidGetMemberRankListAck stAck)
    {
        if (_GuildRaidReady == null)
        {
            _GuildRaidReady = UIResourceMgr.CreatePrefab<GuildRaidReady>(BUNDLELIST.PREFABS_UI_GUILDRAID, transform, "GuildRaidReady");
            _GuildRaidReady.Init(this);

            UIControlManager.instance.AddWindow(enUIType.GUILDRAIDREADY, _GuildRaidReady);
        }

        _GuildRaidReady.SetRecvData(_currentGuildRaidType, stAck, _guildRaidInfoAck.kEndDate.GetDateTime());
        _GuildRaidReady.OpenUI();
    }

    public void ReopenGuildRaidReady()
    {
        // 날짜가 변경됐을때 처리.
        if (UserInfo.Instance.guildRaidInfo.guildRaidDay != _guildRaidInfoAck.kGuildRaidDay)
            return;

        _currentGuildRaidType = UserInfo.Instance.guildRaidInfo.guildRaidType;

        RequestGuildRaidMemberRankList(UserInfo.Instance.guildRaidInfo.guildRaidRankType);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnRanking(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        CNetManager.Instance.GuildRaidStub.OnGuildRaidRankList = RecvGuildRaidRankList;

        _stGuildRaidGetRankListReq stGuildRaidGetRankListReq = new _stGuildRaidGetRankListReq();
        stGuildRaidGetRankListReq.kRankType = _enGuildRaidRankType.enGuildRaidRankType_Total_Week;
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidGetRankList, stGuildRaidGetRankListReq, typeof(_stGuildRaidGetRankListAck));
    }

    private void OnEasyOpen(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        foreach(DATA_GUILDRAID data in _todayGuildRaidTableList)
        {
            if (data.SetLevel == (int)enGuildRaidDifficulty.Easy)
            {
                _currentGuildRaidType = data.GuildRaidID;
                break;
            }
        }

        RequestGuildRaidMemberRankList(_enGuildRaidRankType.enGuildRaidRankType_Member_Easy);
    }

    private void OnNormalOpen(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        foreach (DATA_GUILDRAID data in _todayGuildRaidTableList)
        {
            if (data.SetLevel == (int)enGuildRaidDifficulty.Normal)
            {
                _currentGuildRaidType = data.GuildRaidID;
                break;
            }
        }

        RequestGuildRaidMemberRankList(_enGuildRaidRankType.enGuildRaidRankType_Member_Normal);
    }

    private void OnHardOpen(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        foreach (DATA_GUILDRAID data in _todayGuildRaidTableList)
        {
            if (data.SetLevel == (int)enGuildRaidDifficulty.Hard)
            {
                _currentGuildRaidType = data.GuildRaidID;
                break;
            }
        }

        RequestGuildRaidMemberRankList(_enGuildRaidRankType.enGuildRaidRankType_Member_Hard);
    }

    private void OnReward(GameObject go)
    {
        if (UserInfo.Instance.GuildRaidRewardState == _enGuildRaidRewardState.enGuildRaidRewardState_None
            || UserInfo.Instance.GuildRaidRewardState == _enGuildRaidRewardState.enGuildRaidRewardState_Complete)
            return;

        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        CNetManager.Instance.GuildRaidStub.OnGuildRaidGetReward = RecvGuildRaidReward;
        _stGuildRaidGetRewardReq stGuildRaidGetRewardReq = new _stGuildRaidGetRewardReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidGetReward, stGuildRaidGetRewardReq, typeof(_stGuildRaidGetRewardAck));
    }

    private void OnHelpTooltip(GameObject go, bool state)
    {
        if (state == true)
        {
            if (_SimpleHelpTip == null)
            {
                _SimpleHelpTip = UIResourceMgr.CreatePrefab<SimpleHelpTip>(BUNDLELIST.PREFABS_UI_MAINMENU, _HelpTipButton.transform, "SimpleHelpTip");
                _SimpleHelpTip.Init(24);
            }

            _SimpleHelpTip.OpenUI();
        }
        else
        {
            _SimpleHelpTip.CloseUI();
        }
    }
}
