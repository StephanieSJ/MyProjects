///scr_GetAvailableSurvivors
var AvailableSurvivors = 0;
var TotalAvailableSurvivors = 0;

if (global.MenuCreated != 'Scavenge')
{
    AvailableSurvivors[TotalAvailableSurvivors] = noone;
    TotalAvailableSurvivors++;
}

for (var i = 0; i < global.Survivors; i++)
{
    if (global.SurvivorInstances[i].Assignment == 'Nothing')
    {
        AvailableSurvivors[TotalAvailableSurvivors] = global.SurvivorInstances[i];
        TotalAvailableSurvivors++;
    }
}

return AvailableSurvivors;
