using System.Collections.Generic;
using UnityEngine;

class FightCordenator
{
    private static Stats _instantAgressiveStats = Stats.CreateDictionary(Stats.DefaultStatusType.Empty);
    private static Stats _instantDefensiveStats = Stats.CreateDictionary(Stats.DefaultStatusType.Empty);

    public static int OnTryAHit(FighterStats agressive, FighterStats defensive)
    {
        SetInstantsZero();

        _instantAgressiveStats = agressive.Result();
        _instantDefensiveStats = defensive.Result();

        var chaosDamege = agressive.GetAttrubuteStats(
            FighterStats.StatusType.Buffs,
            FighterStats.StatusKey.ChaosDamage
        );

        var instantRandomDamage = Random.Range(
            _instantAgressiveStats.MinDamage,
            _instantAgressiveStats.MaxDamage + 1
        ) - (chaosDamege == 0 ? _instantDefensiveStats.Defense : 0);

        instantRandomDamage = instantRandomDamage > 0 ? instantRandomDamage : 1;

        var instantHealth = defensive.GetAttrubuteStats(
            FighterStats.StatusType.InstantBase,
            FighterStats.StatusKey.Health
        ) - instantRandomDamage;

        defensive.SetAttrubuteStats(
            FighterStats.StatusType.InstantBase,
            FighterStats.StatusKey.Health,
            instantHealth > 0 ? instantHealth : 0
        );

        return instantRandomDamage;
    }

    static void SetInstantsZero()
    {
        List<string> keys = new List<string>(_instantAgressiveStats.Keys);

        foreach (var item in keys)
        {
            _instantAgressiveStats[item] = 0;
        }

        keys = new List<string>(_instantDefensiveStats.Keys);

        foreach (var item in keys)
        {
            _instantDefensiveStats[item] = 0;
        }
    }
}