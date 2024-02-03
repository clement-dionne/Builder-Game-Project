using UnityEngine;
using UnityEngine.UI;

public class MapRotation : MonoBehaviour
{
    #region Unity Public
    public float RotationSensibility = 1f;
    public float MooveSensibility = 0.5f;
    public GameObject Camera;
    public GameObject MenuPanel;
    public GameObject SettingsPanel;
    #endregion

    #region Unity Private
    private Vector3 euler;
    #endregion

    void Start()
    {
        euler = transform.localEulerAngles;
    }

    void Update()
    {
        if (!MenuPanel.activeInHierarchy && !SettingsPanel.activeInHierarchy)
        {
            if (Input.GetMouseButton(2))
            {
                euler.y -= Input.GetAxis("Mouse X") * RotationSensibility;
                transform.localEulerAngles = euler;
            }

            if (Input.GetMouseButton(1))
            {
                transform.position = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * MooveSensibility,
                                                transform.position.y,
                                                transform.position.z + Input.GetAxis("Mouse Y") * MooveSensibility);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                int a = Mathf.FloorToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
                if (Camera.transform.position.y - a >= 3 && Camera.transform.position.y - a <= 30)
                {
                    Camera.transform.position = new Vector3(Camera.transform.position.x,
                                                            Camera.transform.position.y - (1f * a),
                                                            Camera.transform.position.z - (-1f * a));
                }
            }
        }
    }
}
