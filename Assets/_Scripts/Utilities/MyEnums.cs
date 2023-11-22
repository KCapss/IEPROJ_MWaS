public enum WeaponTag : int
{   
    BasicDagger_0 = 0, BasicDagger_1, BasicDagger_2, BasicDagger_3, BasicDagger_4, BasicDagger_5, BasicDagger_6, BasicDagger_7,
    PoisonDagger_0, PoisonDagger_1, PoisonDagger_2, PoisonDagger_3, PoisonDagger_4, PoisonDagger_5, PoisonDagger_6, PoisonDagger_7, 
    DualDaggers_0, DualDaggers_1, DualDaggers_2, DualDaggers_3, DualDaggers_4, DualDaggers_5, DualDaggers_6, DualDaggers_7,
    BasicBow_0, BasicBow_1, BasicBow_2, BasicBow_3, BasicBow_4, BasicBow_5, BasicBow_6, BasicBow_7,
    ShortBow_0, ShortBow_1, ShortBow_2, ShortBow_3, ShortBow_4, ShortBow_5, ShortBow_6, ShortBow_7, 
    Crossbow_0, Crossbow_1, Crossbow_2, Crossbow_3, Crossbow_4, Crossbow_5, Crossbow_6, Crossbow_7,
    BasicSword_0, BasicSword_1, BasicSword_2, BasicSword_3, BasicSword_4, BasicSword_5, BasicSword_6, BasicSword_7,
    Longsword_0, Longsword_1, Longsword_2, Longsword_3, Longsword_4, Longsword_5, Longsword_6, Longsword_7,
    Zweihander_0, Zweihander_1, Zweihander_2, Zweihander_3, Zweihander_4, Zweihander_5, Zweihander_6, Zweihander_7,
};

public enum RestrictionType
{
    NONE = -1, 
    MIN = 0,
    MAX = 1,
    ODD = 2,
    EVEN = 3
}

public enum DamageType 
{   NONE = -1,
    Fire = 0, 
    Water, 
    Wind
};

public enum DamageTag 
{ 
    Fire_1 = 0, Fire_2, Fire_3, Fire_4, Fire_5, Fire_6, Fire_7,
    Water_1, Water_2, Water_3, Water_4, Water_5, Water_6, Water_7,
    Wind_1, Wind_2, Wind_3, Wind_4, Wind_5, Wind_6, Wind_7
};

public enum Lane 
{ 
    Left = 0, 
    Middle, 
    Right 
};

public enum Faction 
{ 
    Player = 0, 
    Enemy 
};

public enum AudioType 
{ 
    BGM = 0 
}

public enum VFXTag : int
{
    Dagger_Slash = 0,
    PoisonD_Slash,
    Dual_Slash,
    Basic_Bow,
    Short_Bow,
    CrossBow,
    Sword_Slash,
    Short_Slash,
    Zwei_Slash,
}

public enum PoolTag 
{ 
    DamageCard = 0, 
    WeaponCard = 1 
};

public enum BucketTier 
{
    NONE = -1,
    Tier1 = 0,
    Tier2,
    Tier3,
    Tier4,
    Tier5
}

public enum Levels
{
    TUTORIAL = 0,
    LEVEL_1,
    LEVEL_2,
    LEVEL_3,
    LEVEL_4
}


