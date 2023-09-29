using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [SerializeField] private float bobFrequency = 1.5f;
    [SerializeField] private float bobAmplitude = 0.2f;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private float timer = 0f;
    private bool isShaking = false;
    private float shakeTimer = 0f;

    void Start(){
        originalPosition = transform.localPosition;
    }

    void Update(){
        //Math bobbing system
        
        timer += Time.deltaTime;

        float xOffset = Mathf.Sin(timer * bobFrequency) * bobAmplitude;
        float yOffset = Mathf.Cos(timer * bobFrequency * 2f) * bobAmplitude;

        Vector3 bobOffset = new Vector3(xOffset, yOffset, 0f);

        // Camera shake
        if (isShaking){
            if (shakeTimer < shakeDuration){
                Vector2 randomShake = Random.insideUnitCircle * shakeMagnitude;
                transform.localPosition = originalPosition + bobOffset + new Vector3(randomShake.x, randomShake.y, 0f);
                shakeTimer += Time.deltaTime;
            }
            else{
                isShaking = false;
                shakeTimer = 0f;
            }
        }
        else{
            transform.localPosition = originalPosition + bobOffset;
        }
    }

    public void StartShake(){
        isShaking = true;
    }
}
