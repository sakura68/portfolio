using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildDelegation : UIWindowPopup
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject m_CloseButton;

    [SerializeField] private UILabel m_TitleLabel;

    [SerializeField] private UIScrollView m_GuildMemberScrollView;
    [SerializeField] private UIGrid m_GuildMemberGrid;

    [SerializeField] private UILabel m_BottomLabel;

    [SerializeField] private UILabel _DelegationMemberEmptyLabel;       // 길드장 위임이나 부길드장 임명할 길드원이 없을때

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private CGuildDetail m_GuildDetailInfo = null;

    private List<GuildInformationItem> m_DelegationMemberList = new List<GuildInformationItem>();

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;
    }

    public override void Init()
    {
        m_BottomLabel.text = StringTableManager.GetData(6596);      // 6597 길드장 위임을 체크한 길드원이보입니다.

        _DelegationMemberEmptyLabel.text = StringTableManager.GetData(8634);    // 8634    조건에 맞는 길드원이 없습니다.
        _DelegationMemberEmptyLabel.gameObject.SetActive(false);
    }

    public override void Clear()
    {
        int iCount = m_DelegationMemberList.Count;
        for(int i = 0; i < iCount; ++i)
        {
            GuildInformationItem infoItem = m_DelegationMemberList[i];
            if (infoItem == null)
                continue;

            DestroyImmediate(infoItem.gameObject);
        }

        m_DelegationMemberList.Clear();
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void SetDelegationMember(enGuildInfo_WindowType type, CGuildDetail kGuildDetailInfo)
    {
        Clear();

        m_GuildDetailInfo = kGuildDetailInfo;
        _vGuildMembers vGuildMembers = m_GuildDetailInfo.vMembers;

        if(type == enGuildInfo_WindowType.GuildCaptain_Delegation)      // 길드장 위임이면.
        {
            m_TitleLabel.text = StringTableManager.GetData(6294);

            for (int i = 0; i < vGuildMembers.Count; ++i)
            {
                CGuildMember member = vGuildMembers[i];
                if (member == null)
                    continue;

                if (member.kMemberState == _enGuildMemberState.eGuildMemberState_AbleSubCaptain || member.kMemberState == _enGuildMemberState.eGuildMemberState_AbleMember)
                {
                    GuildInformationItem memberItem = UIResourceMgr.CreatePrefab<GuildInformationItem>(BUNDLELIST.PREFABS_UI_GUILD, m_GuildMemberGrid.transform, "GuildInformationItem");
                    memberItem.Init(type, m_GuildDetailInfo.kGuildKey, member);
                    m_DelegationMemberList.Add(memberItem);
                }
            }
        }
        else if(type == enGuildInfo_WindowType.GuildSubCaptain_Appointment)     // 부길드장 임명이면.
        {
            m_TitleLabel.text = StringTableManager.GetData(6298);

            for (int i = 0; i < vGuildMembers.Count; ++i)
            {
                CGuildMember member = vGuildMembers[i];
                if (member == null)
                    continue;

                if (member.kMemberState == _enGuildMemberState.eGuildMemberState_AbleMember)
                {
                    GuildInformationItem memberItem = UIResourceMgr.CreatePrefab<GuildInformationItem>(BUNDLELIST.PREFABS_UI_GUILD, m_GuildMemberGrid.transform, "GuildInformationItem");
                    memberItem.Init(type, m_GuildDetailInfo.kGuildKey, member);
                    m_DelegationMemberList.Add(memberItem);
                }
            }
        }

        if (m_DelegationMemberList.Count < 1)       // 조건에 해당하는 길드원이 없으면.
        {
            _DelegationMemberEmptyLabel.gameObject.SetActive(true);
        }
        else
        {
            _DelegationMemberEmptyLabel.gameObject.SetActive(false);
        }

        m_GuildMemberGrid.Reposition();
        m_GuildMemberScrollView.ResetPosition();
    }
}