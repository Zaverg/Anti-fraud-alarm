using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _step;

    private bool _isIncreasing = true;
    private bool _isWork;

    public bool IsIncreases => _isIncreasing;

    private void OnEnable()
    {
        _audioSource.volume = _minVolume;
        _alarmTrigger.Working += WorkAlarm;
    }

    private void OnDisable()
    {
        _alarmTrigger.Working -= WorkAlarm;
    }

    private void WorkAlarm()
    {
        if (_isWork == false)
            StartCoroutine(StartAlarm());
    }

    private bool IsVolumeReached(float volume)
    {
        float error = 0.001f;

        if (Mathf.Abs(_audioSource.volume - volume) < error)
            return true;

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _step);

        return false;
    }

    private IEnumerator StartAlarm()
    {
        _isWork = true;

        float volume = _isIncreasing ? _maxVolume : _minVolume;

        while (_isWork)
        {
            yield return new WaitUntil(() => IsVolumeReached(volume));

            _isIncreasing = !_isIncreasing;
            _isWork = false;
        }
    }
}

