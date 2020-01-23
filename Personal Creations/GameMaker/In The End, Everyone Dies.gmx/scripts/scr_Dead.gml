///scr_Dead(Survivor)
var Survivor = argument0;
with (obj_ParentSurvivor)
{
    DepressionLevel += 20;
}

scr_DestroySurvivalMenu();

with (Survivor)
{
    with (AssignedObject)
    {
        Person = noone;
        Assigned = 'NOONE';
    }
    
    for (var i = 0; i < global.Survivors; i++)
    {
        if (global.SurvivorInstances[i] == Survivor)
        {
            global.Survivors--;
            if (global.Survivors <= 0)
            {
                game_end();
            }
            for (var j = i; j < global.Survivors - 1; j++)
            {
                global.SurvivorInstances[j] = global.SurvivorInstances[j + 1];
            }
            break;
        }
    }
    
    instance_destroy();
}
