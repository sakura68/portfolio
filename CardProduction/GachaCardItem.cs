using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;
public class GachaCardItem : MonoBehaviour
{
    public enum enCardGrade
    {
        None,
        Normal,
        Royal,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _Card;

    //[SerializeField] private Animation _Anim;

    [SerializeField] private GameObject _CardTrailEffect = null;
    [SerializeField] private GameObject _RoyalEffect = null;
    [SerializeField] private GameObject _NormalEffect = null;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GachaCard _GachaCardUI = null;
    private GameObject _CardEffect = null;

    private BoxCollider _BoxCollider = null;

    private Texture _CardTexture = null;
    public Texture CardTexture { get { return _CardTexture; } }

    private enCardGrade _CardGrade = enCardGrade.None;
    public enCardGrade CardGrade { get { return _CardGrade; } }

    private Quaternion _OriginRotate = Quaternion.identity;

    private Vector3 _OriginPos = Vector3.zero;

    private CItem _CItem = null;
    public CItem Item { get { return _CItem; } }

    private DATA_ITEM_NEW _ItemTableData = null;
    public DATA_ITEM_NEW ItemTableData { get { return _ItemTableData; } }

    private int _CreatureID = 0;

    private float _ElapsedTime = 0.0f;
    private readonly float _Time = 1.2f;

    private bool _IsAction = false;

    private bool _IsFinish = false;
    public bool IsFinish { get { return _IsFinish; } }

    private bool _Init3D = false;

    private readonly string _Idle = "IDLE";

    private DelegateOnceEventDone OnCardOpenEvent;

    public DelegateOnceEventDoneWithInt onOpen3DEvent;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Update()
    {
        if (_IsAction == true && _IsFinish == false)
        {
            if (_Init3D == false)
            {
                _Init3D = true;
                _OriginRotate = transform.localRotation;

                if (onOpen3DEvent != null)
                    onOpen3DEvent(_CreatureID);

                if (_CardGrade == enCardGrade.Normal)
                {
                    SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPENCARD_NORMAL);
                }

                if (_CardGrade == enCardGrade.Royal)
                {
                    SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPENCARD_ROYAL);
                }
            }

            float Speed = 50.0f;

            _ElapsedTime += Time.deltaTime;
            transform.Rotate(new Vector3(0.0f, Speed * _ElapsedTime, 0.0f));

            if (_ElapsedTime > _Time)
            {
                //if (_CardGrade == enCardGrade.Normal)
                //{
                //    SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPENCARD_NORMAL);
                //}

                //if (_CardGrade == enCardGrade.Royal && onOpen3DEvent == null)
                //{                    
                //    SoundManager.Instance.PlayFX(enSoundFXUI.UI_OPENCARD_ROYAL);
                //}

                transform.localRotation = _OriginRotate;

                _ElapsedTime = 0.0f;
                _IsAction = false;
                _IsFinish = true;

                if (_CardEffect != null)
                {
                    _CardEffect.SetActive(true);
                }

                if (onOpen3DEvent == null)
                {
                    if (OnCardOpenEvent != null)
                    {
                        OnCardOpenEvent();
                        OnCardOpenEvent = null;
                    }
                }

                ChangeCard();
            }
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init()
    {
        _BoxCollider = gameObject.GetComponent<BoxCollider>();

        if(_CardTrailEffect != null) _CardTrailEffect.SetActive(true);
        if (_RoyalEffect != null) _RoyalEffect.SetActive(false);
        if (_NormalEffect != null) _NormalEffect.SetActive(false);

        EnableCollider(false);

        if (_CardEffect == null)
        {
            _CardEffect = UIResourceMgr.CreatePrefab(BUNDLELIST.PREFABS_EFFECT_COMMON, transform, "Fx_CardRotate");
            UtilTransform.AttachTransForm(transform, _CardEffect.transform, SetTransformType.IgnoreValue);

            _CardEffect.layer = 0;
            _CardEffect.transform.localPosition = new Vector3(0.0f, 0.0f, 0.02f);
            _CardEffect.transform.localScale = Vector3.one;
            _CardEffect.transform.localRotation = Quaternion.identity;
            _CardEffect.SetActive(false);
        }
    }

    /// <summary>
    /// 크리쳐 카드 생성.
    /// </summary>
    public void CreateCard(DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType kPayType, enCardGrade type, Texture CardTexture, CCreatureDetail CreatureDetail, 
        DATA_CREATURE_NEWVER CreatureTableData, DelegateOnceEventDone cardOpenEvt, DelegateOnceEventDoneWithInt event3D)
    {
        _CardGrade = type;
        _CardTexture = CardTexture;
        _CreatureID = CreatureTableData.m_iCreatureTID;
        OnCardOpenEvent = cardOpenEvt;
        onOpen3DEvent = event3D;

        if (type == enCardGrade.Normal)
        {
            _GachaCardUI = UIResourceMgr.CreatePrefab<GachaCard>(BUNDLELIST.PREFABS_UI_CASHSHOP, transform, "GachaCardNormal3D");
        }
        else if (type == enCardGrade.Royal)
        {
            _GachaCardUI = UIResourceMgr.CreatePrefab<GachaCard>(BUNDLELIST.PREFABS_UI_CASHSHOP, transform, "GachaCardRoyal3D");
        }

        if (_GachaCardUI != null)
        {
            _GachaCardUI.Init(CreatureDetail);

            _GachaCardUI.gameObject.layer = 0;
            _GachaCardUI.transform.localPosition = new Vector3(0.0f, 10000.0f, 0.01f);
            _GachaCardUI.transform.localScale = new Vector3(0.0017f, 0.0017f, 0.0017f);
            _GachaCardUI.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

            _GachaCardUI.gameObject.SetActive(true);
        }

        SkinnedMeshRenderer skinnedMeshRenderer = _Card.GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer != null)
        {
            if (CardTexture is Texture)
            {
                skinnedMeshRenderer.material.mainTexture = (Texture)CardTexture;
            }
            //else if (CardTexture is Material)
            //{
            //    skinnedMeshRenderer.material.mainTexture = ((Material)CardTexture).mainTexture;
            //}
        }
    }

    /// <summary>
    /// 아이템 카드 생성
    /// </summary>
    public void CreateCard(DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType kPayType, enCardGrade CardGrade, Texture CardTexture, 
        CItem item, DATA_ITEM_NEW ItemTableData, DelegateOnceEventDone cardOpenEvt)
    {
        _CardGrade = CardGrade;
        _CardTexture = CardTexture;
        _CItem = item;
        _ItemTableData = ItemTableData;

        OnCardOpenEvent = cardOpenEvt;

        if (_CardGrade == enCardGrade.Normal)
        {
            _GachaCardUI = UIResourceMgr.CreatePrefab<GachaCard>(BUNDLELIST.PREFABS_UI_CASHSHOP, transform, "GachaCardNormal3D");
        }
        else if (_CardGrade == enCardGrade.Royal)
        {
            _GachaCardUI = UIResourceMgr.CreatePrefab<GachaCard>(BUNDLELIST.PREFABS_UI_CASHSHOP, transform, "GachaCardRoyal3D");
        }

        if (_GachaCardUI != null)
        {
#if GMTOOLSHOP
            _GachaCardUI.Init(kPayType, _CardGrade, item, ItemTableData);

            _GachaCardUI.gameObject.layer = 0;
            _GachaCardUI.transform.localPosition = new Vector3(0.0f, 10000.0f, 0.01f);
            _GachaCardUI.transform.localScale = new Vector3(0.0017f, 0.0017f, 0.0017f);
            _GachaCardUI.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

            _GachaCardUI.gameObject.SetActive(true);
#else
            _GachaCardUI.Init(kPayType, _CardGrade, item, ItemTableData);

            _GachaCardUI.gameObject.layer = 0;
            _GachaCardUI.transform.localPosition = new Vector3(0.0f, 10000.0f, 0.01f);
            _GachaCardUI.transform.localScale = new Vector3(0.0017f, 0.0017f, 0.0017f);
            _GachaCardUI.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

            _GachaCardUI.gameObject.SetActive(true);
#endif

        }

        SkinnedMeshRenderer skinnedMeshRenderer = _Card.GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer != null)
        {
            if (CardTexture is Texture)
            {
                skinnedMeshRenderer.material.mainTexture = (Texture)CardTexture;
            }
            //else if (CardTexture is Material)
            //{
            //    skinnedMeshRenderer.material.mainTexture = ((Material)CardTexture).mainTexture;
            //}
        }
    }

    /// <summary>
    /// 아이템 카드 Royal일때 중앙으로 보이게 바꾸기.
    /// </summary>
    public void ChangeItemCard(DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType kPayType, enCardGrade grade, Texture CardTexture, CItem item, DATA_ITEM_NEW itemTableData)
    {
        _CardGrade = grade;
        _CItem = item;
        _ItemTableData = itemTableData;

        if (_GachaCardUI != null)
        {
            DestroyImmediate(_GachaCardUI.gameObject);
        }

        if (_CardGrade == enCardGrade.Normal)
        {
            _GachaCardUI = UIResourceMgr.CreatePrefab<GachaCard>(BUNDLELIST.PREFABS_UI_CASHSHOP, transform, "GachaCardNormal3D");
        }
        else if (_CardGrade == enCardGrade.Royal)
        {
            _GachaCardUI = UIResourceMgr.CreatePrefab<GachaCard>(BUNDLELIST.PREFABS_UI_CASHSHOP, transform, "GachaCardRoyal3D");
        }

        if (_GachaCardUI != null)
        {
            _GachaCardUI.Init(kPayType, _CardGrade, item, itemTableData);

            _GachaCardUI.gameObject.layer = 0;
            _GachaCardUI.transform.localPosition = new Vector3(0.0f, 10000.0f, 0.01f);
            _GachaCardUI.transform.localScale = new Vector3(0.0017f, 0.0017f, 0.0017f);
            _GachaCardUI.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

            _GachaCardUI.gameObject.SetActive(true);
        }

        SkinnedMeshRenderer skinnedMeshRenderer = _Card.GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer != null)
        {
            if (CardTexture is Texture)
            {
                skinnedMeshRenderer.material.mainTexture = (Texture)CardTexture;
            }
            //else if (CardTexture is Material)
            //{
            //    skinnedMeshRenderer.material.mainTexture = ((Material)CardTexture).mainTexture;
            //}
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
        _Card.SetActive(isActive);
    }

    public void Action()
    {
        _IsAction = true;
        EnableCollider(false);
    }

    public void EnableCollider(bool IsEnable)
    {
        if(_BoxCollider != null)
        {
            _BoxCollider.enabled = IsEnable;
        }
    }

    private void ChangeCard()
    {
        _GachaCardUI.transform.localPosition = new Vector3(0.0f, 0.0f, 0.01f);
        //_CardEffect.SetActive(false);
        _Card.SetActive(false);
    }

    public void SetEffect()
    {
        if(_CardTrailEffect != null) _CardTrailEffect.SetActive(false);

        if (_CardGrade == enCardGrade.Normal)
        {
            if (_RoyalEffect != null) _RoyalEffect.SetActive(false);
            if (_NormalEffect != null) _NormalEffect.SetActive(true);
        }
        else if(_CardGrade == enCardGrade.Royal)
        {
            if (_RoyalEffect != null) _RoyalEffect.SetActive(true);
            if (_NormalEffect != null) _NormalEffect.SetActive(false);
        }
    }

    public void SetAnimation()
    {
        Animation anim = gameObject.AddComponent<Animation>();

        Vector3 OriginPos = transform.localPosition;
        AnimationCurve AnimationCurveX = new AnimationCurve();
        AnimationCurve AnimationCurveY = new AnimationCurve();
        AnimationCurve AnimationCurveZ = new AnimationCurve();
        Keyframe[] Keyframes = new Keyframe[3];

        float Random = UnityEngine.Random.Range(0.0f, 3.0f);

        Keyframes[0].time = 0 + Random;
        Keyframes[0].value = OriginPos.x;
        Keyframes[1].time = 0.75f + Random;
        Keyframes[1].value = OriginPos.x;
        Keyframes[2].time = 1.5f + Random;
        Keyframes[2].value = OriginPos.x;
        AnimationCurveX.keys = Keyframes;

        Keyframes[0].time = 0 + Random;
        Keyframes[0].value = OriginPos.y;
        Keyframes[1].time = 0.75f + Random;
        Keyframes[1].value = OriginPos.y + 0.01198f;
        Keyframes[2].time = 1.5f + Random;
        Keyframes[2].value = OriginPos.y;
        AnimationCurveY.keys = Keyframes;

        Keyframes[0].time = 0 + Random;
        Keyframes[0].value = OriginPos.z;
        Keyframes[1].time = 0.75f + Random;
        Keyframes[1].value = OriginPos.z;
        Keyframes[2].time = 1.5f + Random;
        Keyframes[2].value = OriginPos.z;
        AnimationCurveZ.keys = Keyframes;

        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        clip.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurveX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurveY);
        clip.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurveZ);
        anim.AddClip(clip, _Idle);
        anim.wrapMode = WrapMode.Loop;
        anim.Play(_Idle);
    }
}