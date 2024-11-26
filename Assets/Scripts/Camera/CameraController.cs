using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Camera _camera;
    [SerializeField] private List<Transform> players;
    [SerializeField] private List<Transform> arena;
    [SerializeField] private float zOffset;
    [SerializeField] private Bounds cameraLimits;

    void Start()
    {
        _camera = GetComponent<Camera>();
        zOffset = zOffset == default ? -5 : zOffset;
        cameraLimits = getCameraLimits();
        _camera.transform.position = getCameraPos();
    }

    private Bounds getCameraLimits()
    {
        var arenaBounds = new Bounds(arena[0].position, Vector3.zero);
        foreach (var wall in arena)
            arenaBounds.Encapsulate(wall.position);
        arenaBounds.size -= new Vector3(2f * 16f / 9f, 2f, 0) * _camera.orthographicSize;
        arenaBounds.size -= new Vector3(0, 0, 2f) * zOffset;
        return arenaBounds;
    }

    void LateUpdate()
    {
        Vector3 cameraPos = getCameraPosInLimits(getCameraPos());
        _camera.transform.position = cameraPos;
    }

    private Vector3 getCameraPos()
    {
        var playerBound = new Bounds(players[0].position, Vector3.zero);
        foreach (var player in players)
            playerBound.Encapsulate(player.position);
        return playerBound.center;
    }

    private Vector3 getCameraPosInLimits(Vector3 cameraPos)
    {
        var prevCameraPos = _camera.transform.position;
        if (cameraLimits.Contains(new Vector3(0, cameraPos.y)))
            prevCameraPos.y = cameraPos.y;
        if (cameraLimits.Contains(new Vector3(cameraPos.x, 0)))
            prevCameraPos.x = cameraPos.x;
        prevCameraPos.z = zOffset;
        return prevCameraPos;
    }
}
