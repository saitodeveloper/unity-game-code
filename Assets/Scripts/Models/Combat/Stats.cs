using System.Collections.Generic;

public class Stats : Dictionary<string, int>
{
    public enum DefaultStatusType
    {
        Player,
        BasicMonster,
        Any,
        Empty
    }

    public static Stats CreateDictionary(DefaultStatusType statusType)
    {
        return statusType switch
        {
            DefaultStatusType.Player => new Stats
            {
                { "Health", 100 },
                { "Will", 50 },
                { "ExpValue", 0 },
                { "CurrentLevel", 1 },
                { "ExpNextLevel", 100 },
                { "MinDamage", 20 },
                { "MaxDamage", 25 },
                { "Defense", 5 },
                { "ExpAcct", 0 },
                { "HealthRecoveryPercent", 5 }
            },
            DefaultStatusType.BasicMonster => new Stats
            {
                { "Health", 50 },
                { "Will", 20 },
                { "ExpValue", 25 },
                { "CurrentLevel", 1 },
                { "ExpNextLevel", 50 },
                { "MinDamage", 10 },
                { "MaxDamage", 15 },
                { "Defense", 2 },
                { "ExpAcct", 0 },
                { "HealthRecoveryPercent", 0 }
            },
            DefaultStatusType.Any => new Stats
            {
                { "Health", 10 },
                { "Will", 10 },
                { "ExpValue", 5 },
                { "CurrentLevel", 1 },
                { "ExpNextLevel", 10 },
                { "MinDamage", 1 },
                { "MaxDamage", 2 },
                { "Defense", 1 },
                { "ExpAcct", 0 },
                { "HealthRecoveryPercent", 0 }
            },
            DefaultStatusType.Empty => new Stats
            {
                { "Health", 0 },
                { "Will", 0 },
                { "ExpValue", 0 },
                { "CurrentLevel", 0 },
                { "ExpNextLevel", 0 },
                { "MinDamage", 0 },
                { "MaxDamage", 0 },
                { "Defense", 0 },
                { "ExpAcct", 0 },
                { "HealthRecoveryPercent", 0 }
            },
            _ => null
        };
    }

    public int Health
    {
        get => this["Health"];
        set => this["Health"] = value;
    }

    public int Will
    {
        get => this["Will"];
        set => this["Will"] = value;
    }

    public int ExpValue
    {
        get => this["ExpValue"];
        set => this["ExpValue"] = value;
    }

    public int CurrentLevel
    {
        get => this["CurrentLevel"];
        set => this["CurrentLevel"] = value;
    }

    public int ExpNextLevel
    {
        get => this["ExpNextLevel"];
        set => this["ExpNextLevel"] = value;
    }

    public int MinDamage
    {
        get => this["MinDamage"];
        set => this["MinDamage"] = value;
    }

    public int MaxDamage
    {
        get => this["MaxDamage"];
        set => this["MaxDamage"] = value;
    }

    public int Defense
    {
        get => this["Defense"];
        set => this["Defense"] = value;
    }

    public int ExpAcct
    {
        get => this["ExpAcct"];
        set => this["ExpAcct"] = value;
    }

    public int HealthRecoveryPercent
    {
        get => this["HealthRecoveryPercent"];
        set => this["HealthRecoveryPercent"] = value;
    }
}