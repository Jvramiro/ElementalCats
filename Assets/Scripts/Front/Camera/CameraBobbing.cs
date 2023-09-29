using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [SerializeField] private float bobFrequency = 1.5f;
    [SerializeField] private float bobAmplitude = 0.2f;

    private Vector3 originalPosition;
    private float timer = 0f;

    void Start(){
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        //Math bobbing system
        timer += Time.deltaTime;

        float xOffset = Mathf.Sin(timer * bobFrequency) * bobAmplitude;
        float yOffset = Mathf.Cos(timer * bobFrequency * 2f) * bobAmplitude;

        Vector3 bobOffset = new Vector3(xOffset, yOffset, 0f);
        transform.localPosition = originalPosition + bobOffset;
    }
}
