/// <summary>
/// 游戏物体对象池配置
/// </summary>
public static class GameObjectPoolConfiguration
{
    public static int DefaultCapacity = 11; //默认容量
    public static bool EnableThreadSafe = false; //是否开启线程安全（默认关闭，高并发场景可开启）
    public static bool EnableCheckIdle = true; //是否开启检测空闲
    public static int MaxIdleTime = 1800; //最长的空闲时间（默认30分钟）
}