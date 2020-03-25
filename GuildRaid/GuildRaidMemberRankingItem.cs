using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildRaidMemberRankingItem : MonoBehaviour
{
	//===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _RankLabel;

    [SerializeField] private UI2DSprite _LeaderCreatureSprite;

    [SerializeField] private UISprite _VipRankSprite;
    [SerializeField] private UILabel _VipRankLabel;
    
    [SerializeField] private UILabel _LevelLabel;

    [SerializeField] private UILabel _NameLabel;

    [SerializeField] private UILabel _EnterCountLabel;      // 입장횟수

    [SerializeField] private UILabel _guildRaidScore;

    [SerializeField] private UISprite _MeSprite;        // 내 케릭터다.

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================

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
    public void Init(CGuildRaidUserRankInfo info)
    {
        if (CDATA_CREATURE_NEWVER.GetCount() < 1)
            CDATA_CREATURE_NEWVER.Load();

        if (CDATA_VIP.GetCount() < 1)
            CDATA_VIP.Load();

        _RankLabel.text = string.Format(StringTableManager.GetData(3412), info.kUserRank);

        DATA_CREATURE_NEWVER CreatureTable = CDATA_CREATURE_NEWVER.Get(info.kCreatureID);
        _LeaderCreatureSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_CREATUREHEAD, CreatureTable.m_szIcon);

        _VipRankSprite.spriteName = CDATA_VIP.Get(info.kCharVIPLevel).szGradeImg;
        _VipRankLabel.text = string.Format(StringTableManager.GetData(4984), (int)info.kCharVIPLevel);

        _LevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), (int)info.kCharLevel);

        _NameLabel.text = info.kCharName;

        _EnterCountLabel.text = info.kGuildRaidPlayCount.ToString();

        _guildRaidScore.text = info.kGuildRaidScore.ToString();
        
        _MeSprite.gameObject.SetActive(string.Equals(info.kCharName, UserInfo.Instance.NickName));
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
