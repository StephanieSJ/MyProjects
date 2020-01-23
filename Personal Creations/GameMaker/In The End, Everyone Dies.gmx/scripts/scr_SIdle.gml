///scr_SIdle
if (global.UpdateSurvivorGrid)
{
    if (TempX != 0)
    {
        StartX = x;
        StartY = y;
        TempX = 0;
        TempY = 0;
    }
    PathCreated = false;
    StopMoving = true;
    if (path_exists(Path))
    {
        path_end();
        path_delete(Path);
    }
    alarm[1] = (irandom(4) + 1) * 60;
}

if (PathCreated)
{
    if (path_position == 1)
    {
        if (TempX != 0)
        {
            StartX = TempX;
            StartY = TempY;
            TempX = 0;
            TempY = 0;
        }
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
        var RandomX = irandom(110) - 55;
        var RandomY = irandom(110) - 55;
        var ValidPath = mp_grid_path(global.SurvivorGrid, Path, x, y, StartX + RandomX, StartY + RandomY, true);
        if (ValidPath)
        {
            PathCreated = true;
            path_start(Path, Speed, path_action_stop, true);
        }
    }
}
