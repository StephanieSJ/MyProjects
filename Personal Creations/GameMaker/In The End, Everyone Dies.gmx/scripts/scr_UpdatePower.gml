///scr_UpdatePower
var ElectronicsToCheck = obj_Electronics.Electronics;
var Number = obj_Electronics.NumberElectronics;

for (var i = 0; i < Number; i++)
{
    if (ElectronicsToCheck[i].Active)
    {
        if (ElectronicsToCheck[i].Name == 'Generator')
        {
            Power += 5;
            Wood--;
            if (Wood <= 0)
            {
                Wood = 0;
                with (ElectronicsToCheck[i])
                {
                    Times = '000000000000000000000000';
                    Active = false;
                }
            }
        }
        else
        {
            Power--;
            if (Power <= 0)
            {
                Power = 0;
                scr_StopPower();
            }
        }
    }
}
