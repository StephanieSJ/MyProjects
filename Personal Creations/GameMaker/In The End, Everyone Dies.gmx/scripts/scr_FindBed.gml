///scr_FindBed
var BestStation = noone;
for (var i = 0; i < instance_number(obj_Bed); i++)
{
    var Instance = instance_find(obj_Bed, i);
    if !(Instance.Used)
    {
        BestStation = Instance;
        break;
    }
}

return BestStation;
