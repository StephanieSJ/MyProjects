///scr_GenerateTown
TownGrid[Size - 1, Size - 1] = 0;
for (var i = 0; i < Size; i++)
{
    for (var j = 0; j < Size; j++)
    {
        TownGrid[i, j] = 'E';
    }
}

var RandomX, RandomY;
var Farm = false;
while (Farm == false)
{
    RandomX = irandom(Size - 1);
    RandomY = irandom(Size - 1);
    if (TownGrid[RandomX, RandomY] == 'E')
    {
        TownGrid[RandomX, RandomY] = 'F';
        Farm = true;
    }
}

var GunShop = false;
while (GunShop == false)
{
    RandomX = irandom(Size - 1);
    RandomY = irandom(Size - 1);
    if (TownGrid[RandomX, RandomY] == 'E')
    {
        TownGrid[RandomX, RandomY] = 'G';
        GunShop = true;
    }
}

var Woods = false;
while (Woods == false)
{
    RandomX = irandom(Size - 1);
    RandomY = irandom(Size - 1);
    if (TownGrid[RandomX, RandomY] == 'E')
    {
        TownGrid[RandomX, RandomY] = 'W';
        Woods = true;
    }
}

var Mine = false;
while (Mine == false)
{
    RandomX = irandom(Size - 1);
    RandomY = irandom(Size - 1);
    if (TownGrid[RandomX, RandomY] == 'E')
    {
        TownGrid[RandomX, RandomY] = 'M';
        Mine = true;
    }
}

var ShoppingCenter = false;
while (ShoppingCenter == false)
{
    RandomX = irandom(Size - 1);
    RandomY = irandom(Size - 1);
    if (TownGrid[RandomX, RandomY] == 'E')
    {
        TownGrid[RandomX, RandomY] = 'S';
        ShoppingCenter = true;
    }
}

var PowerStation = false;
while (PowerStation == false)
{
    RandomX = irandom(Size - 1);
    RandomY = irandom(Size - 1);
    if (TownGrid[RandomX, RandomY] == 'E')
    {
        TownGrid[RandomX, RandomY] = 'P';
        PowerStation = true;
    }
}

var Hospital = false;
while (Hospital == false)
{
    RandomX = irandom(Size - 1);
    RandomY = irandom(Size - 1);
    if (TownGrid[RandomX, RandomY] == 'E')
    {
        TownGrid[RandomX, RandomY] = 'H';
        Hospital = true;
    }
}

var Depth = 0;
for (var i = 0; i < Size; i++)
{
    for (var j = 0; j < Size; j++)
    {
        var PlotInstance;
        switch(TownGrid[i, j])
        {
            case 'E':
            {
                PlotInstance = instance_create(0, 0, obj_EmptyPlot);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
            
            case 'F':
            {
                PlotInstance = instance_create(0, 0, obj_Farm);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
            
            case 'H':
            {
                PlotInstance = instance_create(0, 0, obj_Hospital);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
            
            case 'W':
            {
                PlotInstance = instance_create(0, 0, obj_Woods);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
            
            case 'M':
            {
                PlotInstance = instance_create(0, 0, obj_Mine);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
            
            case 'G':
            {
                PlotInstance = instance_create(0, 0, obj_GunShop);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
            
            case 'S':
            {
                PlotInstance = instance_create(0, 0, obj_ShoppingCenter);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
            
            case 'P':
            {
                PlotInstance = instance_create(0, 0, obj_PowerStation);
                with (PlotInstance)
                {
                    X = i;
                    Y = j;
                    depth = Depth;
                }
                break;
            }
        }
        Depth++;
    }
    Depth -= 10;
}
