using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _hitRange;

    private RangeWeapon _currentRangeWeapon;
    private MeleeWeapon _currentMeleeWeapon;
    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _currentWeapon == _currentRangeWeapon)
        {
            _animator.Play("Shoot");
            _currentRangeWeapon.Shoot(_shootPoint);
        }
        else if (Input.GetMouseButtonDown(0) && _currentWeapon == _currentMeleeWeapon)
        {
            _animator.Play("AxeBlow");
            _currentMeleeWeapon.Smash(_attackPoint);
        }
        else
        {
            return;
        }
    } 

    private void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _hitRange);
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;

        if (weapon is RangeWeapon)
        {
            _currentRangeWeapon = (RangeWeapon)weapon;
        }
        else if (weapon is MeleeWeapon)
        {
            _currentMeleeWeapon = (MeleeWeapon)weapon;
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth,_health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }
}
