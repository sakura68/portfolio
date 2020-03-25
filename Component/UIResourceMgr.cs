using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIResourceMgr
{
#region AssetBundle ReLinkAtlas
#if ASSET_BUNDLE
    private static Dictionary<string, UIAtlas> _AtlasList = new Dictionary<string, UIAtlas>();

    private const string AbyssRing = "AbyssRing";
    private const string Card = "Card";
    private const string Local = "Local";
    private const string Main = "Main";
    private const string newAchivement = "newAchivement";
    private const string newBattle = "newBattle";
    private const string newDungeon = "newDungeon";
    private const string newEditInfo = "newEditInfo";
    private const string newInventory = "newInventory";
    private const string newResult = "newResult";
    private const string Raid = "Raid";
    private const string StatusIcon = "StatusIcon";
    private const string TowerOfDarkness = "TowerOfDarkness";

    private static bool _Init = false;

    public static void LoadAtlas()
    {
        Clear();

        _AtlasList.Add(AbyssRing, CreateAtlas(BUNDLELIST.ATLAS, "", AbyssRing));
        _AtlasList.Add(Card, CreateAtlas(BUNDLELIST.ATLAS, "", Card));
        _AtlasList.Add(Local, CreateAtlas(BUNDLELIST.ATLAS, "", Local));
        _AtlasList.Add(Main, CreateAtlas(BUNDLELIST.ATLAS, "", Main));
        _AtlasList.Add(newAchivement, CreateAtlas(BUNDLELIST.ATLAS, "", newAchivement));
        _AtlasList.Add(newBattle, CreateAtlas(BUNDLELIST.ATLAS, "", newBattle));
        _AtlasList.Add(newDungeon, CreateAtlas(BUNDLELIST.ATLAS, "", newDungeon));
        _AtlasList.Add(newEditInfo, CreateAtlas(BUNDLELIST.ATLAS, "", newEditInfo));
        _AtlasList.Add(newInventory, CreateAtlas(BUNDLELIST.ATLAS, "", newInventory));
        _AtlasList.Add(newResult, CreateAtlas(BUNDLELIST.ATLAS, "", newResult));
        _AtlasList.Add(Raid, CreateAtlas(BUNDLELIST.ATLAS, "", Raid));
        _AtlasList.Add(StatusIcon, CreateAtlas(BUNDLELIST.ATLAS, "", StatusIcon));
        _AtlasList.Add(TowerOfDarkness, CreateAtlas(BUNDLELIST.ATLAS, "", TowerOfDarkness));
    }

    private static void Clear()
    {
        if (_AtlasList == null)
        {
            _AtlasList = new Dictionary<string, UIAtlas>();
            return;
        }

        if (_AtlasList.Count < 1)
            return;

        _AtlasList.Clear();
    }

    private static UIAtlas CreateAtlas(BUNDLELIST bundleList, string aAtlasPath, string aPrefabName)
    {
#if ASSET_BUNDLE
        GameObject bundleObj = ResourceBundle.intance.ResourceObject(bundleList, aPrefabName, typeof(GameObject)) as GameObject;
        if (bundleObj == null)
        {
#if DEBUG_LOG
            Debug.LogError(string.Format(" === ERROR ATALS {0} PREFAB NAME = {1}", bundleList.ToString(), aPrefabName));
#endif
            return null;
        }

        UIAtlas atlas = bundleObj.GetComponent<UIAtlas>();
        if (atlas == null)
        {
#if DEBUG_LOG
            Debug.LogError("=== ATLAS NULL");
#endif
            return null;
        }

        return atlas;
#else
        return Resources.Load(ResourcePathInfo.intance.GetResourcePathInfo(bundleList, string.Format("{0}{1}", aAtlasPath, aPrefabName)), typeof(UIAtlas)) as UIAtlas;
#endif
    }

    /// <summary>
    /// 아틀라스 재연결
    /// </summary>
    private static void ReLinkAtlas(GameObject obj)
    {
        UIAtlas atlas = null;
        UISprite[] arr = obj.GetComponentsInChildren<UISprite>(true);
        foreach(UISprite sprite in arr)
        {
            if (sprite == null)
                continue;

            if (string.IsNullOrEmpty(sprite.spriteName))
            {
                if (sprite.atlas == null)
                {
#if DEBUG_LOG
                    Debug.LogError(string.Format("Parent : {0} UISprite : {1} Atlas is NULL", obj.name, sprite.name));
#endif
                }
                else if (string.IsNullOrEmpty(sprite.mAtlaseName))
                {
#if DEBUG_LOG
                    Debug.LogError(string.Format("UISprite : {0} AtlasName is NULL", sprite.name));
#endif
                }

                continue;
            }

            if(_AtlasList.TryGetValue(sprite.mAtlaseName, out atlas) == true)
            {
                sprite.atlas = atlas;
            }
        }
    }
#endif
#endregion

    //==================================================================================
    //
    // 에셋 번들 로드
    //
    //==================================================================================
    public static GameObject CreatePrefab(BUNDLELIST bundleList, Transform aParentTrs, string aPrefabName, SetTransformType TransformType = SetTransformType.OriginValue)
    {
#if ASSET_BUNDLE
        GameObject resultObj = ResourceBundle.intance.ResourceObject(bundleList, aPrefabName, typeof(GameObject)) as GameObject;
#else
        GameObject resultObj = Resources.Load(ResourcePathInfo.intance.GetResourcePathInfo(bundleList, aPrefabName)) as GameObject;        
#endif
        if (resultObj == null)
        {
#if DEBUG_LOG
            Debug.LogError(string.Format("error CreatePrefab --- bundleList : {0}", bundleList.ToString()));
            Debug.LogError(string.Format("error CreatePrefab --- aParentTrs : {0}", aParentTrs.name));
            Debug.LogError(string.Format("error CreatePrefab --- aPrefabName : {0}", aPrefabName));
#endif
            return null;
        }

        resultObj = InstantiateGameObject(resultObj, aParentTrs, TransformType, aPrefabName);

        return resultObj;
    }

    public static T CreatePrefab<T>(BUNDLELIST bundleList, Transform aParentTrs, string aPrefabName, SetTransformType TransformType = SetTransformType.OriginValue)
    {
        GameObject resultObj = CreatePrefab(bundleList, aParentTrs, aPrefabName, TransformType);
        if (resultObj == null)
            return default(T);

        return resultObj.GetComponent<T>();
    }

    // Instantiate 하지 않은 크리쳐로딩.
    public static GameObject CreatureDefaultLoadPrefab(string aPrefabName)
    {
#if ASSET_BUNDLE
        GameObject resultObj = ResourceBundle.intance.ResourceObject(UtilFunc.GetCreatureBundleName(aPrefabName), aPrefabName, typeof(GameObject)) as GameObject;
#else
        GameObject resultObj = Resources.Load(UtilFunc.GetCreatureResName(aPrefabName)) as GameObject;
#endif
        if (resultObj == null)
        {
#if DEBUG_LOG
            Debug.LogError(string.Format("<color=red>UIResourceMgr->CreatureDefaultLoadPrefab 크리쳐 모델 로드 실패 모델 리소스 {0} 가 존재하지 않는다.</color>", aPrefabName));
#endif
            return null;
        }

#if ASSET_BUNDLE
        ReLinkAtlas(resultObj);
#endif

        return resultObj;
    }

    // Instantiate 한 크리쳐 모델 로드하고 리턴.
    public static GameObject CreateCreatureModel(string szResourceName, string changeName, Transform parent, SetTransformType TransformType)
    {
#if ASSET_BUNDLE
        GameObject OriginObj = ResourceBundle.intance.ResourceObject(UtilFunc.GetCreatureBundleName(szResourceName), szResourceName, typeof(GameObject)) as GameObject;
#else
        GameObject OriginObj = Resources.Load(UtilFunc.GetCreatureResName(szResourceName)) as GameObject;
#endif
        if (OriginObj == null)
        {
#if DEBUG_LOG
            Debug.LogError(string.Format("<color=red>UIResourceMgr->LoadInstantiateCreatureModel 크리쳐 모델 로드 실패 모델 리소스 {0} 가 존재하지 않는다.</color>", szResourceName));
#endif
            return null;
        }

        if (string.IsNullOrEmpty(changeName) == true)
            changeName = OriginObj.name;

        GameObject resultObj = InstantiateGameObject(OriginObj, parent, TransformType, changeName);

        return resultObj;
    }

    // Instantiate 하지 않은 프리팹로딩.
    public static GameObject CreateDefaultPrefab(BUNDLELIST bundleList, string aPrefabName)
    {
#if ASSET_BUNDLE
        GameObject resultObj = ResourceBundle.intance.ResourceObject(bundleList, aPrefabName, typeof(GameObject)) as GameObject;
#else
        GameObject resultObj = Resources.Load(ResourcePathInfo.intance.GetResourcePathInfo(bundleList, aPrefabName)) as GameObject;
#endif
        if (resultObj == null)
        {
#if DEBUG_LOG
            Debug.LogError(string.Format(" === ERR CreateDefaultPrefab BUNDLE NAME {0} PREFAB NAME{1}", bundleList.ToString(), aPrefabName));
#endif
            return null;
        }

#if ASSET_BUNDLE
        ReLinkAtlas(resultObj);
#endif

        return resultObj;
    }

    // 스프라이트 로딩.
    public static Sprite CreateSprite(BUNDLELIST bundleList, string aSPriteName)
    {
#if ASSET_BUNDLE
        Sprite resultSpr = ResourceBundle.intance.ResourceObject(bundleList, aSPriteName, typeof(Sprite)) as Sprite;
#else
        Sprite resultSpr = Resources.Load(ResourcePathInfo.intance.GetResourcePathInfo(bundleList, aSPriteName), typeof(Sprite)) as Sprite;
#endif

        return resultSpr;
    }

    // 스프라이트 로딩.
//    public static Sprite CreateSprite2(BUNDLELIST bundleList, BUNDLELIST bundleList2, string aSPriteName)
//    {
//        Sprite resultSpr = null;
//#if ASSET_BUNDLE
//        resultSpr = ResourceBundle.intance.ResourceObject(bundleList, aSPriteName, typeof(Sprite)) as Sprite;
//#else
//        resultSpr = Resources.Load(ResourcePathInfo.intance.GetResourcePathInfo(bundleList, aSPriteName), typeof(Sprite)) as Sprite;
//#endif

//        // 2차적으로 스프라이트 찾음.
//        if(resultSpr == null)
//        {
//#if ASSET_BUNDLE
//            resultSpr = ResourceBundle.intance.ResourceObject(bundleList2, aSPriteName, typeof(Sprite)) as Sprite;
//#else
//            resultSpr = Resources.Load(ResourcePathInfo.intance.GetResourcePathInfo(bundleList2, aSPriteName), typeof(Sprite)) as Sprite;
//#endif
//        }

//        if (resultSpr == null)
//        {
#if DEBUG_LOG
//            Debug.LogError(string.Format("error CreateSprite --- BUNDLE NAME : {0}", bundleList.ToString()));
#endif
#if DEBUG_LOG
//            Debug.LogError(string.Format("error CreateSprite --- SPRITE NAME : {0}", aSPriteName));
#endif
//            return null;
//        }

//        return resultSpr;
//    }

    /// <summary>
    /// 빌드에 포함되는 리소스들로드
    /// </summary>
    public static GameObject InstantiatePrefab(string path, Transform anchor)
    {
        GameObject tempObject = Resources.Load(path) as GameObject;
        if(tempObject == null)
        {
#if DEBUG_LOG
            Debug.LogError(string.Format(" === No Game Object - Path : {0} ===", path));
#endif
            return null;
        }

        tempObject = InstantiateGameObject(tempObject, anchor, SetTransformType.Default);
        tempObject.SetActive(true);

#if ASSET_BUNDLE
        ReLinkAtlas(tempObject);
#endif

        return tempObject;
    }

    // <summary>
    // 트랜스폼 디폴트 타입은 포지션 0, 스케일 1     
    // </summary>
    public static GameObject InstantiateGameObject(GameObject OriginObj, Transform trParent = null, SetTransformType TransType = SetTransformType.IgnoreValue, string strName = "")
    {
        GameObject resultObj = GameObject.Instantiate(OriginObj) as GameObject;
        if(resultObj == null)
        {
#if DEBUG_LOG
            Debug.LogError(string.Format("error InstantiateGameObject --- OBJECT NAME : {0}", OriginObj.ToString()));
#endif
#if DEBUG_LOG
            Debug.LogError(string.Format("error InstantiateGameObject --- PARENT : {0}", trParent.name));
#endif
#if DEBUG_LOG
            Debug.LogError(string.Format("error InstantiateGameObject --- PREFAB RENAME : {0}", strName));
#endif
            return null;
        }

        if (string.IsNullOrEmpty(strName) == false)
        {
            resultObj.name = strName;
        }

        UtilTransform.AttachTransForm(trParent, resultObj.transform, TransType);

#if ASSET_BUNDLE
        ReLinkAtlas(resultObj);
#endif

        return resultObj;
    }

    public static T InstantiateGameObject<T>(T OriginObj, Transform trParent = null, SetTransformType TransType = SetTransformType.IgnoreValue, string strName = "") where T : Component
    {
        GameObject resultObj = InstantiateGameObject(OriginObj.gameObject, trParent, TransType, strName);
        if (resultObj == null)
            return default(T);

        return resultObj.GetComponent<T>();
    }
}
