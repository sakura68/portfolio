using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildRaidResult : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _titleLabel;

    [SerializeField] private GameObject _easyIcon;
    [SerializeField] private UILabel _easyIconLabel;

    [SerializeField] private GameObject _nomalIcon;
    [SerializeField] private UILabel _nomalIconLabel;

    [SerializeField] private GameObject _hardIcon;
    [SerializeField] private UILabel _hardIconLabel;

    [SerializeField] private UI2DSprite _bossBannerSprite;
    [SerializeField] private UILabel _bossLevel;
    [SerializeField] private UILabel _bossName;
    [SerializeField] private UILabel _currentScore;
    [SerializeField] private UILabel _totalScore;
    [SerializeField] private UILabel _guildRaidTicketLabel;

    [SerializeField] private GameObject _moveMainMenuButton;
    [SerializeField] private UILabel _moveMainMenuButtonLabel;

    [SerializeField] private GameObject _moveGuildRaid;
    [SerializeField] private UILabel _moveGuildRaidLabel;

    [SerializeField] private GameObject _moveGuildRaidReady;
    [SerializeField] private UILabel _moveGuildRaidReadyLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private ulong _score = 0;
    private int _roundToInt = 0;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        UIEventListener.Get(_moveMainMenuButton.gameObject).onClick = OnMoveMainMenu;
        UIEventListener.Get(_moveGuildRaid.gameObject).onClick = OnMoveGuildRaid;
        UIEventListener.Get(_moveGuildRaidReady.gameObject).onClick = OnMoveGuildRaidReady;
    }

    protected override void OnDestroy()
    {
        UIEventListener.Get(_moveMainMenuButton.gameObject).onClick -= OnMoveMainMenu;
        UIEventListener.Get(_moveGuildRaid.gameObject).onClick -= OnMoveGuildRaid;
        UIEventListener.Get(_moveGuildRaidReady.gameObject).onClick -= OnMoveGuildRaidReady;
    }

    protected override void OnEnable() { }

    protected override void OnDisable() { }

    protected override void Start() { }

    public override void Init() { }

    protected override void Update() { }

    public override void Clear() { }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void SetResultData(_stGuildRaidBattleResultAck resultData)
    {
        GuildRaidInfo guildRaidInfo = UserInfo.Instance.guildRaidInfo;

        DATA_GUILDRAID guildRaidTable = CDATA_GUILDRAID.Get(guildRaidInfo.guildRaidType);
        if (guildRaidTable == null)
        {
#if DEBUG_LOG
            Debug.Log(string.Format("<color=red> GUILDRAID Table Error - guildRaidType : {0} </color>", guildRaidInfo.guildRaidType));
#endif
            return;
        }

        _easyIconLabel.text = StringTableManager.GetData(81);     // 81 쉬움
        _nomalIconLabel.text = StringTableManager.GetData(82);     // 82 보통
        _hardIconLabel.text = StringTableManager.GetData(83);     // 83 어려움

        _easyIcon.gameObject.SetActive(false);
        _nomalIcon.gameObject.SetActive(false);
        _hardIcon.gameObject.SetActive(false);

        if (guildRaidTable.SetLevel == (int)enGuildRaidDifficulty.Easy)
        {
            _easyIcon.gameObject.SetActive(true);
        }
        else if (guildRaidTable.SetLevel == (int)enGuildRaidDifficulty.Normal)
        {
            _nomalIcon.gameObject.SetActive(true);
        }
        else if (guildRaidTable.SetLevel == (int)enGuildRaidDifficulty.Hard)
        {
            _hardIcon.gameObject.SetActive(true);
        }

        //_bossLevel.gameObject.SetActive(false);   // 레벨없음

        _bossBannerSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_GUILDRAID, guildRaidTable.RaidBannerImage);
        _titleLabel.text = StringTableManager.GetData(6753);        // 6753	길드 레이드
        _bossName.text = StringTableManager.GetData(guildRaidTable.RaidBossName);
        _currentScore.text = string.Format(StringTableManager.GetData(3411), 0);
        _totalScore.text = string.Format(StringTableManager.GetData(3411), UtilFunc.CurrencyFormat((int)(guildRaidInfo.guildRaidScore + resultData.kAddScore)));
        _moveMainMenuButtonLabel.text = StringTableManager.GetData(133);
        _moveGuildRaidLabel.text = StringTableManager.GetData(6753);        // 6753	길드 레이드
        _moveGuildRaidReadyLabel.text = StringTableManager.GetData(135);
        _guildRaidTicketLabel.text = string.Format(StringTableManager.GetData(4918), resultData.kUpdatePlayCount);

        if (UserInfo.Instance.GuildRaidTicket < 1)
        {
            Vector3 OriginPos = _moveMainMenuButton.transform.localPosition;
            _moveMainMenuButton.transform.localPosition = new Vector3(-150.0f, OriginPos.y, OriginPos.z);

            OriginPos = _moveGuildRaid.transform.localPosition;
            _moveGuildRaid.transform.localPosition = new Vector3(150.0f, OriginPos.y, OriginPos.z);

            _moveGuildRaidReady.gameObject.SetActive(false);
        }

        // ulog -> float -> (ulog or int) 손실발생. 그래서 저장.
        float Round = Mathf.Round(resultData.kAddScore);      // ulong -> float
        _roundToInt = Mathf.RoundToInt(Round);      // float -> int

        _score = resultData.kAddScore;

        // iTween.ValueTo 호출 시 (int)iCurrScore 이 값을 Hash에서 float으로 저장할때 손실발생.
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", (int)_score, "onUpdate", "CurrScoreCounter", "delay", 1, "time", 1));
    }

    private void CurrScoreCounter(int iCurrScore)
    {
        // 계산되는 값이 손실범위를 넘어서면 원래값으로.
        if (iCurrScore >= _roundToInt)
            iCurrScore = (int)_score;

        string strScore = string.Empty;
        strScore = UtilFunc.CurrencyFormat(iCurrScore);
        _currentScore.text = string.Format(StringTableManager.GetData(3411), strScore);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnMoveMainMenu(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        ScreenCoverNextScene(SceneUIOpenType.MainMenu);
    }

    private void OnMoveGuildRaid(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        ScreenCoverNextScene(SceneUIOpenType.GuildRaid);
    }

    private void OnMoveGuildRaidReady(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        ScreenCoverNextScene(SceneUIOpenType.GuildRaidReady);
    }

    private void ScreenCoverNextScene(SceneUIOpenType NextScene)
    {
        SoundManager.Instance.StopBGM();

        ScreenCover cover = ScreenCover.GetScreenCover();
        cover.StartToEndCover(() => GameSceneManager.Instance.SetSceneChange(EnumGameScene.MainMenuScene, NextScene), null, 0.04f);
        DontDestroyOnLoad(cover.gameObject);
    }
}
