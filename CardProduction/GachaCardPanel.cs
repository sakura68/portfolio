using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGL_DATA_READER;
using CodeStage.AntiCheat.ObscuredTypes;

/// <summary>
/// 3D 가챠 연출 
/// </summary>
public class GachaCardPanel : UIWindow
{
    public enum enGachaBoxType
    {
        Creature,
        Item,
    }

    public enum enGachaType
    {
        One,
        Continue,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UIButton _AllCardOpenButton;
    [SerializeField] private UILabel _AllButtonLabel;

    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private UILabel _ExitLabel;
    [SerializeField] private GameObject _ExitPanelGameObject;

    [SerializeField] private GameObject _SkipButton;
    [SerializeField] private UILabel _SkipButtonLabel;

    [SerializeField] private GameObject _reGachaButton;
    [SerializeField] private UILabel _reGachaButtonLabel;
    [SerializeField] private UILabel _reGachaButtonCountLabel;

    [SerializeField] private GameObject _exitButton;
    [SerializeField] private UILabel _exitButtonLabel;
    
    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private DelegateBoolOnceEventDone OnAllOpenEvent;   // 카드 모두 오픈하는 이벤트

    private Camera _3DCamera = null;

    private CardResult3DElement _CardResult3D;       // 카드 뽑기 결과 3D 모델창.

    private GachaCardCamera _GachaCardCamera = null;
    private GachaBox _GachaCardBox = null;

    private PvpEnterancePopup _PvpEnterancePopup = null;

    private CardOpenType _CardOpenType = CardOpenType.Max;
    private enGachaBoxType _GachaBoxType = enGachaBoxType.Creature;
    private enSoundBGM _PreBGM = enSoundBGM.NONE;
    private enGachaType _gachaType = enGachaType.One;

    private Transform _3DGUITransform = null;

    private Vector3 _3DCameraOriginPos = Vector3.zero;                                                  // 연출 이전 카메라 포지션.
    private Quaternion _3DCameraOriginRotion = Quaternion.identity;                                     // 연출 이전 카메라 로테이션.

    private Vector3 _3DCameraGachaBoxPos = Vector3.zero;                                                // 3D 박스 나올때 연출 포지션
    private Quaternion _3DCameraGachaBoxRotion = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));     // 3D 박스 나올때 연출 로테이션.

    private Vector3 _3DResultCameraPos = new Vector3(0.0f, 0.0f, -10.8f);                               // 3D 카드 연출때(캐릭터연출) 쓰는 3D카메라 포지션.
    private Quaternion _3DResultCameraRotion = Quaternion.identity;

    private int _CardTotalCount;     // 카드 카운트    

    private IEnumerator _OpenPanelCoroutine = null;
    private IEnumerator _ActiveCardPanelCoroutine = null;
    private IEnumerator _CardClick = null;
    private IEnumerator _PlayFXCoroutine = null;
    private IEnumerator _OnExitPanelCoroutine = null;

    private int _reGachaPriceValue = 0;

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public override void Init()
    {
        _PreBGM = SoundManager.Instance.currentBGMPlay;
        SoundManager.Instance.StopBGM();

        _TitleLabel.text = StringTableManager.GetData(3486);
        _AllButtonLabel.text = StringTableManager.GetData(3485);
        _ExitLabel.text = StringTableManager.GetData(6311);
        _SkipButtonLabel.text = StringTableManager.GetData(4480);
        _exitButtonLabel.text = StringTableManager.GetData(2);

        _reGachaButtonLabel.text = StringTableManager.GetData(8775); // 8775    다시 소환

        _ExitLabel.gameObject.SetActive(false);
        _ExitPanelGameObject.SetActive(false);
        _SkipButton.SetActive(false);

        _3DCamera = UIControlManager.instance.Camera3D;
        _3DCameraOriginPos = _3DCamera.transform.localPosition;
        _3DCameraOriginRotion = _3DCamera.transform.localRotation;

        _3DGUITransform = UIControlManager.instance.GUI3DObj.transform;

        UIControlManager.instance.InActiveWindow(new enUIType[] { WindowType });
        UIControlManager.instance.Set3DProduction(false);
        UIControlManager.instance.ShowLoading(false);
    }

    public override void Clear()
    {
    }

    protected override void OnEnable()
    {
    }


    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    protected override void OnDisable()
    {
    }

    protected override void Awake()
    {
        UIEventListener.Get(_AllCardOpenButton.gameObject).onClick = AllCardOpen;
        UIEventListener.Get(_ExitPanelGameObject).onClick = OnExit;
        UIEventListener.Get(_SkipButton).onClick = OnSkip;

        UIEventListener.Get(_reGachaButton).onClick = OnReGacha;
        UIEventListener.Get(_exitButton).onClick = OnExit;
    }

    protected override void OnDestroy()
    {
        UIEventListener.Get(_AllCardOpenButton.gameObject).onClick -= AllCardOpen;
        UIEventListener.Get(_ExitPanelGameObject).onClick -= OnExit;
        UIEventListener.Get(_SkipButton).onClick -= OnSkip;

        UIEventListener.Get(_reGachaButton).onClick = OnReGacha;
        UIEventListener.Get(_exitButton).onClick -= OnExit;

        if (_GachaCardCamera != null) DestroyImmediate(_GachaCardCamera.gameObject);
        if (_GachaCardBox != null) DestroyImmediate(_GachaCardBox.gameObject);
        if (_PvpEnterancePopup != null) _PvpEnterancePopup.CloseUI();

        if (_OpenPanelCoroutine != null)
        {
            StopCoroutine(_OpenPanelCoroutine);
            _OpenPanelCoroutine = null;
        }

        if(_ActiveCardPanelCoroutine != null)
        {
            StopCoroutine(_ActiveCardPanelCoroutine);
            _ActiveCardPanelCoroutine = null;
        }

        if(_CardClick != null)
        {
            StopCoroutine(_CardClick);
            _CardClick = null;
        }

        if(_PlayFXCoroutine != null)
        {
            StopCoroutine(_PlayFXCoroutine);
            _PlayFXCoroutine = null;
        }

        if(_OnExitPanelCoroutine != null)
        {
            StopCoroutine(_OnExitPanelCoroutine);
            _OnExitPanelCoroutine = null;
        }
    }

    public override void OpenUI()
    {
        base.OpenUI();
    }

    public override bool OnClickBack()
    {
        base.OnClickBack();

        return false;
    }

    /// <summary>
    /// 상점 카드정보를 생성.
    /// </summary>
    /// <param name="recvData"></param>

    public void InitCard(_stShopBuyAck recvData)
    {
        _CardOpenType = CardOpenType.Shop;

        if (recvData.vAddCreatures.Count > 0)
        {
            _CardTotalCount = recvData.vAddCreatures.Count;
        }
        else if(recvData.vAddItems.Count > 0)
        {
            _CardTotalCount = recvData.vAddItems.Count;
        }

        CreateCard(recvData);
    }
    /// <summary>
    /// 메일함 카드정보를 생성.
    /// </summary>
    /// <param name="stMailReadAck"></param>
    public void InitCard(_stMailReadAck stMailReadAck)
    {
        _CardOpenType = CardOpenType.Mail;

        _CardTotalCount = 1;

        // 서버에서 준 데이터에서 크리쳐 정보를 찾는다.
        CCreatureDetail BoxAddCreateCreature = null;
        if (stMailReadAck.vItemBoxAddCreateCreature != null)
        {
            foreach (CCreatureDetail AddCreature in stMailReadAck.vItemBoxAddCreateCreature)
            {
                BoxAddCreateCreature = AddCreature;
                break;
            }
        }

        // 찾은 크리쳐로 연출 준비
        if (BoxAddCreateCreature != null)
        {
            _stShopBuyAck stShopBuyAck = new _stShopBuyAck();

            stShopBuyAck.cShopBuyGood = new CShopGood();
            stShopBuyAck.cShopBuyGood.kPayType = DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_DIA;       // 카드 뒷면 default

            stShopBuyAck.vAddCreatures = new _vCreatureDetail();
            stShopBuyAck.vAddCreatures.Add(BoxAddCreateCreature);

            CreateCard(stShopBuyAck);
        }

        // 서버에서 준 데이터에서 아이템 정보를 찾는다.
        CItem vItemBoxAddCreateItem = null;
        if (stMailReadAck.vItemBoxAddCreateItem != null)
        {
            foreach (CItem AddItem in stMailReadAck.vItemBoxAddCreateItem)
            {
                vItemBoxAddCreateItem = AddItem;
                break;
            }
        }

        // 아이템 연출 준비
        if(vItemBoxAddCreateItem != null)
        {
            _stShopBuyAck stShopBuyAck = new _stShopBuyAck();

            stShopBuyAck.cShopBuyGood = new CShopGood();
            stShopBuyAck.cShopBuyGood.kPayType = DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_DIA;       // 카드 뒷면 default

            stShopBuyAck.vAddItems = new _vItem();
            stShopBuyAck.vAddItems.Add(vItemBoxAddCreateItem);

            CreateCard(stShopBuyAck);
        }

        // 연출에 필요한 배경 지우기
        UIControlManager.instance.SetMainMenu_3DObject(false);
    }

    /// <summary>
    /// 뽑기 카드박스 생성
    /// </summary>
    /// <param name="recvData"></param>
    private void CreateCard(_stShopBuyAck recvData)
    {
        DATA_SHOP_NEW shopdata = CDATA_SHOP_NEW.Get(recvData.cShopBuyGood.kCategoryType, recvData.cShopBuyGood.kGoodsID);
        if(shopdata == null || shopdata.iPriceValue2 == 0)
        {
            _gachaType = enGachaType.One;
        }
        else
        {
            _gachaType = enGachaType.Continue;
            _reGachaButtonCountLabel.text = shopdata.iPriceValue2.ToString();
            _reGachaPriceValue = shopdata.iPriceValue2;
        }

        ActiveCardPanel(false);
        _ExitPanelGameObject.gameObject.SetActive(false);
        _ExitLabel.gameObject.SetActive(false);
        _reGachaButton.SetActive(false);
        _exitButton.SetActive(false);

        if (_GachaCardCamera == null)
        {
            _GachaCardCamera = UIResourceMgr.CreatePrefab<GachaCardCamera>(BUNDLELIST.PREFABS_ETC_GACHACARD, UIControlManager.instance.GUI3DObj.transform, "GachaCardCamera");
        }

        _GachaCardCamera.Init();

        if (recvData.vAddCreatures != null && recvData.vAddCreatures.Count > 0)
        {
            _GachaBoxType = enGachaBoxType.Creature;
            if (_GachaCardBox == null)
            {
                _GachaCardBox = UIResourceMgr.CreatePrefab<GachaBox>(BUNDLELIST.PREFABS_ETC_GACHACARD, UIControlManager.instance.MainMenuBaseObj.transform, "GachaMonsterBox");
                _GachaCardBox.Init(recvData);
            }
        }
        else if(recvData.vAddItems != null && recvData.vAddItems.Count > 0)
        {
            _GachaBoxType = enGachaBoxType.Item;
            if (_GachaCardBox == null)
            {
                _GachaCardBox = UIResourceMgr.CreatePrefab<GachaBox>(BUNDLELIST.PREFABS_ETC_GACHACARD, UIControlManager.instance.MainMenuBaseObj.transform, "GachaBox");
                _GachaCardBox.Init(recvData);
            }
        }

        _GachaCardBox.CreateCard(recvData, OnCardOpenEvent, OnCardOpen3DEvent);
        _GachaCardBox.gameObject.SetActive(true);
        _GachaCardBox.Anim.Stop();
        _GachaCardBox.OpenEffect.SetActive(false);
        _GachaCardBox.EndEffect.SetActive(false);

        Set3DGachaCamera();

        if (_CardResult3D == null)
        {
            _CardResult3D = UIResourceMgr.CreatePrefab<CardResult3DElement>(BUNDLELIST.PREFABS_UI_CASHSHOP, this.transform.parent, "CardResult3DElement");
            _CardResult3D.Init(this, OnCardOpenEvent, SetAllCardOpenButton, Set3DResultCamera, Set3DGachaCamera);
        }

        _CardResult3D.CardClear();
        _CardResult3D.SetCardResult(recvData.vAddCreatures);
        _CardResult3D.CloseUI();

        if (_OpenPanelCoroutine != null)
        {
            StopCoroutine(_OpenPanelCoroutine);
            _OpenPanelCoroutine = null;
        }

        _OpenPanelCoroutine = IeOpenPanel(0.5f, _GachaCardBox.AnimLength);
        StartCoroutine(_OpenPanelCoroutine);
    }

    private void Set3DGachaCamera()
    {
        if (_3DCamera != null)
        {
            _3DCamera.transform.parent = _GachaCardCamera.transform;
            _3DCamera.transform.localPosition = _3DCameraGachaBoxPos;
            _3DCamera.transform.localRotation = _3DCameraGachaBoxRotion;
        }

        _GachaCardBox.transform.localPosition = Vector3.zero;

        if(_CardClick != null)
        {
            StopCoroutine(_CardClick);
            _CardClick = null;
        }

        _CardClick = IeCardClick(true);
        StartCoroutine(IeCardClick(true));
    }
    
    private IEnumerator IeCardClick(bool isClick)
    {
        yield return new WaitForSeconds(0.5f);

        _GachaCardBox.IsClick = true;
    }

    /// <summary>
    /// 연출 후 카메라위치 되돌리기, 연출박스 안보이게 옮기기
    /// </summary>
    private void Set3DResultCamera()
    {
        if (_3DCamera != null)
        {
            _3DCamera.transform.parent = _3DGUITransform;
            _3DCamera.transform.localPosition = _3DResultCameraPos;
            _3DCamera.transform.localRotation = _3DResultCameraRotion;
        }

        _GachaCardBox.transform.localPosition = new Vector3(0.0f, 10000.0f, 0.0f);
        _GachaCardBox.IsClick = false;
    }

    private void ActiveCardPanel(bool IsActive)
    {
        _TitleLabel.gameObject.SetActive(IsActive);
        _AllCardOpenButton.gameObject.SetActive(IsActive);
        _AllButtonLabel.gameObject.SetActive(IsActive);
    }

    private IEnumerator IeOpenPanel(float time, float ActivePanelTime)
    {
        yield return new WaitForSeconds(time);

        if(_PlayFXCoroutine != null)
        {
            StopCoroutine(_PlayFXCoroutine);
            _PlayFXCoroutine = null;
        }

        // 사운드에 약간 딜레이 있어서 0.1초 뒤에 플레이
        _PlayFXCoroutine = IePlayFX(0.1f);
        StartCoroutine(_PlayFXCoroutine);

        //if(_CardTotalCount == 1)
        //{
        //    _GachaCardBox.Anim.Play("Open01");
        //}
        //else
        //{
        //    _GachaCardBox.Anim.Play("Open");
        //}
        _GachaCardCamera.Anim.Play();
        _GachaCardBox.Anim.Play();
        _GachaCardBox.OpenEffect.SetActive(true);
        _SkipButton.SetActive(true);

        if (_ActiveCardPanelCoroutine != null)
        {
            StopCoroutine(_ActiveCardPanelCoroutine);
            _ActiveCardPanelCoroutine = null;
        }

        _ActiveCardPanelCoroutine = IeActiveCardPanel(ActivePanelTime);
        StartCoroutine(_ActiveCardPanelCoroutine);
    }

    private IEnumerator IePlayFX(float time)
    {
        yield return new WaitForSeconds(time);

        if (_GachaBoxType == enGachaBoxType.Creature)
        {
            SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPENBOX_CREATURE);
        }
        else if (_GachaBoxType == enGachaBoxType.Item)
        {
            SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPENBOX_ITEM);
        }
    }

    private IEnumerator IeActiveCardPanel(float time)
    {
        yield return new WaitForSeconds(time);

        _SkipButton.gameObject.SetActive(false);
        ActiveCardPanel(true);
        _GachaCardBox.ActiveCardAction();

        SoundManager.Instance.LoadSoundBGMPlay(enSoundBGM.CARDLOOP, GameOption.intance.IsSoundBgm);
        //SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPENBOX_CARDLOOP);

        if (_CardTotalCount == 1)
        {
            _AllCardOpenButton.gameObject.SetActive(false);
        }
    }

    public void ClearReGacha()
    {
        //UIControlManager.instance.ActiveWindow();

        UtilTransform.AttachTransForm(_3DGUITransform, _3DCamera.transform, SetTransformType.IgnoreValue);
        _3DCamera.transform.localPosition = _3DCameraOriginPos;
        _3DCamera.transform.localRotation = _3DCameraOriginRotion;

        UIControlManager.instance.RemoveWindow(enUIType.CARDDIRECTINGPANEL);

        //UIControlManager.instance.Set3DProduction(true);

        if (_CardResult3D != null) DestroyImmediate(_CardResult3D.gameObject);

        SoundManager.Instance.LoadSoundBGMPlay(_PreBGM, GameOption.intance.IsSoundBgm);
    }

    /// <summary>
    /// 카드 오픈 종료 이벤트
    /// </summary>
    private void OnCardOpenEvent()
    {
        _CardTotalCount--;
        if (_CardTotalCount <= 0)
        {
            _AllCardOpenButton.gameObject.SetActive(false);

            _CardTotalCount = 0;
            if (_OnExitPanelCoroutine != null)
            {
                StopCoroutine(_OnExitPanelCoroutine);
                _OnExitPanelCoroutine = null;
            }

            _OnExitPanelCoroutine = IeOnExitPanel();
            StartCoroutine(_OnExitPanelCoroutine);
        }
    }

    /// <summary>
    /// 카드 전체 열기
    /// </summary>
    private void SetAllCardOpenButton()
    {
        _AllCardOpenButton.gameObject.SetActive(true);

        if (_CardTotalCount <= 0)
        {
            _AllCardOpenButton.gameObject.SetActive(false);

            _CardTotalCount = 0;
            if (_OnExitPanelCoroutine != null)
            {
                StopCoroutine(_OnExitPanelCoroutine);
                _OnExitPanelCoroutine = null;
            }

            _OnExitPanelCoroutine = IeOnExitPanel();
            StartCoroutine(_OnExitPanelCoroutine);
        }
    }

    /// <summary>
    /// 뽑기 연출 끝나면 다시뽑기 버튼, 뒤로가기 버튼 활성화
    /// </summary>
    /// <returns></returns>
    private IEnumerator IeOnExitPanel()
    {
        yield return new WaitForSeconds(1.0f);

        _AllCardOpenButton.gameObject.SetActive(false);

        if (_gachaType == enGachaType.One)
        {
            _ExitLabel.gameObject.SetActive(true);
            _ExitPanelGameObject.SetActive(true);
        }
        else
        {
            _reGachaButton.SetActive(true);
            _exitButton.SetActive(true);
        }
    }

    /// <summary>
    /// 카드 오픈할때 연출 이벤트.
    /// </summary>
    /// <param name="kCreatureID"></param>
    private void OnCardOpen3DEvent(int kCreatureID)
    {
        if (_CardResult3D == null)
            return;

        // 3d 연출중일때는 모두열기 버튼을 닫아놓는다.
        _AllCardOpenButton.gameObject.SetActive(false);
        _GachaCardBox.IsClick = false;

        StartCoroutine(IECardOpen3DEvent(kCreatureID));
        //CardOpen3DEvent(kCreatureID);
    }

    private IEnumerator IECardOpen3DEvent(int kCreatureID)
    //private void CardOpen3DEvent(int kCreatureID)
    {
        MainMenuCreatureContainer mainMenuCreatureContainer = _CardResult3D.Get3DModel(kCreatureID);
        if (mainMenuCreatureContainer != null)
        {
            yield return new WaitForSeconds(1.0f);
            _CardResult3D.StartCardResult(mainMenuCreatureContainer);
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void AllCardOpen(GameObject go)
    {
        _AllCardOpenButton.gameObject.SetActive(false);
        _CardResult3D.OnAllCardOpen();
        _GachaCardBox.AllCardOpen();
    }

    /// <summary>
    /// 버튼 종료
    /// </summary>        
    private void OnExit(GameObject go)
    {
        UIControlManager.instance.ActiveWindow();

        UtilTransform.AttachTransForm(_3DGUITransform, _3DCamera.transform, SetTransformType.IgnoreValue);
        _3DCamera.transform.localPosition = _3DCameraOriginPos;
        _3DCamera.transform.localRotation = _3DCameraOriginRotion;

        UIControlManager.instance.RemoveWindow(enUIType.CARDDIRECTINGPANEL);

        UIControlManager.instance.Set3DProduction(true);

        if (_CardResult3D != null) DestroyImmediate(_CardResult3D.gameObject);

        if (_CardOpenType == CardOpenType.Shop)
        {
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954)
                    , StringTableManager.GetData(3007));
        }
        else if (_CardOpenType == CardOpenType.Mail)
        {
            //UIControlManager.instance.SetMainMenu_3DObject(true);

            //MainMenuWindow mainWin = UIControlManager.instance.GetWindow<MainMenuWindow>(enUIType.MAINMENU);
            //if (mainWin != null)
            //{
            //    UIControlManager.instance.m_EditWindowBg.SetActive(false);
            //}

            //BookWindow bookWin = UIControlManager.instance.GetWindow<BookWindow>(enUIType.BOOK);
            //if(bookWin != null)
            //{
            //    UIControlManager.instance.m_BookWindowBg.SetActive(true);
            //}
        }

        SoundManager.Instance.LoadSoundBGMPlay(_PreBGM, GameOption.intance.IsSoundBgm);

#if TUTORIAL
#if DEBUG_LOG
        Debug.Log("===============" + _CardOpenType.ToString());
#endif
        if (_CardOpenType == CardOpenType.Shop)
        {            
            GameSceneManager.Instance.TutorialManager.ExcuteUITutorial(DATA_TUTORIAL._enTutorialLevel.Tutorial_13);
        }
#endif

    }

    /// <summary>
    /// 뽑기 연출 스킵
    /// </summary>
    /// <param name="go"></param>
    private void OnSkip(GameObject go)
    {
        //go.SetActive(false);

        if (_OpenPanelCoroutine != null)
        {
            StopCoroutine(_OpenPanelCoroutine);
            _OpenPanelCoroutine = null;
        }

        if (_PlayFXCoroutine != null)
        {
            StopCoroutine(_PlayFXCoroutine);
            _PlayFXCoroutine = null;
        }

        if (_ActiveCardPanelCoroutine != null)
        {
            StopCoroutine(_ActiveCardPanelCoroutine);
            _ActiveCardPanelCoroutine = null;
        }

        _ActiveCardPanelCoroutine = IeActiveCardPanel(0f);
        StartCoroutine(_ActiveCardPanelCoroutine);

        SoundManager.Instance.StopUISound();

        _GachaCardBox.OpenEffect.SetActive(false);
        _GachaCardBox.EndEffect.SetActive(true);

        _GachaCardCamera.Anim["GachaCardCameraTake001"].time = _GachaCardCamera.Anim.clip.length;
        _GachaCardBox.Anim["Open"].time = _GachaCardBox.Anim.clip.length;
    }

    /// <summary>
    /// 상점으로 돌아가지 않고 다시 뽑기
    /// </summary>
    /// <param name="go"></param>
    private void OnReGacha(GameObject go)
    {
        if (go != null)
            SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        CashShopWindow win = UIControlManager.instance.GetWindow<CashShopWindow>(enUIType.SHOP);
        if (win != null)
        {
            if(win.PaymentBuy(_enGachaDiscountType.enGachaDiscountType_Continue))
            {
                if (_PvpEnterancePopup == null)
                {
                    _PvpEnterancePopup = UIResourceMgr.CreatePrefab<PvpEnterancePopup>(BUNDLELIST.PREFABS_UI_DUNGEON, transform, "PvpEnterancePopup");
                    _PvpEnterancePopup.Init(PvpEnterancePopup.EnterPopupType.ReGacha, (ulong)_reGachaPriceValue);
                }

                _PvpEnterancePopup.OpenUI();
            }
        }
    }    
}