using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;
using System;

public class OtherUserInfomation : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UI2DSprite m_MainCharImg;  
    [SerializeField] private UISprite m_ExpImg;
    [SerializeField] private UILabel m_UserNameLbl;
    [SerializeField] private UILabel m_LevLbl;
    [SerializeField] private UILabel m_ExpPercentLbl;

    [SerializeField] private UISprite    m_SprVipGrade; // vip 등급 이미지
    [SerializeField] private UILabel    m_LabelVipGrade; // vip등급 라벨

    [SerializeField] private GameObject m_CloseButton;

    [SerializeField] private List<GameObject> m_CreatureColliderList = new List<GameObject>();

    [SerializeField] private MenuButton m_AddFriendButton;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<MainMenuCreatureContainer> m_pCreatureList = new List<MainMenuCreatureContainer>();        // 3d 모델.

    private _stGuildUserTeamInfoAck m_GuildUserTeamInfoAck = null;

    private Vector3 _origin3DCameraPos = Vector3.zero;

    private int m_iFriendsSendMax = 0;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
        if (m_AddFriendButton != null) UIEventListener.Get(m_AddFriendButton.gameObject).onClick = AddFriendEvent;

        int iCount = m_CreatureColliderList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GameObject obj = m_CreatureColliderList[i];
            if (obj == null)
                continue;

            UIEventListener.Get(obj).onClick = OnCreatureClick;
        }
    }

    protected override void Start()
    {
    }

    public override void Init()
    {
        m_iFriendsSendMax = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Friends_Send_Max).Value;

        m_AddFriendButton.SetLabel(StringTableManager.GetData(3464));
        m_AddFriendButton.state = ButtonState.On;

        _origin3DCameraPos = UIControlManager.instance.Camera3D.transform.localPosition;
    }

    public override void Clear()
    {
        int iCount = m_pCreatureList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            MainMenuCreatureContainer model = m_pCreatureList[i];
            if (model == null)
                continue;

            DestroyImmediate(model.baseGameObject);
        }

        m_pCreatureList.Clear();
    }

    protected override void OnDestroy()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;
        if (m_AddFriendButton != null) UIEventListener.Get(m_AddFriendButton.gameObject).onClick -= AddFriendEvent;

        int iCount = m_CreatureColliderList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GameObject obj = m_CreatureColliderList[i];
            if (obj == null)
                continue;

            UIEventListener.Get(obj).onClick -= OnCreatureClick;
        }

        Clear();
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void Update()
    {
    }

    public override bool OnClickBack()
    {
        UIControlManager.instance.UpperMenuActive(true);
        UIControlManager.instance.SetOtherUser3DObject(false);
        UIControlManager.instance.ActiveWindow();

        UIControlManager.instance.Camera3D.transform.localPosition = _origin3DCameraPos;

        return true;
    }

    public override void OpenUI()
    {
        base.OpenUI();

        UIControlManager.instance.UpperMenuActive(false);
        UIControlManager.instance.SetOtherUser3DObject(true);
        UIControlManager.instance.InActiveWindow(new enUIType[] { WindowType });

        UIControlManager.instance.Camera3D.transform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);
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
    public void CreatreOtherUserTeamInfo(_stGuildUserTeamInfoAck stAck)
    {
        if (m_GuildUserTeamInfoAck != null && m_GuildUserTeamInfoAck.kDestCharNo == stAck.kDestCharNo)
            return;

        m_GuildUserTeamInfoAck = stAck;

        m_AddFriendButton.state = ButtonState.On;

        // 오더순으로 정렬.
        stAck.kCreatureIDTeamInfo.vMembers.Sort((a, b) => a.kOrder.CompareTo(b.kOrder));

        SetOtherUserInfo(m_GuildUserTeamInfoAck.kCharSimpleInfo);
        CreateModel(m_GuildUserTeamInfoAck.kCreatureIDTeamInfo.vMembers);
    }

    /// <summary>
    /// 유저정보 셋팅.
    /// </summary>
    private void SetOtherUserInfo(_stCharSimpleInfo kCharSimpleInfo)
    {
        m_UserNameLbl.text = kCharSimpleInfo.kDestCharName;
        m_LevLbl.text = string.Format("{0} {1}", StringTableManager.GetData(12), kCharSimpleInfo.kDestCharLevel);

        float fResult = UtilFunc.GetUserExpPercent(kCharSimpleInfo.kDestCharExp, kCharSimpleInfo.kDestCharExp);
        m_ExpImg.fillAmount = fResult;
        m_ExpPercentLbl.text = string.Format("{0:F2} {1}", (fResult * 100), "%");

        // 대표 크리쳐 셋팅.
        string cretureName = CDATA_CREATURE_NEWVER.Get(kCharSimpleInfo.kDestCharLearderCreatureID).m_szIcon;
        m_MainCharImg.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_CREATUREHEAD, cretureName);

#if VIP_SYSTEM
        DATA_VIP datavip = CDATA_VIP.Get(kCharSimpleInfo.kDestCharVIPLevel);
        if (datavip != null)
        {
            m_SprVipGrade.spriteName = datavip.szGradeImg;
            m_LabelVipGrade.text = string.Format(StringTableManager.GetData(4984), (int)kCharSimpleInfo.kDestCharVIPLevel);
        }
#endif
    }

    /// <summary>
    /// 모델 생성.
    /// </summary>
    private void CreateModel(_vCreatureIDTeamMembers Members)
    {
        Clear();

        for (int i = 0; i < Members.Count; ++i)
        {
            _stCreatureIDTeamMember CreatureIDTeamMember = Members[i];
            if (CreatureIDTeamMember == null)
                continue;

            MainMenuCreatureContainer model = CreateModel(CreatureIDTeamMember, UIControlManager.instance.OtherCreatureList[i].transform);
            if (model != null)
                m_pCreatureList.Add(model);
        }
    }

    /// <summary>
    /// 모델 생성
    /// </summary>
    private MainMenuCreatureContainer CreateModel(_stCreatureIDTeamMember CreatureIDTeamMember, Transform trParent)
    {
        MainMenuCreatureContainer pCreature = new MainMenuCreatureContainer();

        DATA_CREATURE_NEWVER creatureTable = CDATA_CREATURE_NEWVER.Get(CreatureIDTeamMember.kCreatureID);

        string objName = "CREATURE_" + creatureTable.m_szResourceName;
        if (pCreature.LoadCreatureModel(creatureTable.m_szResourceName, objName, trParent) == false)
            return null;

        pCreature.SetCreatureTableData(creatureTable);

        pCreature.baseGameObject.transform.localPosition = Vector3.zero;
        pCreature.baseGameObject.transform.Rotate(Vector3.up, 180);

        pCreature.SetActiveCharPanel(true);
        pCreature.SetEnableCharPanel(true);

        pCreature.SetCharPanelShadowTexture("Texture/Rim/shadow");
        pCreature.SetCharPanelShader("Mobile/Transparent/Alpha Blended");

        pCreature.SetCharacterShader();

        pCreature.CreateTranscendenceEffect(enRenderLayer.EFFECT, creatureTable.m_enGrade, CreatureIDTeamMember.kCreatureAwake);

        return pCreature;
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 크리쳐 클릭했을때 애니메이션 이벤트.
    /// </summary>
    /// <param name="go"></param>
    private void OnCreatureClick(GameObject go)
    {
        for(int i = 0; i < m_pCreatureList.Count; ++i)
        {
            if(m_CreatureColliderList[i] == go)
            {
                m_pCreatureList[i].SetUIModelAnimationWithTime();
                break;
            }
        }
    }

    /// <summary>
    /// 친구신청 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void AddFriendEvent(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.UI_GETFRIEND);

        //if (m_AddFriendButton.state == ButtonState.Off)
        //    return;

        if (m_GuildUserTeamInfoAck.kDestCharNo == UserInfo.Instance.CharNo)
        {
            // 자기 자신에게 요청.
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), StringTableManager.GetData(3332));
            return;
        }


        // FriendInfo
        {
            _vFriend Friends = UserInfo.Instance.FriendInfo.vFriends;
            int iFriendsCount = Friends.Count;

            for (int i = 0; i < iFriendsCount; ++i)
            {
                if (Friends[i].kFriendCharNo == m_GuildUserTeamInfoAck.kDestCharNo)
                {
                    // 이미 친구.
                    SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), StringTableManager.GetData(3334));
                    return;
                }
            }
        }


        // AddFriendInfo
        {
            _vFriend AddFriends = UserInfo.Instance.AddFriendInfo.vSendFriends;
            int iAddFriendsCount = AddFriends.Count;

            if (iAddFriendsCount >= m_iFriendsSendMax)
            {
                // 친구 최대치.
                SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), StringTableManager.GetData(3341));
                return;
            }

            for (int i = 0; i < iAddFriendsCount; ++i)
            {
                if (AddFriends[i].kFriendCharNo == m_GuildUserTeamInfoAck.kDestCharNo)
                {
                    //이미 친구 신청한 상대.
                    SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), StringTableManager.GetData(3335));
                    return;
                }
            }
        }


        // RecvFriendInfo
        {
            _vFriend RecvFriends = UserInfo.Instance.RecvFriendInfo.vRecvFriends;
            int iRecvFriendsCount = RecvFriends.Count;

            for (int i = 0; i < iRecvFriendsCount; ++i)
            {
                if (RecvFriends[i].kCharNo == m_GuildUserTeamInfoAck.kDestCharNo)
                {
                    //상대방이 친구 신청한 상대.
                    SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), StringTableManager.GetData(3336));
                    return;
                }
            }
        }

        string str = string.Format(StringTableManager.GetData(3944), m_GuildUserTeamInfoAck.kCharSimpleInfo.kDestCharName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(3954), str, OnAddFriendOK);
    }

    private void OnAddFriendOK(enSystemMessageFlag enFlag)
    {
        if (enFlag == enSystemMessageFlag.NO)
            return;

        //m_AddFriendButton.state = ButtonState.Off;

        _stFriendRequestSendReq stFriendRequestSendReq = new _stFriendRequestSendReq();
        stFriendRequestSendReq.kRequestCharNo = m_GuildUserTeamInfoAck.kDestCharNo;

        CNetManager.Instance.SendPacket(CNetManager.Instance.FriendProxy.FriendRequestSend, stFriendRequestSendReq, typeof(_stFriendRequestSendAck));
    }
}