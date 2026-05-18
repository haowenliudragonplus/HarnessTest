using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;

public class Boot : MonoBehaviour
{
    private Procedure[] procedureList = new Procedure[]
    {
        new ProcedureInitialize(new ProcedureContext(0)),
        new ProcedureFetchServerVersion(new ProcedureContext(10)),
        new ProcedureYooAssetInit(new ProcedureContext(20)),
        new ProcedureUpdateVersion(new ProcedureContext(30)),
        new ProcedureUpdateManifest(new ProcedureContext(40)),
        new ProcedureDownloadFiles(new ProcedureContext(50)),
        new ProcedureClearCache(new ProcedureContext(80)),
        new ProcedureLoadDll(new ProcedureContext(90)),
        new ProcedureDone(new ProcedureContext(100)),
    };

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private async void Start()
    {
        // 创建启动界面
        await CreateBootView();

        // 依次执行每个启动步骤
        for (var i = 0; i < procedureList.Length; i++)
        {
            await procedureList[i].ExecuteProcedure();
        }
    }

    private async UniTask CreateBootView()
    {
        var bootView = Resources.Load<GameObject>(Const_Boot.PrefabPath_BootView);
        Instantiate(bootView, GameObject.Find(Const_Boot.ScenePath_UICanvas).transform);
        await UIView_Boot.Ins.ShowSplash();
    }

    #region 热更工程调主工程的MonoBehaviour生命周期

    private float lastSecondsUpdateTime;
    private float lastHalfSecondsUpdateTime;

    private void FixedUpdate()
    {
        Loader.OnFixedUpdate?.Invoke();
    }

    private void Update()
    {
        Loader.OnUpdate?.Invoke();
        lastSecondsUpdateTime += Time.deltaTime;
        lastHalfSecondsUpdateTime += Time.deltaTime;
        if (lastSecondsUpdateTime >= 1f)
        {
            Loader.OnSecondsUpdate?.Invoke();
            lastSecondsUpdateTime = 0;
        }
        if (lastHalfSecondsUpdateTime >= 0.5f)
        {
            Loader.OnHalfSecondsUpdate?.Invoke();
            lastHalfSecondsUpdateTime = 0;
        }
    }

    private void LateUpdate()
    {
        Loader.OnLateUpdate?.Invoke();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        Loader.OnApplicatePause?.Invoke(pauseStatus);
    }

    private void OnApplicationQuit()
    {
        Loader.OnApplicateQuit?.Invoke();
        LogService.Dispose();
    }

    #endregion 热更工程调主工程的MonoBehaviour生命周期
}