using System.Text;
using UnityEngine;

public class FighterStats
{
    private Stats _base;
    private Stats _instantBase;
    private Stats _buffs;
    private Stats _nerfs;
    private Stats _tempBuffs;
    private Stats _tempNerfs;

    public enum StatusType
    {
        Base,
        InstantBase,
        Buffs,
        Nerfs,
        TempBuffs,
        TempNerfs
    }

    public enum StatusKey
    {
        Health,
        Will,
        ExpValue,
        CurrentLevel,
        ExpNextLevel,
        MinDamage,
        MaxDamage,
        Defense,
        ChaosDamage,
        HealthRecoveryPercent
    }

    public FighterStats(Stats.DefaultStatusType type)
    {
        _base = Stats.CreateDictionary(type);
        _instantBase =  Stats.CreateDictionary(type);
        _buffs = Stats.CreateDictionary(Stats.DefaultStatusType.Empty);
        _nerfs = Stats.CreateDictionary(Stats.DefaultStatusType.Empty);
        _tempBuffs = Stats.CreateDictionary(Stats.DefaultStatusType.Empty);
        _tempNerfs = Stats.CreateDictionary(Stats.DefaultStatusType.Empty);
    }

    private Stats GetByStatusType(StatusType statusType)
    {
        return statusType switch
        {
            StatusType.Base => _base,
            StatusType.InstantBase => _instantBase,
            StatusType.Buffs => _buffs,
            StatusType.Nerfs => _nerfs,
            StatusType.TempBuffs => _tempBuffs,
            StatusType.TempNerfs => _tempNerfs,
            _ => null
        };
    }

    public void SetAttrubuteStats(StatusType statusType, StatusKey statusKey, int value)
    {
        var key = GetStatusName(statusKey);
        var stats = GetByStatusType(statusType);
        if (stats?.ContainsKey(key) ?? false) stats[key] = value;
    }

    public int GetAttrubuteStats(StatusType statusType, StatusKey statusKey)
    {
        var key = GetStatusName(statusKey);
        var stats = GetByStatusType(statusType);

        if (stats?.TryGetValue(key, out int value) ?? false)
        {
            return value;
        }
        else
        {
            return 0;
        }
    }

    public Stats Result()
    {
        Stats result = new Stats();

        // Base calculation
        foreach (var key in _base.Keys)
        {
            result[key] =
                _base[key] + _buffs[key] + _tempBuffs[key] -
                (_nerfs[key] + _tempNerfs[key]);

            result[key] = result[key] > 0 ? result[key] : 1;
        }

        return result;
    }

    public string GetStatusName(StatusKey statusKey)
    {
        return statusKey.ToString();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        void AppendStats(string title, Stats stats)
        {
            sb.AppendLine(title + ":");
            foreach (var kvp in stats)
            {
                sb.AppendLine($"  {kvp.Key}: {kvp.Value}");
            }
        }

        AppendStats("Base", _base);
        AppendStats("Buffs", _buffs);
        AppendStats("Nerfs", _nerfs);
        AppendStats("TempBuffs", _tempBuffs);
        AppendStats("TempNerfs", _tempNerfs);

        return sb.ToString();
    }
}
