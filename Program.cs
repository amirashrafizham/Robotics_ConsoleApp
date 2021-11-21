﻿using System;
using PiTop;
using PiTop.MakerArchitecture.Expansion;
using PiTop.MakerArchitecture.Foundation;
using PiTop.MakerArchitecture.Foundation.Sensors;
using UnitsNet;
using System.Threading;
using Iot.Device.Common;
using Iot.Device.SenseHat;
using System.Drawing;

namespace ConsolePiTop
{
    class Program
    {
        static void Main(string[] args)
        {
            Speed fwSpeed = Speed.FromCentimetersPerSecond(20);
            Speed turnSpeed = Speed.FromCentimetersPerSecond(10);
            Speed revSpeed = Speed.FromCentimetersPerSecond(100);
            while (true)
            {
                Console.Write("Input: ");
                var input = Console.ReadKey();
                var sh = new SenseHat();
                Console.WriteLine(" ");
                using (sh)
                {
                    switch (input.Key)
                    {
                        case (ConsoleKey.W):
                            sh.SetPixel(2,4,Color.Purple);
                            sh.SetPixel(4,4,Color.Purple);
                            sh.SetPixel(2,6,Color.Purple);
                            sh.SetPixel(4,6,Color.Purple);
                            Console.WriteLine("Moving forward...");
                            Forward(fwSpeed);
                            Console.Clear();
                            break;
                        case (ConsoleKey.D):
                            sh.SetPixel(2,4,Color.Purple);
                            sh.SetPixel(4,4,Color.Blue);
                            sh.SetPixel(2,6,Color.Purple);
                            sh.SetPixel(4,6,Color.Blue);
                            Console.WriteLine("Turning clockwise...");
                            TurnRight(turnSpeed);
                            Console.Clear();
                            break;
                        case (ConsoleKey.A):
                            sh.SetPixel(2,4,Color.Blue);
                            sh.SetPixel(4,4,Color.Purple);
                            sh.SetPixel(2,6,Color.Blue);
                            sh.SetPixel(4,6,Color.Purple);
                            Console.WriteLine("Turning counter-clockwise...");
                            TurnLeft(turnSpeed);
                            Console.Clear();
                            break;
                        case (ConsoleKey.S):
                            sh.SetPixel(2,4,Color.Violet);
                            sh.SetPixel(4,4,Color.Violet);
                            sh.SetPixel(2,6,Color.Violet);
                            sh.SetPixel(4,6,Color.Violet);
                            Console.WriteLine("Reversing...");
                            Reverse(turnSpeed);
                            Console.Clear();
                            break;
                        case (ConsoleKey.Z):
                            sh.SetPixel(2,4,Color.Violet);
                            sh.SetPixel(4,4,Color.Red);
                            sh.SetPixel(2,6,Color.Violet);
                            sh.SetPixel(4,6,Color.Red);
                            Console.WriteLine("Reversing counter-clockwise...");
                            ReverseLeft(turnSpeed);
                            Console.Clear();
                            break;
                        case (ConsoleKey.C):
                            sh.SetPixel(2,4,Color.Red);
                            sh.SetPixel(4,4,Color.Violet);
                            sh.SetPixel(2,6,Color.Red);
                            sh.SetPixel(4,6,Color.Violet);
                            Console.WriteLine("Reversing clockwise...");
                            ReverseRight(turnSpeed);
                            break;
                        case (ConsoleKey.L):
                            sh.SetPixel(2,7,Color.Green);
                            sh.SetPixel(5,3,Color.Blue);
                            Console.WriteLine("Getting Ultrasonic reading...");
                            GetUltraSonic();
                            break;
                        case (ConsoleKey.K):
                            Console.WriteLine("Getting weather reading...");
                            GetWeather();
                            break;
                        case (ConsoleKey.J):
                            Console.WriteLine("Getting IMU and Magnetometer reading...");
                            GetIMU();
                            break;
                        default:
                            sh.Fill(Color.Empty);
                            break;
                    }
                }
            }
        }

        static void Forward(Speed rpmSpeed)
        {

                EncoderMotor mtrLeft;
                EncoderMotor mtrRight;
                var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();

                mtrLeft = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M2); //Port M1 Default Forward
                mtrRight = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M3); //Port M2 Default Reverse
                mtrRight.ForwardDirection = ForwardDirection.CounterClockwise;
                using (mtrLeft)
                {
                    mtrLeft.SetSpeed(rpmSpeed);
                }
                using (mtrRight)
                {
                    mtrRight.SetSpeed(rpmSpeed);
                }
                Thread.Sleep(2000);
                using (mtrLeft)
                {
                    mtrLeft.Stop();
                }
                using (mtrRight)
                {
                    mtrRight.Stop();
                }
        }

        static void TurnRight(Speed rpmSpeed)
        {

                EncoderMotor mtrLeft;
                var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();
                mtrLeft = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M2); //Port M1 Default Forward
                using (mtrLeft)
                {
                    mtrLeft.SetSpeed(rpmSpeed);
                    Thread.Sleep(2000);
                    mtrLeft.Stop();
                }

        }

        static void TurnLeft(Speed rpmSpeed)
        {

                EncoderMotor mtrRight;
                var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();
                mtrRight = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M3); //Port M2 Default Reverse
                mtrRight.ForwardDirection = ForwardDirection.CounterClockwise;

                using (mtrRight)
                {
                    mtrRight.SetSpeed(rpmSpeed);
                    Thread.Sleep(2000);
                    mtrRight.Stop();
                }
        }

        static void Reverse(Speed rpmSpeed)
        {

                EncoderMotor mtrLeft;
                EncoderMotor mtrRight;
                var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();


                mtrLeft = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M2); //Port M1 Default Forward
                mtrRight = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M3); //Port M2 Default Reverse 
                mtrLeft.ForwardDirection = ForwardDirection.CounterClockwise;

                using (mtrLeft)
                {
                    mtrLeft.SetSpeed(rpmSpeed);
                }
                using (mtrRight)
                {
                    mtrRight.SetSpeed(rpmSpeed);
                }
                Thread.Sleep(2000);
                using (mtrLeft)
                {
                    mtrLeft.Stop();
                }
                using (mtrRight)
                {
                    mtrRight.Stop();
                }
        }

        static void ReverseLeft(Speed rpmSpeed)
        {

                EncoderMotor mtrLeft;
                var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();

                mtrLeft = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M2); //Port M1 Default Forward
                mtrLeft.ForwardDirection = ForwardDirection.CounterClockwise;

                using (mtrLeft)
                {
                    mtrLeft.SetSpeed(rpmSpeed);
                    Thread.Sleep(2000);
                    mtrLeft.Stop();
                }
        }

        static void ReverseRight(Speed rpmSpeed)
        {

                EncoderMotor mtrRight;
                var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();

                mtrRight = expansionPlate.GetOrCreateEncoderMotor(EncoderMotorPort.M3); //Port M2 Default Reverse

                using (mtrRight)
                {
                    mtrRight.SetSpeed(rpmSpeed);
                    Thread.Sleep(2000);
                    mtrRight.Stop();
                }
        }

        static void GetUltraSonic()
        {
            try
            {
                UltrasonicSensor frontUltraSound;
                var expansionPlate = PiTop4Board.Instance.GetOrCreateExpansionPlate();
                frontUltraSound = expansionPlate.GetOrCreateUltrasonicSensor(DigitalPort.D3);
                Console.WriteLine($"Distance of object infront of robot: {frontUltraSound.Distance.Centimeters:N2} cm");
                Console.WriteLine("  ");
            }
            catch (System.Exception)
            {
                Console.WriteLine("try again");   
                Console.WriteLine("  ");
            }
        }

        static void GetWeather()
        {
            var sh = new SenseHat();
            using (sh)
            {
                try
                {
                    sh.SetPixel(6,5,Color.Orange);
                    sh.SetPixel(3,7,Color.Violet);
                    Console.WriteLine($"Temperature: Sensor1: {sh.Temperature.DegreesCelsius:N2} °C   Sensor2: {sh.Temperature2.DegreesCelsius:N2} °C");
                    Console.WriteLine($"Humidity: {sh.Humidity} %rH");
                    Console.WriteLine($"Pressure: {sh.Pressure} hPa");
                    Console.WriteLine("  ");
                }
                catch (System.Exception)
                {
                    Console.WriteLine("try again");   
                    Console.WriteLine("  ");
                }
            }
        }

        static void GetIMU()
        {
            var sh = new SenseHat();
            using (sh)
            {
                try
                {
                    sh.SetPixel(2,4,Color.Blue);
                    sh.SetPixel(4,4,Color.Red);
                    Console.WriteLine($"Acceleration: {sh.Acceleration:N2} g");
                    Console.WriteLine($"Angular rate: {sh.AngularRate:N2} DPS");
                    Console.WriteLine($"Magnetic induction: {sh.MagneticInduction:N2} gauss");
                    Console.WriteLine("  ");
                }
                catch (System.Exception)
                {
                    Console.WriteLine("try again");   
                    Console.WriteLine("  ");
                }
            }

        }
    }
}