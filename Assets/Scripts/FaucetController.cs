using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetController : MonoBehaviour
{
    Animator _anim;
    const string LEAK_TRIGGER_PARAM = "Leak";
    [SerializeField] private List<Transform> spawnPositions;
    private Coroutine _coroutine;

    void Start()
    {
        _coroutine = StartCoroutine(LeakWater());





        // _anim = GetComponent<Animator>();
        // EventsManager.Subcribe(EventID.OnStartLeak, StartLeak);
    }

    private void OnDestroy()
    {
        // EventsManager.Unsubcribe(EventID.OnStartLeak, StartLeak);
    }

    private IEnumerator LeakWater()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            foreach (var trans in spawnPositions)
            {
                Pool.Instante.Spawn("Box", trans.position);
                yield return null;
            }
        }
    }

    private void StartLeak(object obj)
    {
        _anim.SetTrigger(LEAK_TRIGGER_PARAM);
    }
}
