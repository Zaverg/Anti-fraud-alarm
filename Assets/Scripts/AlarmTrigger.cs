using System;
using System.Collections;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private float _time;

    public event Func<bool, bool> Working;
    private bool _isAlarmWork = false;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Thief thief))
        {
            _isAlarmWork = !_isAlarmWork;
            StartCoroutine(Wait(thief));
        }
    }

    private IEnumerator Wait(Thief thief)
    {
        bool isWork = true;

        while (isWork)
        {
            yield return new WaitUntil(() => (bool)Working?.Invoke(_isAlarmWork));
            thief.UpdateState();
            isWork = false;
        }
        
    }
}
