using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;


public enum enTributeType
{
    Free,
    Good,
    Extra
}

public class GuildGoddness : UIWindowPopup
{
    //===================================================================================
    //
    // Inner Class
    //
    //===================================================================================
    public class TributeData
    {
        public Sprite _GoddnessSprite = null;
        public string _TributeButtonText = null;
        public DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType _PriceType = DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_NONE;
        public int _Price = 0;
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _CloseButton;

    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private UILabel _GuildLevelTitleLabel;
    [SerializeField] private GameObject _LeftButton;
    [SerializeField] private GameObject _RightButton;

    [SerializeField] private UILabel _FreeGoddnessTitleLabel;
    [SerializeField] private UI2DSprite _FreeGoddness2DSprite;                      // 여신이미지.
    [SerializeField] private GameObject _FreeTributeButton;                         // 무료공물.
    [SerializeField] private UILabel _FreeTributeButtonLabel;                       // 무료공물버튼.
    [SerializeField] private GameObject _FreeTributeTooltipButton;                  // 무료공물 툴팁버튼.
    [SerializeField] private UIScrollView _FreeTributeScrollView;
    [SerializeField] private UIGrid _FreeTributeGrid;

    [SerializeField] private UILabel _GoodGoddnessTitleLabel;
    [SerializeField] private UI2DSprite _GoodGoddness2DSprite;                      // 여신이미지.
    [SerializeField] private GameObject _GoodTributeButton;                         // 고급공물버튼.
    [SerializeField] private UILabel _GoodTributeButtonLabel;                       // 고급공물버튼 라벨.
    [SerializeField] private UISprite _GoodTributePriceTypeSprite;                  // 고급공물 재화 이미지.
    [SerializeField] private UILabel _GoodTributePriceTypeLabel;                    // 고급공물 재화 라벨.
    [SerializeField] private GameObject _GoodTributeTooltipButton;                  // 고급공물 툴팁버튼.
    [SerializeField] private UIScrollView _GoodTributeScrollView;
    [SerializeField] private UIGrid _GoodTributeGrid;

    [SerializeField] private UILabel _ExtraGoddnessTitleLabel;
    [SerializeField] private UI2DSprite _ExtraGoddness2DSprite;                      // 여신이미지.
    [SerializeField] private GameObject _ExtraTributeButton;                         // 최고급공물.
    [SerializeField] private UILabel _ExtraTributeButtonLabel;                       // 최고급공물버튼 라벨.
    [SerializeField] private UISprite _ExtraTributePriceTypeSprite;                  // 최고급 공물 재화 이미지.
    [SerializeField] private UILabel _ExtraTributePriceTypeLabel;                    // 최고급 공물 재화 라벨.
    [SerializeField] private GameObject _ExtraTributeTooltipButton;                  // 최고급 공물 툴팁버튼.
    [SerializeField] private UIScrollView _ExtraTributeScrollView;
    [SerializeField] private UIGrid _ExtraTributeGrid;

    [SerializeField] private UILabel _BottomLevelMissMatchLabel;

    [SerializeField] private UILabel _BottomLabel;

    [SerializeField] private GameObject _HelpButton;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildGoddnessItemInfo _GuildGoddnessItemInfoPopup = null;

    private DATA_GUILD_MAIN _GuildMainData = null;

    private SimpleHelpTip _SimpleHelpTip = null;

    private List<GuildGoddnessText> _GuildGoddnessTextList = new List<GuildGoddnessText>();

    private readonly string _GuildGoddnessText = "GuildGoddnessText";
    private readonly string _GuildGoddnessItemInfo = "GuildGoddnessItemInfo";

    private DATA_GUILD_MAIN._enGuildLv _MyGuildLv = DATA_GUILD_MAIN._enGuildLv._enGuildLv_Max;
    private DATA_GUILD_MAIN._enGuildLv _DisplayGuildLv = DATA_GUILD_MAIN._enGuildLv._enGuildLv_Max;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick = OnClickBack;
        //if (_FreeTributeTooltipButton != null) UIEventListener.Get(_FreeTributeTooltipButton).onClick = OnFreeTributeTooltip;
        //if (_GoodTributeTooltipButton != null) UIEventListener.Get(_GoodTributeTooltipButton).onClick = OnGoodTributeTooltip;
        //if (_ExtraTributeTooltipButton != null) UIEventListener.Get(_ExtraTributeTooltipButton).onClick = OnExtraTributeTooltip;
        if (_FreeTributeButton != null) UIEventListener.Get(_FreeTributeButton).onClick = OnFreeTribute;
        if (_GoodTributeButton != null) UIEventListener.Get(_GoodTributeButton).onClick = OnGoodTribute;
        if (_ExtraTributeButton != null) UIEventListener.Get(_ExtraTributeButton).onClick = OnExtraTribute;
        if (_LeftButton != null) UIEventListener.Get(_LeftButton).onClick = OnLeftClick;
        if (_RightButton != null) UIEventListener.Get(_RightButton).onClick = OnRightClick;

        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress = OnHelpTooltip;
    }

    protected override void OnDestroy()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick -= OnClickBack;
        //if (_FreeTributeTooltipButton != null) UIEventListener.Get(_FreeTributeTooltipButton).onClick -= OnFreeTributeTooltip;
        //if (_GoodTributeTooltipButton != null) UIEventListener.Get(_GoodTributeTooltipButton).onClick -= OnGoodTributeTooltip;
        //if (_ExtraTributeTooltipButton != null) UIEventListener.Get(_ExtraTributeTooltipButton).onClick -= OnExtraTributeTooltip;
        if (_FreeTributeButton != null) UIEventListener.Get(_FreeTributeButton).onClick -= OnFreeTribute;
        if (_GoodTributeButton != null) UIEventListener.Get(_GoodTributeButton).onClick -= OnGoodTribute;
        if (_ExtraTributeButton != null) UIEventListener.Get(_ExtraTributeButton).onClick -= OnExtraTribute;
        if (_LeftButton != null) UIEventListener.Get(_LeftButton).onClick -= OnLeftClick;
        if (_RightButton != null) UIEventListener.Get(_RightButton).onClick -= OnRightClick;

        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress -= OnHelpTooltip;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(CGuildDetail guildDetail)
    {
        // 6752 봉헌
        _TitleLabel.text = StringTableManager.GetData(6752);

        // 6896    봉헌을 통한 여신의 가호 효과는 모험지역에서만 적용됩니다.
        _BottomLabel.text = StringTableManager.GetData(6896);

        // grid sort setting
        {
            _FreeTributeGrid.sorting = UIGrid.Sorting.Custom;
            _FreeTributeGrid.onCustomSort = SortByCustomType;

            _GoodTributeGrid.sorting = UIGrid.Sorting.Custom;
            _GoodTributeGrid.onCustomSort = SortByCustomType;

            _ExtraTributeGrid.sorting = UIGrid.Sorting.Custom;
            _ExtraTributeGrid.onCustomSort = SortByCustomType;
        }

        _MyGuildLv = guildDetail.kGuildLevel;
        SetGoddnessData(guildDetail.kGuildLevel);

        DATA_GUILD_MAIN GuildMainTableData = CDATA_GUILD_MAIN.Get(guildDetail.kGuildLevel);
        if (GuildMainTableData != null)
        {
            // 8289    길드 레벨과 맞지 않는 봉헌은 이용할 수 없습니다. (이용 가능 봉헌 : Lv.{0})
            _BottomLevelMissMatchLabel.text = string.Format(StringTableManager.GetData(8289), GuildMainTableData.iGuildLv);
        }

        if (_FreeTributeTooltipButton != null) _FreeTributeTooltipButton.gameObject.SetActive(false);
        if (_GoodTributeTooltipButton != null) _GoodTributeTooltipButton.gameObject.SetActive(false);
        if (_ExtraTributeTooltipButton != null) _ExtraTributeTooltipButton.gameObject.SetActive(false);
    }

    private void SetGoddnessData(DATA_GUILD_MAIN._enGuildLv kGuildLevel)
    {
        _DisplayGuildLv = kGuildLevel;
        _GuildMainData = CDATA_GUILD_MAIN.Get(_DisplayGuildLv);
        if (_GuildMainData == null)
            return;     // error

        int GuildLevel = _GuildMainData.iGuildLv;
        _GuildLevelTitleLabel.text = string.Format(StringTableManager.GetData(8288), GuildLevel);      // 8288    Lv.{0} 길드 봉헌

        for (int i = 0; i < _GuildGoddnessTextList.Count; ++i)
        {
            DestroyImmediate(_GuildGoddnessTextList[i].gameObject);
        }
        _GuildGoddnessTextList.Clear();

        // Free
        TributeData tributeData = SetTributeText(_GuildMainData.enTributeFree, _FreeTributeGrid.transform);
        if (tributeData != null)
        {
            // 6900	{0}단계 여신의 숨결
            _FreeGoddnessTitleLabel.text = string.Format(StringTableManager.GetData(6900), GuildLevel);
            _FreeGoddness2DSprite.sprite2D = tributeData._GoddnessSprite;
            _FreeTributeButtonLabel.text = tributeData._TributeButtonText;
        }

        // Good
        tributeData = SetTributeText(_GuildMainData.enTributeGood, _GoodTributeGrid.transform);
        if (tributeData != null)
        {
            // 6901	{0}단계 눈부신 여신의 가호
            _GoodGoddnessTitleLabel.text = string.Format(StringTableManager.GetData(6901), GuildLevel);
            _GoodGoddness2DSprite.sprite2D = tributeData._GoddnessSprite;
            _GoodTributeButtonLabel.text = tributeData._TributeButtonText;
            _GoodTributePriceTypeLabel.text = tributeData._Price.ToString();

            _GoodTributePriceTypeSprite.spriteName = UtilFunc.GetWealthIconName(tributeData._PriceType);
        }

        // Extra
        tributeData = SetTributeText(_GuildMainData.enTributeExtra, _ExtraTributeGrid.transform);
        if (tributeData != null)
        {
            // 6902	{0}단계 성스러운 여신의 축복
            _ExtraGoddnessTitleLabel.text = string.Format(StringTableManager.GetData(6902), GuildLevel);
            _ExtraGoddness2DSprite.sprite2D = tributeData._GoddnessSprite;
            _ExtraTributeButtonLabel.text = tributeData._TributeButtonText;
            _ExtraTributePriceTypeLabel.text = tributeData._Price.ToString();

            _ExtraTributePriceTypeSprite.spriteName = UtilFunc.GetWealthIconName(tributeData._PriceType);
        }

        if(_MyGuildLv == kGuildLevel)
        {
            _FreeTributeButton.SetActive(true);
            _GoodTributeButton.SetActive(true);
            _ExtraTributeButton.SetActive(true);

            _BottomLevelMissMatchLabel.gameObject.SetActive(false);
        }
        else
        {
            _FreeTributeButton.SetActive(false);
            _GoodTributeButton.SetActive(false);
            _ExtraTributeButton.SetActive(false);

            _BottomLevelMissMatchLabel.gameObject.SetActive(true);
        }

        ResetPosition();
    }

    private TributeData SetTributeText(DATA_GUILD_TRIBUTE._enTributeEnum TributeEnum, Transform TributeInfoGrid)
    {
        DATA_GUILD_TRIBUTE GuildTributeData = CDATA_GUILD_TRIBUTE.Get(TributeEnum);
        if (GuildTributeData == null)
            return null;     // error

        TributeData tributeData = new TributeData();

        tributeData._PriceType = GuildTributeData.enPriceType;
        tributeData._Price = GuildTributeData.iPriceValue;
        tributeData._GoddnessSprite = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GODDNESS, GuildTributeData.sTributeObj);
        tributeData._TributeButtonText = StringTableManager.GetData(GuildTributeData.iTributeTitle);

        if (GuildTributeData.iGexp > 0)
        {
            GuildGoddnessText GuildExpText = UIResourceMgr.CreatePrefab<GuildGoddnessText>(BUNDLELIST.PREFABS_UI_GUILD, TributeInfoGrid, _GuildGoddnessText);
            GuildExpText.SetText(string.Format(StringTableManager.GetData(6889), GuildTributeData.iGexp), GuildGoddnessText.enGuildGoddnessText_Type.GuildExp); // 6889    길드 경험치 +{0}

            _GuildGoddnessTextList.Add(GuildExpText);

            GuildGoddnessText GuildContributionText = UIResourceMgr.CreatePrefab<GuildGoddnessText>(BUNDLELIST.PREFABS_UI_GUILD, TributeInfoGrid, _GuildGoddnessText);
            GuildContributionText.SetText(string.Format("{0} +{1}", StringTableManager.GetData(6905), GuildTributeData.iGexp), GuildGoddnessText.enGuildGoddnessText_Type.GuildContribution); // 6905	기여도

            _GuildGoddnessTextList.Add(GuildContributionText);
        }

        float Percent = 0.0f;
        if (GuildTributeData.fbuff_Gold > 0)
        {
            Percent = (GuildTributeData.fbuff_Gold * 100);
            GuildGoddnessText BuffGoldText = UIResourceMgr.CreatePrefab<GuildGoddnessText>(BUNDLELIST.PREFABS_UI_GUILD, TributeInfoGrid, _GuildGoddnessText);
            BuffGoldText.SetText(string.Format(StringTableManager.GetData(6890), Percent.ToString("F2")), GuildGoddnessText.enGuildGoddnessText_Type.BuffGold); // 6890    획득 골드 +{0}%

            _GuildGoddnessTextList.Add(BuffGoldText);
        }

        // 기획팀 제거요청.
        //if (GuildTributeData.fbuff_Pexp > 0)
        //{
        //    Percent = (GuildTributeData.fbuff_Pexp * 100);
        //    GuildGoddnessText BuffUserExpPercentText = UIResourceMgr.CreatePrefab<GuildGoddnessText>(BUNDLELIST.PREFABS_UI_GUILD, TributeInfoGrid, _GuildGoddnessText);
        //    BuffUserExpPercentText.SetText(string.Format(StringTableManager.GetData(6891), Percent.ToString("F2")), GuildGoddnessText.enGuildGoddnessText_Type.BuffUserExpPercent); // 6891    획득 플레이어 경험치 +{0}%

        //    _GuildGoddnessTextList.Add(BuffUserExpPercentText);
        //}


        if (GuildTributeData.fbuff_Cexp > 0)
        {
            Percent = (GuildTributeData.fbuff_Cexp * 100);
            GuildGoddnessText BuffCreatureExpPercentText = UIResourceMgr.CreatePrefab<GuildGoddnessText>(BUNDLELIST.PREFABS_UI_GUILD, TributeInfoGrid, _GuildGoddnessText);
            BuffCreatureExpPercentText.SetText(string.Format(StringTableManager.GetData(6892), Percent.ToString("F2")), GuildGoddnessText.enGuildGoddnessText_Type.BuffCreatureExpPercent); // 6892    획득 크리쳐 경험치 +{0}%

            _GuildGoddnessTextList.Add(BuffCreatureExpPercentText);
        }

        // 기획팀 제거요청.
        //if (GuildTributeData.iDuration > 0)
        //{
        //    GuildGoddnessText BuffDurationText = UIResourceMgr.CreatePrefab<GuildGoddnessText>(BUNDLELIST.PREFABS_UI_GUILD, TributeInfoGrid, _GuildGoddnessText);
        //    BuffDurationText.SetText(string.Format(StringTableManager.GetData(6897), GuildTributeData.iDuration), GuildGoddnessText.enGuildGoddnessText_Type.BuffDuration);       // 6897    가호 지속시간 : {0}분

        //    _GuildGoddnessTextList.Add(BuffDurationText);
        //}

        if (GuildTributeData.iKeyAmount > 0)
        {
            GuildGoddnessText GuildRewardKeyText = UIResourceMgr.CreatePrefab<GuildGoddnessText>(BUNDLELIST.PREFABS_UI_GUILD, TributeInfoGrid, _GuildGoddnessText);
            GuildRewardKeyText.SetText(string.Format(StringTableManager.GetData(8799), GuildTributeData.iKeyAmount), GuildGoddnessText.enGuildGoddnessText_Type.BuffRewardKey); // 

            _GuildGoddnessTextList.Add(GuildRewardKeyText);
        }

        return tributeData;
    }

    private int SortByCustomType(Transform a, Transform b)
    {
        GuildGoddnessText guildGoddnessText1 = a.GetComponent<GuildGoddnessText>();
        if (guildGoddnessText1 == null)
            return 0;

        GuildGoddnessText guildGoddnessText2 = b.GetComponent<GuildGoddnessText>();
        if (guildGoddnessText2 == null)
            return 0;

        return guildGoddnessText1.TextType.CompareTo(guildGoddnessText2.TextType);
    }

    private void ResetPosition()
    {
        _FreeTributeGrid.Reposition();
        _FreeTributeScrollView.ResetPosition();

        _GoodTributeGrid.Reposition();
        _GoodTributeScrollView.ResetPosition();

        _ExtraTributeGrid.Reposition();
        _ExtraTributeScrollView.ResetPosition();
    }
    
    //private void OpenGoddnessInfoPopup(DATA_GUILD_TRIBUTE._enTributeEnum enTribute)
    //{
    //    if (_GuildGoddnessItemInfoPopup == null)
    //    {
    //        _GuildGoddnessItemInfoPopup = UIResourceMgr.CreatePrefab<GuildGoddnessItemInfo>(BUNDLELIST.PREFABS_UI_GUILD, transform, _GuildGoddnessItemInfo);
    //        _GuildGoddnessItemInfoPopup.Init();
    //    }

    //    _GuildGoddnessItemInfoPopup.SetData(enTribute);
    //    _GuildGoddnessItemInfoPopup.OpenUI();
    //}

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    //private void OnFreeTributeTooltip(GameObject go)
    //{
    //    if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

    //    OpenGoddnessInfoPopup(_GuildMainData.enTributeFree);
    //}

    //private void OnGoodTributeTooltip(GameObject go)
    //{
    //    if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
    //    OpenGoddnessInfoPopup(_GuildMainData.enTributeGood);
    //}

    //private void OnExtraTributeTooltip(GameObject go)
    //{
    //    if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
    //    OpenGoddnessInfoPopup(_GuildMainData.enTributeExtra);
    //}

    private void OnFreeTribute(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        OnTribute(_GuildMainData.enTributeFree);
    }

    private void OnGoodTribute(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        OnTribute(_GuildMainData.enTributeGood);
    }

    private void OnExtraTribute(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        OnTribute(_GuildMainData.enTributeExtra);
    }

    private bool CheckTributePrice(DATA_GUILD_TRIBUTE._enTributeEnum TributeEnum)
    {
        DATA_GUILD_TRIBUTE GuildTributeData = CDATA_GUILD_TRIBUTE.Get(TributeEnum);
        if (GuildTributeData == null)
            return false;

        if (GuildTributeData.enPriceType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_GOLD)
        {
            if (UserInfo.Instance.Gold < (ulong)GuildTributeData.iPriceValue)
            {
                // 6873	재화가 부족하여 봉헌할 수 없습니다.
                SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6873));
                return false;
            }
        }
        else if (GuildTributeData.enPriceType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_DIA)
        {
            if (UserInfo.Instance.iDiaCount < (ulong)GuildTributeData.iPriceValue)
            {
                // 6873	재화가 부족하여 봉헌할 수 없습니다.
                SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6873));
                return false;
            }
        }
        else
        {
#if DEBUG_LOG
            Debug.Log("다른 재화타입이 들어왔다!!");
#endif
            return false;
        }

        return true;
    }

    private void OnTribute(DATA_GUILD_TRIBUTE._enTributeEnum TributeEnum)
    {
        if (UserInfo.Instance.GuildAttendanceTime.Date >= TimeManager.Instance.GetServerTime().Date)
        {
            // 6871	이미 봉헌을 완료하였습니다.\n오늘은 더 이상 봉헌할 수 없습니다.
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6871));
            return;
        }

        if (CheckTributePrice(TributeEnum) == false)
            return;

        _stGuildAttendanceReq stGuildAttendanceReq = new _stGuildAttendanceReq();
        stGuildAttendanceReq.kGuildTributeKind = TributeEnum;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildAttendance, stGuildAttendanceReq, typeof(_stGuildAttendanceAck));
    }

    private void OnLeftClick(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        DATA_GUILD_MAIN._enGuildLv DisplayGuildLv = _DisplayGuildLv;
        _DisplayGuildLv--;
        if(_DisplayGuildLv < DATA_GUILD_MAIN._enGuildLv.Glevel_01)
        {
            _DisplayGuildLv = DATA_GUILD_MAIN._enGuildLv.Glevel_01;
        }

        if (DisplayGuildLv == _DisplayGuildLv)
            return;

        SetGoddnessData(_DisplayGuildLv);
    }

    private void OnRightClick(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        DATA_GUILD_MAIN._enGuildLv DisplayGuildLv = _DisplayGuildLv;
        _DisplayGuildLv++;
        if (_DisplayGuildLv > DATA_GUILD_MAIN._enGuildLv.Glevel_10)
        {
            _DisplayGuildLv = DATA_GUILD_MAIN._enGuildLv.Glevel_10;
        }

        if (DisplayGuildLv == _DisplayGuildLv)
            return;

        SetGoddnessData(_DisplayGuildLv);
    }

    private void OnHelpTooltip(GameObject go, bool state)
    {
        if (state == true)
        {
            if (_SimpleHelpTip == null)
            {
                _SimpleHelpTip = UIResourceMgr.CreatePrefab<SimpleHelpTip>(BUNDLELIST.PREFABS_UI_MAINMENU, _HelpButton.transform, "SimpleHelpTip");
                _SimpleHelpTip.Init(17);
            }

            _SimpleHelpTip.OpenUI();
        }
        else
        {
            _SimpleHelpTip.CloseUI();
        }
    }
}
