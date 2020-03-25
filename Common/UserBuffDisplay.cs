using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

enum enUserBuffType
{
    ExpGuild,
    GoldGuild,
    ExpSpecial,
    GoldSpecial,
    ExpEvent,
    GoldEvent,
    MAX,
}

public class UserBuffInfo
{
    public bool[] UserBuffKind = new bool[(int)enUserBuffType.MAX];

    public float GoldBuffGuild = 0.0f;
    public float ExpBuffGuild = 0.0f;
    public string BuffTextGuild = string.Empty;

    public float GoldBuffBuySubItem = 0.0f;
    public float ExpBuffBuySubItem = 0.0f;

    public float GoldBuffEvent = 0.0f;
    public float ExpBuffEvent = 0.0f;
    public string[] BuffTextEvent;

    public UserBuffInfo()
    {
        Clear();
    }

    public void Clear()
    {
        if (BuffTextEvent == null)
        {
            Array array = Enum.GetValues(typeof(_enTempEventType));
            BuffTextEvent = new string[array.Length];
        }

        BuffGuildClear();
        BuffGoldSubItemClear();
        BuffExpSubItemClear();
        BuffGoldEventClear();
        BuffExpEventClear();
    }

    public void BuffGuildClear()
    {
        UserBuffKind[(int)enUserBuffType.ExpGuild] = false;
        UserBuffKind[(int)enUserBuffType.GoldGuild] = false;

        GoldBuffGuild = 0.0f;
        ExpBuffGuild = 0.0f;
        BuffTextGuild = string.Empty;
    }

    public void BuffGoldSubItemClear()
    {
        UserBuffKind[(int)enUserBuffType.GoldSpecial] = false;
        GoldBuffBuySubItem = 0.0f;
    }

    public void BuffExpSubItemClear()
    {
        UserBuffKind[(int)enUserBuffType.ExpSpecial] = false;
        ExpBuffBuySubItem = 0.0f;
    }

    public void BuffGoldEventClear()
    {
        UserBuffKind[(int)enUserBuffType.GoldEvent] = false;
        BuffTextEvent[(int)_enTempEventType.Stage_Gold] = string.Empty;
        GoldBuffEvent = 0.0f;
    }

    public void BuffExpEventClear()
    {
        UserBuffKind[(int)enUserBuffType.ExpEvent] = false;
        BuffTextEvent[(int)_enTempEventType.Stage_Exp] = string.Empty;
        ExpBuffEvent = 0.0f;
    }

    public string GetText(enUserBuff buffType)
    {
        string result = string.Empty;
        string format = "\n{0} ({1}%)";

        switch (buffType)
        {
            case enUserBuff.GOLD:
                {
                    // 8797     [FFBB00]골드 획득량 {0}% 증가[-]\n─────────────\n
                    result = string.Format(StringTableManager.GetData(8797), GetValue(buffType));

                    if (GoldBuffGuild > 0.0f) result += string.Format(format, BuffTextGuild, (GoldBuffGuild * 100.0f));
                    if (GoldBuffBuySubItem > 0.0f) result += string.Format(format, StringTableManager.GetData(8794), (GoldBuffBuySubItem * 100.0f));
                    if (GoldBuffEvent > 0.0f) result += string.Format(format, BuffTextEvent[(int)_enTempEventType.Stage_Gold], (GoldBuffEvent));
                }
                break;

            case enUserBuff.EXP:
                {
                    // 8798     [FFBB00]영웅 획득량 { 0}% 증가[-]\n─────────────\n
                    result = string.Format(StringTableManager.GetData(8798), GetValue(buffType));

                    if (ExpBuffGuild > 0.0f) result += string.Format(format, BuffTextGuild, (ExpBuffGuild * 100.0f));
                    if (ExpBuffBuySubItem > 0.0f) result += string.Format(format, StringTableManager.GetData(8791), (ExpBuffBuySubItem * 100.0f));
                    if (ExpBuffEvent > 0.0f) result += string.Format(format, BuffTextEvent[(int)_enTempEventType.Stage_Exp], (ExpBuffEvent));
                }
                break;

            default:
#if DEBUG_LOG
                Debug.LogError("enUserBuff - no buff type");
#endif
                break;
        }

        return result;
    }

    private float GetValue(enUserBuff buffType)
    {
        float result = 0.0f;

        switch (buffType)
        {
            case enUserBuff.GOLD:
                result += (GoldBuffGuild * 100.0f) + (GoldBuffBuySubItem * 100.0f) + (GoldBuffEvent);
                break;

            case enUserBuff.EXP:
                result += (ExpBuffGuild * 100.0f) + (ExpBuffBuySubItem * 100.0f) + (ExpBuffEvent);
                break;

            default:
#if DEBUG_LOG
                Debug.LogError("enUserBuff - no buff type");
#endif
                break;
        }

        return result;
    }
}

public class UserBuffDisplay : MonoBehaviour
{

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [Header("클라이언트 Enum enUserBuff 순서대로")]
    [SerializeField] private UserBuffIcon[] _buffs;

    [SerializeField] private UIScrollView _scrollView;
    [SerializeField] private UIGrid _grid;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private SimpleHelpTip _simpleHelpTip = null;

    private float _time = 0.2f;
    private float _elapsedTime = 0.2f;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Awake()
    {
        for (int i = 0; i < _buffs.Length; i++)
        {
            _buffs[i].gameObject.SetActive(false);
            UIEventListener.Get(_buffs[i].gameObject).onPress = OnHelpTooltip;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _buffs.Length; i++)
            UIEventListener.Get(_buffs[i].gameObject).onPress -= OnHelpTooltip;
    }

    private void Update()
    {
        _time += Time.unscaledDeltaTime;
        if (_time > _elapsedTime)
        {
            _time = 0.0f;

            UserBuffInfo buffInfo = UserInfo.Instance.userBuffInfo;

            ActiveBuffIcon(_buffs[(int)enUserBuff.GOLD].gameObject, buffInfo.UserBuffKind[(int)enUserBuffType.GoldGuild] || buffInfo.UserBuffKind[(int)enUserBuffType.GoldSpecial] || buffInfo.UserBuffKind[(int)enUserBuffType.GoldEvent]);
            ActiveBuffIcon(_buffs[(int)enUserBuff.EXP].gameObject, buffInfo.UserBuffKind[(int)enUserBuffType.ExpGuild] || buffInfo.UserBuffKind[(int)enUserBuffType.ExpSpecial] || buffInfo.UserBuffKind[(int)enUserBuffType.ExpEvent]);
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(Vector3 position)
    {
        transform.localPosition = position;

        _grid.sorting = UIGrid.Sorting.Custom;
        _grid.onCustomSort = SortByCustomType;
    }

    private int SortByCustomType(Transform a, Transform b)
    {
        UserBuffIcon userBuffIcon1 = a.GetComponent<UserBuffIcon>();
        if (userBuffIcon1 == null)
            return 0;

        UserBuffIcon userBuffIcon2 = b.GetComponent<UserBuffIcon>();
        if (userBuffIcon2 == null)
            return 0;

        return userBuffIcon2.userBuffType.CompareTo(userBuffIcon1.userBuffType);
    }

    public void SetScrollViewDepth(int parentDepth)
    {
        UtilFunc.SetPanelDepth(_scrollView.panel, parentDepth + 5);
    }

    private void ActiveBuffIcon(GameObject icon, bool isActive)
    {
        bool preActive = icon.activeSelf && icon.activeInHierarchy;

        icon.SetActive(isActive);

        Transform parent = null;
        if (isActive == false)
            parent = transform;

        if (preActive == false)
        {
            parent = _grid.transform;

            UtilTransform.AttachTransForm(parent, icon.transform, SetTransformType.Default);

            _grid.Reposition();
            _scrollView.ResetPosition();
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnHelpTooltip(GameObject go, bool state)
    {
        UserBuffInfo buffInfo = UserInfo.Instance.userBuffInfo;

        UserBuffIcon icon = go.GetComponent<UserBuffIcon>();

        if (_simpleHelpTip == null)
            _simpleHelpTip = UIResourceMgr.CreatePrefab<SimpleHelpTip>(BUNDLELIST.PREFABS_UI_MAINMENU, transform, "SimpleHelpTip", SetTransformType.Default);

        _simpleHelpTip.gameObject.SetActive(state);

        if (state == true)
        {
            UtilTransform.AttachTransForm(go.transform, _simpleHelpTip.transform, SetTransformType.Default);

            _simpleHelpTip.Init(buffInfo.GetText(icon.userBuffType));
            _simpleHelpTip.OpenUI();

            UtilTransform.AttachTransForm(_scrollView.transform.parent, _simpleHelpTip.transform, SetTransformType.IgnoreValue);
        }
        else
        {
            _simpleHelpTip.CloseUI();
        }
    }
}
