using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class DispatchTeamItemFriend : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private CreatureIcon m_CreatureIcon;

    [SerializeField] private GameObject m_CoolTimeObj;
    [SerializeField] private UILabel m_CoolTimeLabel;

    //[SerializeField] private GameObject m_DispatchingObj;

    [SerializeField] private UISprite m_VipSprite;
    [SerializeField] private UILabel m_VipLabel;

    [SerializeField] private UILabel m_UserLevelLabel;
    [SerializeField] private UILabel m_UserNameLabel;

    [SerializeField] private UILabel m_LastLoginTimeLabel;

    [SerializeField] private UISprite m_SelectSprite;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private CFriend m_FriendInfo = null;
    public CFriend FriendInfo { get { return m_FriendInfo; } }

    public bool bDispatching { get; private set; }

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
    public void Init(CFriend friendInfo, DATA_CREATURE_NEWVER pCreatureData)
    {
        m_FriendInfo = friendInfo;

        _vCharNo UsedFriendKeyList = UserInfo.Instance.DispatchInfoAck.vUsedFriendKey;

        bDispatching = false;
        m_CoolTimeObj.SetActive(false);

        for (int i = 0; i < UsedFriendKeyList.Count; ++i)
        {
            if (friendInfo.kFriendCharNo == UsedFriendKeyList[i])
            {
                // 쿨타임 표시.
                bDispatching = true;
                m_CoolTimeObj.SetActive(true);
                break;
            }
        }

        m_CreatureIcon.SetIcon(friendInfo.kCreatureID, enCreatureIcon_Type.Dispatch);
        //m_CreatureIcon.m_OnClickEvent += evt;

        m_CreatureIcon.m_IsNew = false;
        //icon.m_kCreatureKey = kCreatureKey;
        m_CreatureIcon.m_iGrade = pCreatureData.m_iGrade;
        m_CreatureIcon.m_szName = StringTableManager.GetData(pCreatureData.iCreatureName);
        //icon.m_Reinforce = pMy.GetForceCount();

        m_CreatureIcon.m_iCreatureTID = pCreatureData.m_iCreatureTID;
        m_CreatureIcon.m_szIcon = pCreatureData.m_szIcon;
        //icon.m_iLevel = pMy.GetItemLV();
        m_CreatureIcon.m_enCreatureArmy = pCreatureData.m_enCreatureArmy; //병과
        m_CreatureIcon.m_SellCheck = false;
        m_CreatureIcon.m_iSellCost = pCreatureData.m_iSellCost;
        m_CreatureIcon.m_kLock = 0;

        // vip정보 셋팅.
        {
            if (CDATA_VIP.GetCount() < 1)
                CDATA_VIP.Load();

            DATA_VIP vipData = CDATA_VIP.Get(friendInfo.kVIPLevel);
            if (vipData == null)
                return;

            m_VipSprite.spriteName = vipData.szGradeImg;
            m_VipLabel.text = string.Format(StringTableManager.GetData(4984), (int)friendInfo.kVIPLevel);
        }

        // 친구 레벨, 아이디 셋팅.
        {
            m_UserLevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), friendInfo.kCharLevel);
            m_UserNameLabel.text = friendInfo.kCharName;
        }

        // 접속시간 셋팅.
        {
            if (friendInfo.kFriendState == _enFriendState.eFriendState_OnLine)
            {
                m_LastLoginTimeLabel.text = StringTableManager.GetData(3473);
            }
            else if (friendInfo.kFriendState == _enFriendState.eFriendState_OffLine)
            {
                TimeSpan ts = TimeManager.Instance.GetServerTime() - friendInfo.kGameLastLogonTime.GetDateTime();
                int iDay = ts.Days;
                int iHour = ts.Hours;
                int iMin = ts.Minutes;

                if (iDay > 0)
                    m_LastLoginTimeLabel.text = string.Format(StringTableManager.GetData(3470), iDay, StringTableManager.GetData(3472), StringTableManager.GetData(3483));
                else if (iHour > 0)
                    m_LastLoginTimeLabel.text = string.Format(StringTableManager.GetData(3470), iHour, StringTableManager.GetData(3471), StringTableManager.GetData(3483));
                else if (iMin > 0)
                    m_LastLoginTimeLabel.text = string.Format(StringTableManager.GetData(3470), iMin, StringTableManager.GetData(3482), StringTableManager.GetData(3483));
                else
                {
                    // 1분전접속.
                    m_LastLoginTimeLabel.text = string.Format(StringTableManager.GetData(3470), 1, StringTableManager.GetData(3482), StringTableManager.GetData(3483));
                }
            }

            //TimeSpan ts = TimeManager.Instance.GetServerTime() - friendInfo.kGameLastLogonTime.GetDateTime();
            //int iDay = ts.Days;
            //int iHour = ts.Hours;
            //int iMin = ts.Minutes;

            //if (iDay > 0)
            //    m_LastLoginTimeLabel.text = string.Format(StringTableManager.GetData(3470), iDay, StringTableManager.GetData(3472), StringTableManager.GetData(3483));
            //else if (iHour > 0)
            //    m_LastLoginTimeLabel.text = string.Format(StringTableManager.GetData(3470), iHour, StringTableManager.GetData(3471), StringTableManager.GetData(3483));
            //else if (iMin > 0)
            //    m_LastLoginTimeLabel.text = string.Format(StringTableManager.GetData(3470), iMin, StringTableManager.GetData(3482), StringTableManager.GetData(3483));
            //else
            //{
            //    m_LastLoginTimeLabel.text = StringTableManager.GetData(3473);
            //}
        }

        SetActiveSelect(false);
    }

    public void SetActiveSelect(bool bIsActive)
    {
        m_SelectSprite.gameObject.SetActive(bIsActive);
    }
}