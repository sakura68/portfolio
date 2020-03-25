using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildCreate : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_GuildNameTitleLabel;
    [SerializeField] private UILabel m_GuildEmblemTitleLabel;
    [SerializeField] private UILabel m_JoinMethodTitleLabel;
    [SerializeField] private UILabel m_GuildDescTitleLabel;
    [SerializeField] private UILabel m_DuplicateCheckButtonLabel;
    [SerializeField] private UILabel m_ChooseEmblemButtonLabel;
    [SerializeField] private UILabel m_FreeJoinLabel;
    [SerializeField] private UILabel m_ApprovalJoinLabel;
    [SerializeField] private UILabel m_GuildCreateMoneyLabel;
    [SerializeField] private UILabel m_GuildRequiredLevelLabel;
    [SerializeField] private UILabel m_CreateGuildButtonLabel;

    [SerializeField] private UIInput m_GuildNameInput;
    [SerializeField] private UIInput m_GuildDescInput;

    [SerializeField] private CustomButtonUI m_FreeJoinButton;
    [SerializeField] private CustomButtonUI m_ApprovalJoinButton;

    [SerializeField] private UI2DSprite m_GuildEmblemSprite;

    [SerializeField] private GameObject m_DuplicateCheckButton;
    [SerializeField] private GameObject m_ChooseEmblemButton;
    [SerializeField] private GameObject m_CreateGuildButton;

    [SerializeField] private UILabel m_BottomLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildEmblem m_GuildEmblemPopupWindow = null;
    private _enGuildJoinMethod m_JoinType = _enGuildJoinMethod.eGuildJoinMethod_Free;

    private string m_strGuildNameDefault;       // 사용할 길드 이름을 입력해주세요
    private string m_strGuildDescDefault;       // 길드에 대한 간략한 소개글을 입력해주세요. (최대 30자)
    private string m_ServerCheckGuildName = string.Empty;   // 서버에서 체크한 길드 이름.

    private int m_iGuildCreateCountGold = 0;
    private int m_iGuildCreateStringCount = 0;
    private int m_iGuildCreateLevel = 0;
    private int m_iGuildIntroStringCount = 0;

    private byte m_kEmblemNumber = 0;

    private bool m_bDuplicateCheck = false;
    private bool m_bEmblemCheck = false;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Awake()
    {
        if (m_FreeJoinButton != null) UIEventListener.Get(m_FreeJoinButton.gameObject).onClick = OnFreeJoin;
        if (m_ApprovalJoinButton != null) UIEventListener.Get(m_ApprovalJoinButton.gameObject).onClick = OnApprovalJoin;
        if (m_DuplicateCheckButton != null) UIEventListener.Get(m_DuplicateCheckButton).onClick = OnDuplicateCheck;
        if (m_ChooseEmblemButton != null) UIEventListener.Get(m_ChooseEmblemButton).onClick = OnChooseEmblem;
        if (m_CreateGuildButton != null) UIEventListener.Get(m_CreateGuildButton).onClick = OnCreateGuild;
    }

    private void OnDestroy()
    {
        if (m_FreeJoinButton != null) UIEventListener.Get(m_FreeJoinButton.gameObject).onClick -= OnFreeJoin;
        if (m_ApprovalJoinButton != null) UIEventListener.Get(m_ApprovalJoinButton.gameObject).onClick -= OnApprovalJoin;
        if (m_DuplicateCheckButton != null) UIEventListener.Get(m_DuplicateCheckButton).onClick -= OnDuplicateCheck;
        if (m_ChooseEmblemButton != null) UIEventListener.Get(m_ChooseEmblemButton).onClick -= OnChooseEmblem;
        if (m_CreateGuildButton != null) UIEventListener.Get(m_CreateGuildButton).onClick -= OnCreateGuild;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init()
    {
        m_GuildEmblemPopupWindow = UIResourceMgr.CreatePrefab<GuildEmblem>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildEmblem");
        m_GuildEmblemPopupWindow.Init(this);
        m_GuildEmblemPopupWindow.CloseUI();

        SetJoinButton(_enGuildJoinMethod.eGuildJoinMethod_Free);

        m_iGuildCreateCountGold = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_Create_Count_Gold).Value;
        m_iGuildCreateStringCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_CreateString_Count).Value;
        m_iGuildCreateLevel = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_CreateLevel).Value;
        m_iGuildIntroStringCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_IntroString_Count).Value;

        m_GuildNameTitleLabel.text = StringTableManager.GetData(6261);
        m_GuildEmblemTitleLabel.text = StringTableManager.GetData(6266);
        m_JoinMethodTitleLabel.text = StringTableManager.GetData(6268);
        m_GuildDescTitleLabel.text = StringTableManager.GetData(6249);
        m_DuplicateCheckButtonLabel.text = StringTableManager.GetData(6263);
        m_ChooseEmblemButtonLabel.text = StringTableManager.GetData(6266);
        m_FreeJoinLabel.text = StringTableManager.GetData(6233);
        m_ApprovalJoinLabel.text = StringTableManager.GetData(6239);

        m_GuildCreateMoneyLabel.text = m_iGuildCreateCountGold.ToString();
        m_GuildRequiredLevelLabel.text = string.Format(StringTableManager.GetData(6584), m_iGuildCreateLevel.ToString()); // Lv {0} 이상.

        m_CreateGuildButtonLabel.text = StringTableManager.GetData(6260);

        m_strGuildNameDefault = StringTableManager.GetData(6262);
        m_GuildNameInput.defaultText = m_strGuildNameDefault;
        m_GuildNameInput.characterLimit = m_iGuildCreateStringCount;

        m_strGuildDescDefault = StringTableManager.GetData(6269);
        m_GuildDescInput.defaultText = m_strGuildDescDefault;
        m_GuildDescInput.characterLimit = m_iGuildIntroStringCount;     // 길드소개 글자 제한 수.

        // 6585    길드 마크와 가입 형태는 추후 변경 가능합니다.
        m_BottomLabel.text = StringTableManager.GetData(6585);
    }

    private void SetJoinButton(_enGuildJoinMethod join)
    {
        m_JoinType = join;

        if (join == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            m_FreeJoinButton.state = ButtonState.On;
            m_ApprovalJoinButton.state = ButtonState.Off;
        }
        else if (join == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            m_FreeJoinButton.state = ButtonState.Off;
            m_ApprovalJoinButton.state = ButtonState.On;
        }
    }

    public void SetEmblem(EmblemElement SelectEmblem)
    {
        m_bEmblemCheck = true;
        m_GuildEmblemSprite.sprite2D = SelectEmblem.GetEmblemSprite();
        m_kEmblemNumber = byte.Parse(SelectEmblem.name);
    }

    public void SetDuplicateCheck(_stGuildNameCheckAck stAck)
    {
        m_ServerCheckGuildName = stAck.kCheckName;
        m_bDuplicateCheck = stAck.kAbleName;
    }

    private bool GuildNameCheck(string strName, out string error)
    {
        bool bCompleted = true;
        error = string.Empty;

        int iGuildNameLength = strName.Length;

        if (iGuildNameLength < 1) // 1글자 미만.
        {
            // 사용할 길드 이름을 입력해주세요
            error = StringTableManager.GetData(6262);
            bCompleted = false;
        }
        else if (iGuildNameLength > m_iGuildCreateStringCount)  // 15글자 넘어가면.
        {
            // 길드 이름이 최대 글자수를 초과 하었습니다.
            error = StringTableManager.GetData(6514);
            bCompleted = false;
        }

        if(UtilFunc.IsEmptyCharacters(strName) == true)
        {
            // 특수문자, 금칙어는 사용할 수 없습니다.
            error = StringTableManager.GetData(6231);
            bCompleted = false;
        }

        if (UtilFunc.IsSpecialAndFiterCharacters(strName) == true) // 특수기호와 금칙어 사용 여부.
        {
            // 특수문자, 금칙어는 사용할 수 없습니다.
            error = StringTableManager.GetData(6231);
            bCompleted = false;
        }

        return bCompleted;
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnDuplicateCheck(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        string strError = string.Empty;
        if(GuildNameCheck(m_GuildNameInput.value, out strError) == false)
        {
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), strError);
            return;
        }

        _stGuildNameCheckReq stGuildNameCheckReq = new _stGuildNameCheckReq();
        stGuildNameCheckReq.kCheckName = m_GuildNameInput.value;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildNameCheck, stGuildNameCheckReq, typeof(_stGuildNameCheckAck));
    }

    private void OnChooseEmblem(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);


        m_GuildEmblemPopupWindow.OpenUI();
    }

    /// <summary>
    /// 길드 창설 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnCreateGuild(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        string str = string.Empty;

        if(string.IsNullOrEmpty(m_GuildNameInput.value) == true)
        {
            //길드 이름을 입력해주세요.(스트링 ID: 6271)
            str = StringTableManager.GetData(6271);
        }
        else if (m_bDuplicateCheck == false)
        {
            // 길드 이름을 중복 확인 해주세요.
            str = StringTableManager.GetData(6583);
        }
        else if (m_bEmblemCheck == false)
        {
            // 길드 마크를 선택해 주세요 (스트링 ID : 6272)
            str = StringTableManager.GetData(6267);
        }
        else if (string.IsNullOrEmpty(m_GuildDescInput.value) == true)
        {
            //소개글을 입력해주세요. (스트링 ID: 6273)
            str = StringTableManager.GetData(6273);
        }
        else if (UserInfo.Instance.Level < m_iGuildCreateLevel)
        {
            // 플레이어의 레벨이 10이상이 되어야 길드를 창설 할 수 있습니다. (스트링 ID: 6274)
            str = StringTableManager.GetData(6274);
        }
        else if (UserInfo.Instance.Gold < (ulong)m_iGuildCreateCountGold)
        {
            //길드 창설 비용이 부족합니다. (스트링 ID: 6275)
            str = StringTableManager.GetData(6275);
        }
        else if (string.Equals(m_ServerCheckGuildName, m_GuildNameInput.value) == false)      // 체크버튼 누른 후 클라에서 길드이름 다시 바꿨을때.
        {
            // 길드 이름을 중복 확인 해주세요.
            str = StringTableManager.GetData(6583);
            m_bDuplicateCheck = false;
        }

        if (string.IsNullOrEmpty(str) == false)
        {
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), str);
            return;
        }

        _stGuildCreateReq stGuildCreateReq = new _stGuildCreateReq();
        CGuildDetail GuildDetail = new CGuildDetail();
        GuildDetail.kGuildName = m_GuildNameInput.value.Trim();
        GuildDetail.kGuildMark = m_kEmblemNumber;
        GuildDetail.kJoinMethod = m_JoinType;
        GuildDetail.kGuildDesc = m_GuildDescInput.value.TrimEnd();

        _vGuildMembers vGuildMembers = new _vGuildMembers();
        GuildDetail.vMembers = vGuildMembers;
        stGuildCreateReq.kGuildDetailInfo = GuildDetail;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildCreate, stGuildCreateReq, typeof(_stGuildCreateAck));
    }

    private void OnFreeJoin(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SetJoinButton(_enGuildJoinMethod.eGuildJoinMethod_Free);
    }

    private void OnApprovalJoin(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SetJoinButton(_enGuildJoinMethod.eGuildJoinMethod_Approval);
    }
}