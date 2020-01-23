///scr_FindToilet
var BestStation = noone;
for (var i = 0; i < instance_number(obj_Toilet); i++)
{
    var Instance = instance_find(obj_Toilet, i);
    if !(Instance.Used)
    {
        BestStation = Instance;
        break;
    }
}

return BestStation;
