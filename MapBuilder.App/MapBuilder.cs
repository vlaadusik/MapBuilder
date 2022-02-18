using System;
using System.Collections.Generic;

namespace MapBuilder.App
{
    public class MapBuilder
    {
        private SignStep _currentOfficeManagerPosition = new()
        {
            Floor = 0,
            Section = 1
        };

        public IEnumerable<string> BuildRouteMap(IEnumerable<SignStep> signatureMap)
        {
            List<string> rezult = new List<string>();
            foreach (var item in signatureMap)
            {
                int timeStairs = Math.Abs(item.Floor - _currentOfficeManagerPosition.Floor) * 2;
                int timeElevator = 0;
                bool oddFloorItem = item.Floor % 2 != 0;
                bool currentSection1 = _currentOfficeManagerPosition.Section == 1;
                bool sectionEquals = item.Section == _currentOfficeManagerPosition.Section;

                // going to another section
                if (oddFloorItem)
                    if (item.Section == 1 && currentSection1)
                        timeElevator += 2;
                    else
                        if (!sectionEquals)
                            timeElevator += 1;
                if (!oddFloorItem)
                    if (item.Section == 2 && !currentSection1)
                        timeElevator += 2;
                    else
                        if (!sectionEquals)
                            timeElevator += 1;


                timeElevator += 1; // waiting for the elevator
                timeElevator += Math.Abs(item.Floor - _currentOfficeManagerPosition.Floor);

                if (timeStairs <= timeElevator)
                    rezult.Add("S");
                else
                {
                    if (oddFloorItem)
                        rezult.Add("E2");
                    else
                        rezult.Add("E1");
                }

                _currentOfficeManagerPosition = item;
            }
            return rezult;
        }
    }
}