///scr_Sleeping
if !(Distracted)
{
    Counter++;
    if (Counter == 375)
    {
        Counter = 0;
        Tiredness += 25;
        if (Tiredness >= 100)
        {
            Bed.Used = false;
            Bed = noone;
            Tiredness = 100;
            scr_GoBackToAssignedState();
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
        if (Bed == noone)
        {
            Bed = scr_FindBed();
        }
        if (Bed == noone)
        {
            StartX = x;
            StartY = y;
        }
        else
        {
            with (Bed)
            {
                Used = true;
            }
            StartX = Bed.x;
            StartY = Bed.y;
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