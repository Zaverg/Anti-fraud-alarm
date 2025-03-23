using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _step;

    private void OnEnable()
    {
        _audioSource.volume = _minVolume;
        _alarmTrigger.Working += WorkAlarm;
      
    }

    private void OnDisable()
    {
        _alarmTrigger.Working -= WorkAlarm;
    }

    private bool WorkAlarm(bool isIncreases)
    {
        float volume;

        if (isIncreases)
            volume = _maxVolume;
        else
            volume = _minVolume;

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _step);

        if (_audioSource.volume == _maxVolume || _audioSource.volume == _minVolume)
            return true;

        return false;
    }
}

