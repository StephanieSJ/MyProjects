///scr_Cooking
if (obj_Resources.Power == 0) or (obj_Resources.Food == 0)
{
    with (AssignedObject)
    {
        Person = noone;
        Assigned = 'NOONE';
    }
    Assignment = 'Nothing';
    AssignedObject = noone;
    State = scr_SIdle;
}

if !(Distracted)
{
    if (collision_rectangle(AssignedObject.x - 20, AssignedObject.y - 20, AssignedObject.x + 20, AssignedObject.y + 20, self, false, false))
    {
        Counter++;
        if (Counter == 750)
        {
            if (Profession == 'Chef')
            {
                obj_Resources.CookedFood += 6;
            }
            else
            {
                obj_Resources.CookedFood += 3;
            }
            Counter = 0;
            obj_Resources.Food--;
            obj_Resources.Power--;
        }
    }
}

if (global.UpdateSurvivorGrid)
{
    PathCreated = false;
    StopMoving = true;
    if (path_exists(Path))
    {
        path_end();
        path_delete(Path);
    }
    alarm[1] = 2;
}

if (PathCreated)
{
    if (path_position == 1)
    {
        PathCreated = false;
        StopMoving = true;
        alarm[1] = (irandom(4) + 1) * 60;
        path_delete(Path);
    }
}
else if !(StopMoving)
{
    Path = path_add();
    while !(PathCreated)
    {
        StartX = AssignedObject.x;
        StartY = AssignedObject.y;
        var RandomX = irandom(32) - 16;
        var RandomY = irandom(32) - 16;
        var ValidPath = mp_grid_path(global.SurvivorGrid, Path, x, y, StartX + RandomX, StartY + RandomY, true);
        if (ValidPath)
        {
            PathCreated = true;
            path_start(Path, Speed, path_action_stop, true);
        }
    }
}
