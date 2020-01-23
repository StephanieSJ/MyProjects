///scr_FindHealingStation
var BestStation = noone;
for (var i = 0; i < instance_number(obj_HealingStation); i++)
{
    var Instance = instance_find(obj_HealingStation, i);
    if (Instance.Assigned != 'NOONE') and !(Instance.Used)
    {
        BestStation = Instance;
        break;
    }
}

if (BestStation == noone)
{
    for (var i = 0; i < instance_number(obj_HealingStation); i++)
    {
        var Instance = instance_find(obj_HealingStation, i);
        if !(Instance.Used)
        {
            BestStation = Instance;
            break;
        }
    }
}

return BestStation;
