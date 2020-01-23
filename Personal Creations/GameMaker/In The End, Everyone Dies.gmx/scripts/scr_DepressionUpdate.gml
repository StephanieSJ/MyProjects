///scr_DepressionUpdate(Object)
var Object = argument0;
var Change = 0;
for (var i = 0; i < instance_number(Object); i++)
{
    if (Object == obj_TV)
    {
        with (instance_find(Object, i))
        {
            if (Active)
            {
                Change += 5;
            }
        }
    }
    else
    {
        Change++;
    }
}

return Change;
