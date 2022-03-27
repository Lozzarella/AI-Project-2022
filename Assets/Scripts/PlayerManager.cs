using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    [SerializeField] protected CanvasGroup _buttonGroup;
    protected AIManager _aiManager;
    protected bool _isHealOverTimeRunning = false;
    protected int healTimesRemaining = 0;

    protected override void Start()
    {
        base.Start();
        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)
        {
            Debug.LogError("AIManager no found");
        }
    }

    public override void TakeTurn()
    {
        _buttonGroup.interactable = true;
    }

    public void EndTurn()
    {
        _buttonGroup.interactable = false;
        _aiManager.TakeTurn();  
    }

    public void EatBerries()
    {
        Heal(20f);
        StartCoroutine(HealOverTime(3,1f));
    }

    
    private IEnumerator HealOverTime(int times, float waitTime)
    {
        healTimesRemaining += times;

        if (_isHealOverTimeRunning == false)
        {
            _isHealOverTimeRunning = true;


            while (healTimesRemaining > 0)
            {
                Heal(10f);
                yield return new WaitForSecondsRealtime(waitTime);
                healTimesRemaining--;
            }
            _isHealOverTimeRunning = false;
        }
    }
    public void SelfDestruct()
    {
        DealDamage(_maxHealth);
        _aiManager.DealDamage(80f);
        EndTurn();
    }

    public void VineWhip()
    {
        _aiManager.DealDamage(30f);
        EndTurn();
    }

    public void FlameWheel()
    {
        _aiManager.DealDamage(50f);
        EndTurn();
    }
}
