///scr_InstancesAtPoint(X1, Y1, Object, Precise, NotMe)
var X1, Y1, Object, Precise, NotMe, IDList, ID;
X1 = argument0;
Y1 = argument1;
Object = argument2;
Precise = argument3;
NotMe = argument4;
IDList = ds_list_create();
with (Object) 
{
    if (!NotMe) or (id != other.id)
    {
        ID = collision_point(X1, Y1, id, Precise, false);
        if (ID != noone) 
        {
            ds_list_add(IDList, ID);
        }
    }
}

if (ds_list_empty(IDList))
{
    ds_list_destroy(IDList);
    IDList = noone;
}

return IDList;
