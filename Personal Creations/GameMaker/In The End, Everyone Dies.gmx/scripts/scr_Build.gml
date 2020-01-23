///scr_Build(startx, starty, endx, endy, object)
var BuildStartX = argument0;
var BuildStartY = argument1;
var BuildEndX = argument2;
var BuildEndY = argument3;
var Object = argument4;

if (BuildStartX > BuildEndX)
{
    var Temp = BuildEndX;
    BuildEndX = BuildStartX;
    BuildStartX = Temp;
}

if (BuildStartY > BuildEndY)
{
    var Temp = BuildEndY;
    BuildEndY = BuildStartY;
    BuildStartY = Temp;
}

var Rows = abs((BuildEndX / 16) - (BuildStartX / 16));
var Columns = abs((BuildEndY / 16) - (BuildStartY / 16));
for (var i = 0; i <= Rows; i++)
{
    for (var j = 0; j <= Columns; j++)
    {
        if !(collision_rectangle(BuildStartX + (i * 16) + 1, BuildStartY + (j * 16) + 1, BuildStartX + (i * 16) + 14, BuildStartY + (j * 16) + 14, obj_ParentFoundation, true, false))
        {
            instance_create(BuildStartX + (i * 16), BuildStartY + (j * 16), Object);
        }
    }
}
