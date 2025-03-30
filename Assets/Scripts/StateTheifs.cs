using System.Collections.Generic;
using UnityEngine;

public class StateTheifs : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private AlarmTrigger _alarmTriger;

    private List<Thief> _thiefs = new List<Thief>();
    private bool _isLastState;

    private void OnEnable()
    {
        _alarmTriger.Entered += SetThief;
    }

    private void OnDisable()
    {
        _alarmTriger.Entered -= SetThief;
    }

    public void Update()
    {
        if (_alarm.IsIncreases != _isLastState)
            UpdateStates();
    }

    private void UpdateStates()
    {
        foreach (Thief thief in _thiefs)
            thief.UpdateState();

        _isLastState = _alarm.IsIncreases;
    }

    private void SetThief(Thief thief)
    {
        if(_thiefs.Contains(thief) == false)
            _thiefs.Add(thief);
    }
}
