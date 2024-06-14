using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public AudioSource audioSource;
    public Button upgradeButton;
    private bool isButtonClickable = true;
    private GameObject playerObject;

    float upgrades = 1;

    public GameObject levelUpCanvas;
    private List<int> availableElements = new List<int> { 1, 2, 3, 4 };

    private void Start()
    {
        if (upgradeButton == null)
        {
            upgradeButton = GetComponent<Button>();
        }

        // Find the GameObject with the "Player" tag
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnButtonClick()
    {
        if (isButtonClickable)
        {
            audioSource.clip = Resources.Load<AudioClip>("Sounds/click");
            audioSource.Play();
            isButtonClickable = false;

            // Increase upgrades count
            upgrades += 1;

            // Hangi butona basıldığını kontrol etmek için butonun ismini kullanabiliriz
            if (upgradeButton.name == "HP")
            {
                UpgradeHP(playerObject);
            }
            else if (upgradeButton.name == "Attack Damage")
            {
                UpgradeAttackDamage(playerObject);
            }
            else if (upgradeButton.name == "Elemental Buff")
            {
                UpgradeElementalBuff();
            }

            StartCoroutine(CallUpgrade(0.15f));
        }
    }

    public void UpgradeHP(GameObject playerObject)
    {
        // HP upgrade logic
        if (playerObject != null)
        {
            Damagable damagable = playerObject.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.MaxHealth += 20;
                damagable.Health += 20;
            }
            Debug.Log("Health Increased, new hp and level: " + damagable.Health + " " + playerObject.GetComponent<PlayerExperience>().level);
        }
    }

    public void UpgradeAttackDamage(GameObject playerObject)
    {
        // Attack damage upgrade logic
        if (playerObject != null)
        {
            // Increase AttackDamage for specified children
            IncreaseAttackDamage(playerObject, "SwordAttack1");
            IncreaseAttackDamage(playerObject, "SwordAttack2");
            IncreaseAttackDamage(playerObject, "SwordAttack3");
            IncreaseAttackDamage(playerObject, "AirAttack");
        }
    }

    private void IncreaseAttackDamage(GameObject playerObject, string childName)
    {
        // Find the child game object by name and get its Attack component
        Transform childTransform = playerObject.transform.Find(childName);
        if (childTransform != null)
        {
            Attack attack = childTransform.GetComponent<Attack>();
            if (attack != null)
            {
                attack.attackDamage += attack.attackDamage * 20 / 100;
            }
        }
    }

    public void UpgradeElementalBuff()
    {
        // Elemental strike upgrade logic
        if (availableElements.Count > 0)
        {
            int randomIndex = Random.Range(0, availableElements.Count);
            int chosenElement = availableElements[randomIndex];

            switch (chosenElement)
            {
                case 1:
                    ApplyFireElement();
                    break;
                case 2:
                    ApplyWaterElement();
                    break;
                case 3:
                    ApplyEarthElement();
                    break;
                case 4:
                    ApplyAirElement();
                    break;
            }

            // Remove the chosen element from the list so it can't be selected again
            availableElements.RemoveAt(randomIndex);
        }
        else
        {
            EnhanceElementalBuffs();
        }
    }

    private void ApplyFireElement()
    {
        // Apply fire element logic
        ApplyFireEffectToChild("SwordAttack1");
        ApplyFireEffectToChild("SwordAttack2");
        ApplyFireEffectToChild("SwordAttack3");
        ApplyFireEffectToChild("AirAttack");
        Debug.Log("Fire element applied!");
    }

    private void ApplyFireEffectToChild(string childName)
    {
        Transform childTransform = playerObject.transform.Find(childName);
        if (childTransform != null)
        {
            Attack attack = childTransform.GetComponent<Attack>();
            if (attack != null)
            {
                attack.applyFireEffect = true;
            }
        }
    }

    private void ApplyWaterElement()
    {
        // Apply water element logic
        ApplyWaterEffectToChild("SwordAttack1");
        ApplyWaterEffectToChild("SwordAttack2");
        ApplyWaterEffectToChild("SwordAttack3");
        ApplyWaterEffectToChild("AirAttack");
        Debug.Log("Water element applied!");
    }

    private void ApplyWaterEffectToChild(string childName)
    {
        Transform childTransform = playerObject.transform.Find(childName);
        if (childTransform != null)
        {
            Attack attack = childTransform.GetComponent<Attack>();
            if (attack != null)
            {
                attack.applyWaterEffect = true;
            }
        }
    }

    private void ApplyEarthElement()
    {
        // Apply earth element logic
        Damagable damagable = playerObject.GetComponent<Damagable>();
        GameObject earthBuff = playerObject.transform.Find("Earth Buff").gameObject;
        if (damagable != null)
        {
            earthBuff.SetActive(true);
            damagable.Armor += 5;
            Debug.Log("Earth element applied! New armor: " + damagable.Armor);
        }
    }

    private void ApplyAirElement()
    {
        // Apply air element logic
        KnightController knightController = playerObject.GetComponent<KnightController>();
        GameObject windBuff = playerObject.transform.Find("Wind Buff").gameObject;
        if (knightController != null)
        {
            windBuff.SetActive(true);
            knightController.walkSpeed *= 1.2f; // Increase walkSpeed by 20%
            Debug.Log("Air element applied! New walkSpeed: " + knightController.walkSpeed);
        }
    }

    private void EnhanceElementalBuffs()
    {
        // Enhance the effects of already acquired elements
        EnhanceFireElement();
        EnhanceWaterElement();
        EnhanceEarthElement();
        EnhanceAirElement();
        Debug.Log("Elemental buffs enhanced!");
    }

    private void EnhanceFireElement()
    {
        // Enhance fire element logic
        EnhanceFireEffectForChild("SwordAttack1");
        EnhanceFireEffectForChild("SwordAttack2");
        EnhanceFireEffectForChild("SwordAttack3");
        EnhanceFireEffectForChild("AirAttack");
    }

    private void EnhanceFireEffectForChild(string childName)
    {
        Transform childTransform = playerObject.transform.Find(childName);
        if (childTransform != null)
        {
            Attack attack = childTransform.GetComponent<Attack>();
            if (attack != null)
            {
                attack.burnDamage += 2; // Increase burn damage
            }
        }
    }

    private void EnhanceWaterElement()
    {
        // Enhance water element logic
        EnhanceWaterEffectForChild("SwordAttack1");
        EnhanceWaterEffectForChild("SwordAttack2");
        EnhanceWaterEffectForChild("SwordAttack3");
        EnhanceWaterEffectForChild("AirAttack");
    }

    private void EnhanceWaterEffectForChild(string childName)
    {
        Transform childTransform = playerObject.transform.Find(childName);
        if (childTransform != null)
        {
            Attack attack = childTransform.GetComponent<Attack>();
            if (attack != null)
            {
                attack.waterBuffHeal += 1; // Increase water buff heal
            }
        }
    }

    private void EnhanceEarthElement()
    {
        // Enhance earth element logic
        Damagable damagable = playerObject.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Armor += 2; // Increase armor
        }
    }

    private void EnhanceAirElement()
    {
        // Enhance air element logic
        KnightController knightController = playerObject.GetComponent<KnightController>();
        if (knightController != null)
        {
            knightController.walkSpeed *= 1.1f; // Increase walkSpeed by 10%
        }
    }

    IEnumerator CallUpgrade(float delay)
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime + delay)
        {
            yield return null;
        }

        if (upgrades == playerObject.GetComponent<PlayerExperience>().level)
        {
            Time.timeScale = 1f;
            levelUpCanvas.SetActive(false);
        }

        isButtonClickable = true;
    }
}
