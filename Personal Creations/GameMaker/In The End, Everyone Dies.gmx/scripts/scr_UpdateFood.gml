///scr_UpdateFood
var ActiveFridges = 0;
for (var i = 0; i < instance_number(obj_Fridge); i++)
{
    if (instance_find(obj_Fridge, i).Active)
    {
        ActiveFridges++;
    }
}

var FoodLoss = 10 - ActiveFridges;
FoodLoss = clamp(FoodLoss, 1, 10);

return FoodLoss;
