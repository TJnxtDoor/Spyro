using UnityEngine;
using System.Collections.Generic;

public enum WaypointType
{
    Custom,
    Objective,
    Item,
    Enemy,
    Secret,
    SavePoint,
    Portal
}

public class WayPoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string waypointName;
    [SerializeField] private WaypointType waypointType;
    [SerializeField] private bool showOnMinimap = true;
    [SerializeField] private bool showOnScreen = true;
    [SerializeField] private bool showOnFullscreenMap = true;

    [Header("Visuals")]
    [SerializeField] private Color waypointColor = Color.yellow;
    [SerializeField] private float iconSize = 1f;
    [SerializeField] private float screenIconSize = 30f;
    [SerializeField] private float minimapIconSize = 20f;

    [Header("Range")]
    [SerializeField] private float visibleRange = 50f;
    [SerializeField] private float screenVisibleRange = 100f;

    private bool isActive = true;
    private bool isReached = false;
    private static WaypointManager waypointManager;

    void Start()
    {
        if (waypointManager == null)
            waypointManager = FindObjectOfType<WaypointManager>();

        waypointManager?.RegisterWaypoint(this);
        SetWaypointColor();
    }

    void OnDestroy()
    {
        waypointManager?.UnregisterWaypoint(this);
    }

    private void SetWaypointColor()
    {
        waypointType = waypointType switch
        {
            WaypointType.Custom => WaypointType.Custom,
            WaypointType.Objective => WaypointType.Objective,
            WaypointType.Item => WaypointType.Item,
            WaypointType.Enemy => WaypointType.Enemy,
            WaypointType.Secret => WaypointType.Secret,
            WaypointType.SavePoint => WaypointType.SavePoint,
            WaypointType.Portal => WaypointType.Portal,
            _ => waypointType
        };

        waypointColor = waypointType switch
        {
            WaypointType.Custom => Color.yellow,
            WaypointType.Objective => Color.green,
            WaypointType.Item => Color.cyan,
            WaypointType.Enemy => Color.red,
            WaypointType.Secret => Color.magenta,
            WaypointType.SavePoint => Color.blue,
            WaypointType.Portal => new Color(1f, 0.5f, 0f),
            _ => Color.white
        };
    }

    public bool IsVisibleOnMinimap() => showOnMinimap && isActive && GetPlayerDistance() <= visibleRange;
    public bool IsVisibleOnScreen() => showOnScreen && isActive && GetPlayerDistance() <= screenVisibleRange;

    private float GetPlayerDistance()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        return player != null ? Vector3.Distance(transform.position, player.transform.position) : float.MaxValue;
    }

    public void MarkAsReached() { isReached = true; isActive = false; }
    public void Activate() => isActive = true;
    public void Deactivate() => isActive = false;

    public string GetWaypointName() => waypointName;
    public WaypointType GetWaypointType() => waypointType;
    public Color GetWaypointColor() => waypointColor;
    public float GetIconSize() => iconSize;
    public float GetScreenIconSize() => screenIconSize;
    public float GetMinimapIconSize() => minimapIconSize;
    public bool IsActive() => isActive;
    public bool IsReached() => isReached;
    public bool ShowOnFullscreenMap() => showOnFullscreenMap;
}

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    private readonly List<WayPoint> activeWaypoints = new();
    private readonly List<WayPoint> visibleWaypoints = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update() => UpdateVisibleWaypoints();

    public void RegisterWaypoint(WayPoint waypoint)
    {
        if (!activeWaypoints.Contains(waypoint))
            activeWaypoints.Add(waypoint);
    }

    public void UnregisterWaypoint(WayPoint waypoint)
    {
        activeWaypoints.Remove(waypoint);
        visibleWaypoints.Remove(waypoint);
    }

    private void UpdateVisibleWaypoints()
    {
        visibleWaypoints.Clear();
        visibleWaypoints.AddRange(activeWaypoints.FindAll(w => w.IsVisibleOnMinimap() || w.IsVisibleOnScreen()));
    }

    public List<WayPoint> GetVisibleWaypoints() => visibleWaypoints;
    public List<WayPoint> GetMinimapWaypoints() => visibleWaypoints.FindAll(w => w.IsVisibleOnMinimap());
    public List<WayPoint> GetScreenWaypoints() => visibleWaypoints.FindAll(w => w.IsVisibleOnScreen());
    public List<WayPoint> GetWaypointsByType(WaypointType type) => activeWaypoints.FindAll(w => w.GetWaypointType() == type && w.IsActive());
    public void ClearAllWaypoints() { activeWaypoints.Clear(); visibleWaypoints.Clear(); }
}

public class WaypointIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform iconTransform;
    [SerializeField] private UnityEngine.UI.Image iconImage;
    [SerializeField] private Camera mainCamera;

    private WayPoint currentWaypoint;

    public void Initialize(WayPoint waypoint)
    {
        currentWaypoint = waypoint;
        mainCamera ??= Camera.main;
    }

    void Update()
    {
        if (currentWaypoint == null || !currentWaypoint.IsActive())
        {
            gameObject.SetActive(false);
            return;
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        var screenPosition = mainCamera.WorldToScreenPoint(currentWaypoint.transform.position);
        bool isOnScreen = screenPosition.z > 0 && screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;

        if (isOnScreen)
            iconTransform.position = screenPosition;
        else
            PositionOnScreenEdge(screenPosition);

        iconImage.color = currentWaypoint.GetWaypointColor();
    }

    private void PositionOnScreenEdge(Vector3 screenPosition)
    {
        var screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        var direction = (new Vector2(screenPosition.x, screenPosition.y) - screenCenter).normalized;

        float edgeX = screenCenter.x;
        float edgeY = screenCenter.y;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            edgeX = direction.x > 0 ? Screen.width - 50f : 50f;
            edgeY = screenCenter.y + (edgeX - screenCenter.x) * (direction.y / Mathf.Abs(direction.x));
        }
        else
        {
            edgeY = direction.y > 0 ? Screen.height - 50f : 50f;
            edgeX = screenCenter.x + (edgeY - screenCenter.y) * (direction.x / Mathf.Abs(direction.y));
        }

        iconTransform.position = new Vector3(Mathf.Clamp(edgeX, 50f, Screen.width - 50f), Mathf.Clamp(edgeY, 50f, Screen.height - 50f), 0);
        gameObject.SetActive(true);
    }
} 