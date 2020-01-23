///scr_CheckFloors
var Valid = true;

with (obj_Floor)
{
    var TestWall = false;
    if !(collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_Floor, false, true))
    {
        TestWall = true;    
    }
    
    if !(collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_Floor, false, true))
    {
        TestWall = true;
    }
    
    if !(collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_Floor, false, true))
    {
        TestWall = true;
    }
    
    if !(collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_Floor, false, true))
    {
        TestWall = true;
    }
    
    if (TestWall)
    {
        if !(collision_rectangle(x - 7, y - 7, x + 7, y + 7, obj_ParentWall, false, true))
        {
            Valid = false;
        }
    }
}

return Valid;
