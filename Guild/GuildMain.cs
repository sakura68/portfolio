using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGL_DATA_READER;

public enum enGuildMain_ButtonType
{
    None = -1,
    Join,
    Create,
}

public class GuildMain : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject m_CloseButton;

    [SerializeField] private UILabel m_MainTitleLabel;
    [SerializeField] private MenuButton m_JoinGuildButton;
    [SerializeField] private MenuButton m_CreateGuildButton;

    [SerializeField] private GameObject _HelpButton;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildList m_GuildList;
    public GuildList guildList { get { return m_GuildList; } }

    private GuildCreate m_GuildCreate;

    private SimpleHelpTip _SimpleHelpTip = null;

    private enGuildMain_ButtonType m_GuildMainButtonType = enGuildMain_ButtonType.None;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    public override void Init()
    {
        if (m_GuildCreate == null)
        {
            m_GuildCreate = UIResourceMgr.CreatePrefab<GuildCreate>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildCreate");
            m_GuildCreate.Init();
        }

        if(m_GuildList == null)
        {
            m_GuildList = UIResourceMgr.CreatePrefab<GuildList>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildList");
            m_GuildList.Init();
        }

        OnJoinGuild(null);

        m_MainTitleLabel.text = StringTableManager.GetData(3494);

        m_JoinGuildButton.SetLabel(StringTableManager.GetData(6227));
        m_CreateGuildButton.SetLabel(StringTableManager.GetData(6260));
    }

    public override void Clear()
    {
    }

    protected override void Awake()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
        

        if (m_JoinGuildButton != null) UIEventListener.Get(m_JoinGuildButton.gameObject).onClick = OnJoinGuild;
        if (m_CreateGuildButton != null) UIEventListener.Get(m_CreateGuildButton.gameObject).onClick = OnCreateGuild;

        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress = OnHelpTooltip;
    }

    protected override void OnEnable()
    {
#if GMTOOLSHOP
        UIControlManager.instance.SetChangeWealth(WEB_SHOP_CATEGORY._enWebUI_Category._enWebUI_Category_Max);
#else
        UIControlManager.instance.SetChangeWealth(DGL_DATA_READER.DATA_SHOP_NEW_CATEGORY._enCategory._enCategory_Max);
#endif
    }

    protected override void Start()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void Update()
    {
    }
    
    protected override void OnDestroy()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;

        if (m_JoinGuildButton != null) UIEventListener.Get(m_JoinGuildButton.gameObject).onClick -= OnJoinGuild;
        if (m_CreateGuildButton != null) UIEventListener.Get(m_CreateGuildButton.gameObject).onClick -= OnCreateGuild;

        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress -= OnHelpTooltip;
    }

    public override void OpenUI()
    {
        base.OpenUI();
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    private void SetMenu(enGuildMain_ButtonType type)
    {
        if (m_GuildMainButtonType == type)
            return;

        m_GuildMainButtonType = type;

        if (type == enGuildMain_ButtonType.Join)
        {
            m_GuildList.gameObject.SetActive(true);
            m_GuildCreate.gameObject.SetActive(false);

            m_JoinGuildButton.state = ButtonState.On;
            m_CreateGuildButton.state = ButtonState.Off;
        }
        else if (type == enGuildMain_ButtonType.Create)
        {
            m_GuildList.gameObject.SetActive(false);
            m_GuildCreate.gameObject.SetActive(true);

            m_JoinGuildButton.state = ButtonState.Off;
            m_CreateGuildButton.state = ButtonState.On;
        }
    }

    public void SetDuplicateCheck(_stGuildNameCheckAck stAck)
    {
        if(m_GuildCreate != null)
        {
            m_GuildCreate.SetDuplicateCheck(stAck);
        }
    }
    
    /// <summary>
    /// 길드가입 요청 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildJoinRequest(_stGuildJoinRequestAck stAck)
    {
        if(m_GuildList.SelectGuildInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            string str = string.Format(StringTableManager.GetData(6235), m_GuildList.SelectGuildInfo.kGuildName);
            SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.Ok, StringTableManager.GetData(4300), str, GuildJoinRequest);
        }
        else if(m_GuildList.SelectGuildInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            string str = string.Format(StringTableManager.GetData(6241), m_GuildList.SelectGuildInfo.kGuildName);
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), str);
            m_GuildList.GuildJoinRequest(stAck);
        }
    }

    public void GuildJoinRequest(enSystemMessageFlag state)
    {
        m_GuildList.CloseGuildInfomation();

        UIControlManager.instance.RemoveWindow(enUIType.GUILDMAIN);

        _stGuildRecommendListReq req = new _stGuildRecommendListReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildRecommendList, req, typeof(_stGuildRecommendListAck));
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnJoinGuild(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        SetMenu(enGuildMain_ButtonType.Join);
    }

    private void OnCreateGuild(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        SetMenu(enGuildMain_ButtonType.Create);
    }

    private void OnHelpTooltip(GameObject go, bool state)
    {
        if (state == true)
        {
            if (_SimpleHelpTip == null)
            {
                _SimpleHelpTip = UIResourceMgr.CreatePrefab<SimpleHelpTip>(BUNDLELIST.PREFABS_UI_MAINMENU, _HelpButton.transform, "SimpleHelpTip");
                _SimpleHelpTip.Init(15);
            }

            _SimpleHelpTip.OpenUI();
        }
        else
        {
            _SimpleHelpTip.CloseUI();
        }
    }
}
