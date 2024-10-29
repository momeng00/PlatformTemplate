using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    private Volume volume;
    private Bloom bloom;
    public float waveValue;
    public float waveSpeed;
    public float variable;
    private float MAX;
    private float MIN;
    private float _curValue;
    public float curValue
    {
        
        get => _curValue;
        set
        {
            if (_curValue >= MAX)
            {
                waveValue = -Mathf.Abs(waveValue);
            } 
            if(_curValue <= MIN)
            {
                waveValue = Mathf.Abs(waveValue);
            }
            _curValue = value;
            bloom.intensity.value = _curValue;
        }
    }
    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        curValue = bloom.intensity.value;
        MIN = bloom.intensity.value - variable;
        MAX = bloom.intensity.value + variable;
        Wave();
    }
    private void Wave()
    {
        curValue += waveValue;
        Invoke("Wave", waveSpeed);
    }


}
