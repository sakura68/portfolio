using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildRaidRankingItem : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _RankLabel;

    [SerializeField] private UI2DSprite _EmblemSprite;

    [SerializeField] private UILabel _GuildLevelLabel;
    [SerializeField] private UILabel _GuildNameLabel;

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
    public void Init(CGuildRaidRankInfo rankInfo)
    {
        bool isMyGuild = string.Equals(UserInfo.Instance.GuildName, rankInfo.kGuildName);

        if (isMyGuild && rankInfo.kGuildRaidRank > 100)
            _RankLabel.text = "-";
        else
            _RankLabel.text = rankInfo.kGuildRaidRank.ToString();

        _GuildNameLabel.text = rankInfo.kGuildName;

        DATA_GUILD_MAIN guildMainTable = CDATA_GUILD_MAIN.Get(rankInfo.kGuildLevel);
        _GuildLevelLabel.text = string.Format("{0}{1}", StringTableManager.GetData(12), guildMainTable.iGuildLv);

        _guildRaidScore.text = string.Format(StringTableManager.GetData(3411), rankInfo.kGuildRaidScore);

        _MeSprite.gameObject.SetActive(isMyGuild);

        _EmblemSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, string.Format("GuildEmblem{0}", rankInfo.kGuildMark.ToString("D2")));
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
