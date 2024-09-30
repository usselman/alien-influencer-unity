using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum BuildingState
    {
        Untouched,
        StartDamaged,
        IsDamaged,
        StartDestroyed,
        IsDestroyed
    }
    public BuildingState CurrentState = BuildingState.Untouched;
    public int maxDamage = 100;
    public int currentDamage = 0;

    public GameObject buildingStanding, buildingDestroyed;
    public ParticleSystem damagedParticles, destroyedParticles;
    public Animator damageBarAnimator;
    public ProgressBarPro damageProgressBar;

    public void Start()
    {
        buildingStanding.SetActive(true);
        buildingDestroyed.SetActive(false);
        damagedParticles.gameObject.SetActive(false);
        destroyedParticles.gameObject.SetActive(false);
        damageBarAnimator.gameObject.SetActive(false);

    }
    void Update()
    {
        switch (CurrentState)
        {
            case BuildingState.Untouched:
                Untouched();
                break;
            case BuildingState.StartDamaged:
                StartDamaged();
                break;
            case BuildingState.IsDamaged:
                IsDamaged();
                break;
            case BuildingState.StartDestroyed:
                StartDestroyed();
                break;
            case BuildingState.IsDestroyed:
                IsDestroyed();
                break;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartDamaged();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddDamage(10);
        }
    }
    void Untouched()
    {

    }
    void StartDamaged()
    { 
        damageBarAnimator.gameObject.SetActive(true);
        damageBarAnimator.SetTrigger("Open");

        damagedParticles.gameObject.SetActive(true);
        damagedParticles.Play();
        damageProgressBar.SetValue(0);

        CurrentState = BuildingState.IsDamaged;
    }
    void IsDamaged()
    {
    }
    void StartDestroyed()
    {
        damageBarAnimator.SetTrigger("Close");
        damagedParticles.loop = false;
        buildingStanding.SetActive(false);

        destroyedParticles.gameObject.SetActive(true);
        destroyedParticles.Play();
        StartCoroutine(StallDisableStandingBuildingObjects());

        CurrentState = BuildingState.IsDestroyed;
    }
    void IsDestroyed()
    {
    }
    IEnumerator StallDisableStandingBuildingObjects()
    {
        yield return new WaitForSeconds(1f);
        damagedParticles.gameObject.SetActive(false);
        damageBarAnimator.gameObject.SetActive(false);
    }

    public void AddDamage(int amount)
    {
        currentDamage += amount;
        damageProgressBar.SetValue(currentDamage, maxDamage, false);
        if (currentDamage >= maxDamage)
        {
            StartDestroyed();
        }
    }
}
