using UnityEngine;

public class PixelToUnitConverter : MonoBehaviour
{

    public Vector2 WorldUnitsInCamera;
    public Vector2 WorldToPixelAmount;

    public GameObject Camera;

    void Update()
    {
        WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
        WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
    }

    public void SetCamera(GameObject camera)
    {
        Camera = camera;
    }

    public GameObject GetCamera()
    {
        return Camera;
    }

    public void ForceConversionFromCamera(Camera camera)
    {
        WorldUnitsInCamera.y = camera.orthographicSize * 2;
        WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
    }
}
