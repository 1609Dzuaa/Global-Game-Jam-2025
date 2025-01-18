using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetController : MonoBehaviour
{
    Animator _anim;
    const string LEAK_TRIGGER_PARAM = "Leak";

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        EventsManager.Subcribe(EventID.OnStartLeak, StartLeak);
    }

    private void OnDestroy()
    {
        EventsManager.Unsubcribe(EventID.OnStartLeak, StartLeak);
    }

    private void StartLeak(object obj) => _anim.SetTrigger(LEAK_TRIGGER_PARAM);
}
