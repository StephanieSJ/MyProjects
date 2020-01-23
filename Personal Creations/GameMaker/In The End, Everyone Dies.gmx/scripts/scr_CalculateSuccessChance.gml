///scr_CalculateSuccessChance(survivor)
var Survivor = argument0;

//Use Distances


var HungerChance = 100 - Survivor.Hunger;
if (HungerChance < 0)
{
    HungerChance = 0;
}

var HealthChance = Survivor.Health;
var DepressionChance = 100 - (Survivor.DepressionLevel / Survivor.Protection * 100);
var TirednessChance = Survivor.Tiredness;

var TotalChance = (HungerChance + HealthChance + DepressionChance + TirednessChance) / 4;
return TotalChance;
