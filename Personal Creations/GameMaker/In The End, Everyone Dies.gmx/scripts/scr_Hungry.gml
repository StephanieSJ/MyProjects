///scr_Hungry
if (obj_Resources.Food == 0) and (obj_Resources.CookedFood == 0)
{
    scr_GoBackToAssignedState();
}
if !(Distracted)
{
    Counter++;
    if (instance_exists(obj_Table))
    {
        if (Counter == 375)
        {
            Counter = 0;
            if (obj_Resources.CookedFood > 0)
            {
                obj_Resources.CookedFood--;
                Hunger -= 25;
                if (Hunger < 0)
                {
                    Hunger = 0;
                }
            }
            else if (obj_Resources.Food > 0)
            {
                obj_Resources.Food--;
                Hunger -= 10;
                if (Hunger < 0)
                {
                    Hunger = 0;
                }
            }
            else
            {
                scr_GoBackToAssignedState();
            }
        }
    }
    else
    {
        if (Counter == 750)
        {
            Counter = 0;
            if (obj_Resources.CookedFood > 0)
            {
                obj_Resources.CookedFood--;
                Hunger -= 25;
                if (Hunger < 0)
                {
                    Hunger = 0;
                }
            }
            else if (obj_Resources.Food > 0)
            {
                obj_Resources.Food--;
                Hunger -= 10;
                if (Hunger < 0)
                {
                    Hunger = 0;
                }
            }
            else
            {
                scr_GoBackToAssignedState();
            }
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
        if (instance_exists(obj_Table))
        {
            var RandomInstance = instance_find(obj_Table, random(instance_number(obj_Table)) - 1);
            StartX = RandomInstance.x;
            StartY = RandomInstance.y;
        }
        var RandomX = 0;
        var RandomY = 0;
        var ValidPath = mp_grid_path(global.SurvivorGrid, Path, x, y, StartX + RandomX, StartY + RandomY, true);
        if (ValidPath)
        {
            PathCreated = true;
            path_start(Path, Speed, path_action_stop, true);
        }
    }
}
