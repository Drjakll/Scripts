using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterHealth : MonoBehaviour {

    [SerializeField] public float MaxHealth = 1000.0f;
    [Range(0.0f, 1000.0f)]
    [SerializeField] public float CurrentHealth = 1000.0f;
    [SerializeField] public float MaxMana = 100.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField] public float CurrentMana = 100.0f;
    public SimpleHealthBar healthBar;
    public SimpleHealthBar manaBar;
    public GameObject PopUpText;
    private float WhenToPopText;
    private float AccHealth;

    // Use this for initialization
    void Start () {
        WhenToPopText = 20.0f;
        AccHealth = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.UpdateBar(CurrentHealth, MaxHealth);
        manaBar.UpdateBar(CurrentMana, MaxMana);
        float Heal = 5f * Time.deltaTime;
        AccHealth += Heal;
        UpdateHealth(Heal);
        UpdateMana(2 * Time.deltaTime);
	}

    public bool UpdateHealth(float health)
    {
        if (health > 0 && AccHealth >= WhenToPopText && CurrentHealth < 1000.0f)
        {
            GameObject TextInstance = Instantiate(PopUpText);
            TextInstance.GetComponent<TextMeshPro>().text = ((int)AccHealth).ToString();
            TextInstance.transform.position = transform.position + (new Vector3(0, 1, 0));
            TextInstance.GetComponent<TextMeshPro>().color = Color.green;
            TextInstance.GetComponent<PopUpText>().MaxDamage = AccHealth;
            AccHealth = AccHealth % WhenToPopText;
            WhenToPopText = Random.Range(5, 50);
        }
        else if(health < 0)
        {
            GameObject TextInstance = Instantiate(PopUpText);
            TextInstance.GetComponent<PopUpText>().MaxDamage = health;
            TextInstance.GetComponent<TextMeshPro>().text = ((int)health).ToString();
            TextInstance.transform.position = transform.position + (new Vector3(0, 1, 0));
        }
        if (CurrentHealth + health < 0)
        {
            CurrentHealth = 0;
            return false;
        }
        CurrentHealth += health;
        if (CurrentHealth > 1000.0f)
            CurrentHealth = 1000.0f;
        return true;
    }

    public bool UpdateMana(float mana)
    {
        if (CurrentMana + mana < 0)
            return false;
        CurrentMana = CurrentMana + mana;
        if (CurrentMana > 100.0f)
            CurrentMana = 100.0f;
        return true;
    }
}
