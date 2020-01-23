///scr_CheckValidPlot
var ValidFloors, ValidRooms;

ValidFloors = scr_CheckFloors();
if (ValidFloors)
{
    ValidRooms = scr_CheckRooms();
    if (ValidRooms)
    {
        return 3;
    }
    else
    {
        return 1;
    }
}
else
{
    ValidRooms = scr_CheckRooms();
    if (ValidRooms)
    {
        return 2;
    }
    else
    {
        return 0;
    }
}
