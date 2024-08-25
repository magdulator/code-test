using TollCalculator.source.Models;

var tollCalculator = new TollCalculatorService();
DateTime[] dates = {
                    new DateTime(2024, 8, 26, 7, 0, 0), //18
                   new DateTime(2024, 8, 26, 7, 59, 0), // 0 (within one hour)
                   new DateTime(2024, 8, 26, 10, 0, 0), //8
                   new DateTime(2024, 8, 26, 10, 30, 0), //0 (within one hour)
                   new DateTime(2024, 8, 26, 19, 0, 0) }; //0
                                                          //== 26
var vehicle = new Car();

Console.WriteLine(tollCalculator.GetTollFee(vehicle, dates));
