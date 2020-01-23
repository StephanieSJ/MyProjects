///scr_PlotChosen(plot instance)
var ChosenPlot = argument0;
var Resources = instance_create(0, 0, obj_Resources);

instance_create(0, 0, obj_Electronics);

var Size = obj_TownGrid.Size;
var TownGrid = obj_TownGrid.TownGrid;
var Distance = 0;
var MinFarm = 999;
var MinWoods = 999;
var MinMine = 999;
var MinGuns = 999;
var MinShops = 999;
var MinPower = 999;
var MinHospital = 999;

/*for (var i = 0; i < Size; i++)
{
    for (var j = 0; j < Size; j++)
    {
        switch (TownGrid[i, j])
        {
            case 'F':
            {
                Distance = abs(ChosenPlot.X - i) + abs(ChosenPlot.Y - j);
                MinFarm = Distance;
                Resources.Food += (((Size * 2) - Distance) * 20);
                break;
            }
            
            case 'W':
            {
                Distance = abs(ChosenPlot.X - i) + abs(ChosenPlot.Y - j);
                MinWoods = Distance;
                Resources.Wood += (((Size * 2) - Distance) * 20);
                break;
            }
            
            case 'M':
            {
                Distance = abs(ChosenPlot.X - i) + abs(ChosenPlot.Y - j);
                MinMine = Distance;
                Resources.Stone += (((Size * 2) - Distance) * 20);
                break;
            }
            
            case 'G':
            {
                Distance = abs(ChosenPlot.X - i) + abs(ChosenPlot.Y - j);
                MinGuns = Distance;
                Resources.Guns += (((Size * 2) - Distance) * 20);
                break;
            }
            
            case 'S':
            {
                Distance = abs(ChosenPlot.X - i) + abs(ChosenPlot.Y - j);
                MinShops = Distance;
                Resources.Shops += (((Size * 2) - Distance) * 20);
                break;
            }
            
            case 'P':
            {
                Distance = abs(ChosenPlot.X - i) + abs(ChosenPlot.Y - j);
                MinPower = Distance;
                Resources.Power += (((Size * 2) - Distance) * 20);
                break;
            }
        }
    }
}*/

Resources.ClosestWoods = MinWoods;
Resources.ClosestMine = MinMine;
Resources.ClosestFarm = MinFarm;
Resources.ClosestShops = MinShops;
Resources.ClosestGuns = MinGuns;
Resources.ClosestPower = MinPower;
Resources.ClosestHospital = MinHospital;
