using System;
using PiTop;
using PiTop.MakerArchitecture.Expansion;
using PiTop.MakerArchitecture.Foundation;
using PiTop.MakerArchitecture.Foundation.Sensors;
using UnitsNet;
using System.Threading;
using Iot.Device.Common;
using Iot.Device.SenseHat;
using System.Drawing;
using System.Threading.Tasks;
using ConsolePiTop;




static void CheckBattery()
{
    var batteryState = PiTop4Board.Instance.BatteryState.TimeRemaining.Minutes;
    Console.WriteLine($"{batteryState}");
}


static void ServoHeadController()
{

    ServoMotor mtrHorizontal;
    ServoMotor mtrVertical;

    Console.Write("Set horizontal angle: ");
    int valueHorizontal = Convert.ToInt16(Console.ReadLine());
    Angle setAngleHorizontal = Angle.FromDegrees(valueHorizontal);


    Console.Write("Set vertical angle: ");
    int valueVertical = Convert.ToInt16(Console.ReadLine());
    Angle setAngleVertical = Angle.FromDegrees(valueVertical);

    var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();
    mtrHorizontal = expansionPlate.GetOrCreateServoMotor(ServoMotorPort.S1); //Port M1 Default Forward
    mtrVertical = expansionPlate.GetOrCreateServoMotor(ServoMotorPort.S4);

    using (mtrHorizontal)
    {
        mtrHorizontal.GoToAngle(setAngleHorizontal);
    }

    using (mtrVertical)
    {
        mtrVertical.GoToAngle(setAngleVertical);
    }
}