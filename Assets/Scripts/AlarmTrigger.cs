using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private float _time;

    public event Action Working;
    public event Action<Thief> Entered;
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
            Entered?.Invoke(thief);
            Working?.Invoke();
        }
    }
}
