///scr_FindShower
var BestStation = noone;

for (var i = 0; i < instance_number(obj_Shower); i++)
{
    var Instance = instance_find(obj_Shower, i);
    if !(Instance.Used)
    {
        BestStation = Instance;
        break;
    }
}

return BestStation;
