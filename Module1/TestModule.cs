using System;
using NetTopologySuite.Geometries;
using OSMLSGlobalLibrary;
using OSMLSGlobalLibrary.Map;
using OSMLSGlobalLibrary.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Module1
{
    public class TestModule : OSMLSModule
    {
        //инициализируем все переменные
        Airport airport1;
        Airport airport2;
        Airport airport3;
        Airport airport4;
        Airport airport5;

        Airplane airplane1;
        Airplane airplane2;
        Airplane airplane3;
        Airplane airplane4;
        Airplane airplane5;

        Port port1;
        Port port2;
        Port port3;
        Port port4;
        Port port5;

        Ship ship1;
        Ship ship2;
        Ship ship3;
        Ship ship4;
        Ship ship5;

        Coordinate CoordinateDep1;
        Coordinate CoordinateDep2;
        Coordinate CoordinateDep3;
        Coordinate CoordinateDep4;
        Coordinate CoordinateDep5;

        Coordinate CoordinateDepShip1;
        Coordinate CoordinateDepShip2;
        Coordinate CoordinateDepShip3;
        Coordinate CoordinateDepShip4;
        Coordinate CoordinateDepShip5;

        protected override void Initialize()
        {
            //координаты аэропорта
            var airport1Coordinate = MathExtensions.LatLonToSpherMerc(-34.831747, -56.020034);
            var airport2Coordinate = new Coordinate(4942686, 6234338);
            var airport3Coordinate = new Coordinate(-8579292, 4700571);
            var airport4Coordinate = new Coordinate(15547903, 4259071);
            var airport5Coordinate = new Coordinate(5241746, -2178150);

            //создаем аэропорты
            airport1 = new Airport(airport1Coordinate);
            airport2 = new Airport(airport2Coordinate);
            airport3 = new Airport(airport3Coordinate);
            airport4 = new Airport(airport4Coordinate);
            airport5 = new Airport(airport5Coordinate);

            //создаем порты
            port1 = new Port(new Coordinate(-3991847, -596820));
            port2 = new Port(new Coordinate(-4520180, -2299226));
            port3 = new Port(new Coordinate(1193641, -381574));
            port4 = new Port(new Coordinate(2074195, -3982063));
            port5 = new Port(new Coordinate(12914800, -4138606));

            //добавляем на карту объекты
            MapObjects.Add(port1);
            MapObjects.Add(port2);
            MapObjects.Add(port3);
            MapObjects.Add(port4);
            MapObjects.Add(port5);
            MapObjects.Add(airport1);
            MapObjects.Add(airport2);
            MapObjects.Add(airport3);
            MapObjects.Add(airport4);
            MapObjects.Add(airport5);

            //создаем 5 самолетов
            airplane1 = CreateAirplane(MathExtensions.LatLonToSpherMerc(-34.831747, -56.020034));
            airplane2 = CreateAirplane(new Coordinate(4942686, 6234338));
            airplane3 = CreateAirplane(new Coordinate(-8579292, 4700571));
            airplane4 = CreateAirplane(new Coordinate(15547903, 4259071));
            airplane5 = CreateAirplane(new Coordinate(5241746, -2178150));

            //создаем 5 кораблей
            ship1 = CreateShip(new Coordinate(-3991847, -596820));
            ship2 = CreateShip(new Coordinate(-4520180, -2299226));
            ship3 = CreateShip(new Coordinate(1193641, -381574));
            ship4 = CreateShip(new Coordinate(2074195, -3982063));
            ship5 = CreateShip(new Coordinate(12914800, -4138606));
        }
        /// <summary>
        /// Создает самолет в заданной точке пространства
        /// </summary>
        /// <param name="coordinateAirport"></param>
        /// <returns></returns>
        public Airplane CreateAirplane(Coordinate coordinateAirport)
        {
            Airplane airplane;
            LineString line;
            Random rnd = new Random();
            Coordinate coordinateDeparture = coordinateAirport;
            Coordinate coordinateArrival = new Coordinate(0, 0);
            double speed = rnd.Next(1000, 2000);
            do
            {
                int rndForCoordinate = rnd.Next(1, 6);
                switch (rndForCoordinate)
                {
                    case 1:
                        coordinateArrival = MathExtensions.LatLonToSpherMerc(-34.831747, -56.020034);
                        break;
                    case 2:
                        coordinateArrival = new Coordinate(4942686, 6234338);
                        break;
                    case 3:
                        coordinateArrival = new Coordinate(-8579292, 4700571);
                        break;
                    case 4:
                        coordinateArrival = new Coordinate(15547903, 4259071);
                        break;
                    case 5:
                        coordinateArrival = new Coordinate(5241746, -2178150);
                        break;
                }
            } while (coordinateDeparture.Equals2D(coordinateArrival));
            airplane = new Airplane(coordinateDeparture, speed, coordinateArrival);
            var lineCoordinates = new Coordinate[] { airplane.coordinateDepartureAirplane, airplane.coordinateArrivalAirplane };
            line = new LineString(lineCoordinates);
            MapObjects.Add(line);
            MapObjects.Add(airplane);
            return airplane;
        }



        /// <summary>
        /// Создает корабль в заданной точке пространства
        /// </summary>
        /// <param name="coordinateAirport"></param>
        /// <returns></returns>
        public Ship CreateShip(Coordinate coordinateAirport)
        {
            Ship ship;
            LineString line;
            Random rnd = new Random();
            Coordinate coordinateIntermediate = new Coordinate(0, 0);
            Coordinate coordinateDeparture = coordinateAirport;
            Coordinate coordinateArrival = new Coordinate(0, 0);
            double speed = rnd.Next(1000, 2000);
            do
            {
                int rndForCoordinate = rnd.Next(1, 6);
                switch (rndForCoordinate)
                {
                    case 1:
                        coordinateArrival = new Coordinate(-3991847, -596820);
                        break;
                    case 2:
                        coordinateArrival = new Coordinate(-4520180, -2299226);
                        break;
                    case 3:
                        coordinateArrival = new Coordinate(1193641, -381574);
                        break;
                    case 4:
                        coordinateArrival = new Coordinate(2074195, -3982063);
                        break;
                    case 5:
                        coordinateArrival = new Coordinate(12914800, -4138606);
                        break;
                }
            } while (coordinateDeparture.Equals2D(coordinateArrival));
            ship = new Ship(coordinateDeparture, speed, coordinateArrival);
            var lineCoordinates = new Coordinate[] { ship.coordinateDepartureShip, ship.coordinateArrivalShip };
            line = new LineString(lineCoordinates);
            MapObjects.Add(line);
            MapObjects.Add(ship);
            return ship;
        }



        /// <summary>
        /// Вызывается постоянно, здесь можно реализовывать логику перемещений и всего остального, требующего времени.
        /// </summary>
        /// <param name="elapsedMilliseconds">TimeNow.ElapsedMilliseconds</param>
        public override void Update(long elapsedMilliseconds)
        {
            if (airplane1.reached && airplane2.reached && airplane3.reached && airplane4.reached && airplane5.reached)
            {
                CoordinateDep1 = airplane1.coordinateDepartureAirplane.Copy();
                CoordinateDep2 = airplane2.coordinateDepartureAirplane.Copy();
                CoordinateDep3 = airplane3.coordinateDepartureAirplane.Copy();
                CoordinateDep4 = airplane4.coordinateDepartureAirplane.Copy();
                CoordinateDep5 = airplane5.coordinateDepartureAirplane.Copy();
                airplane1.reached = false;
                airplane2.reached = false;
                airplane3.reached = false;
                airplane4.reached = false;
                airplane5.reached = false;
            }
            if (ship1.reached && ship2.reached && ship3.reached && ship4.reached && ship5.reached)
            {

                CoordinateDepShip1 = ship1.coordinateDepartureShip.Copy();
                CoordinateDepShip2 = ship2.coordinateDepartureShip.Copy();
                CoordinateDepShip3 = ship3.coordinateDepartureShip.Copy();
                CoordinateDepShip4 = ship4.coordinateDepartureShip.Copy();
                CoordinateDepShip5 = ship5.coordinateDepartureShip.Copy();
                ship1.reached = false;
                ship2.reached = false;
                ship3.reached = false;
                ship4.reached = false;
                ship5.reached = false;
                ship1.reacheIntermediate = false;
                ship2.reacheIntermediate = false;
                ship3.reacheIntermediate = false;
                ship4.reacheIntermediate = false;
                ship5.reacheIntermediate = false;
            }

            //Движение саможетов из одной точки пространства в другую 
            airplane1.straight_line_equation(CoordinateDep1, airplane1.coordinateArrivalAirplane);
            airplane2.straight_line_equation(CoordinateDep2, airplane2.coordinateArrivalAirplane);
            airplane3.straight_line_equation(CoordinateDep3, airplane3.coordinateArrivalAirplane);
            airplane4.straight_line_equation(CoordinateDep4, airplane4.coordinateArrivalAirplane);
            airplane5.straight_line_equation(CoordinateDep5, airplane5.coordinateArrivalAirplane);


            //Движение кораблей через промежуточную точку
            if (!ship1.reacheIntermediate)
            {
                ship1.straight_line_equation(CoordinateDepShip1, ship1.coordinateIntermediate);
            }
            else
            {
                ship1.straight_line_equation(ship1.coordinateIntermediate.Copy(), ship1.coordinateArrivalShip);
            }

            if (!ship2.reacheIntermediate)
            {
                ship2.straight_line_equation(CoordinateDepShip2, ship2.coordinateIntermediate);
            }
            else
            {
                ship2.straight_line_equation(ship2.coordinateIntermediate.Copy(), ship2.coordinateArrivalShip);
            }

            if (!ship3.reacheIntermediate)
            {
                ship3.straight_line_equation(CoordinateDepShip3, ship3.coordinateIntermediate);
            }
            else
            {
                ship3.straight_line_equation(ship3.coordinateIntermediate.Copy(), ship3.coordinateArrivalShip);
            }

            if (!ship4.reacheIntermediate)
            {
                ship4.straight_line_equation(CoordinateDepShip4, ship4.coordinateIntermediate);
            }
            else
            {
                ship4.straight_line_equation(ship4.coordinateIntermediate.Copy(), ship4.coordinateArrivalShip);
            }

            if (!ship5.reacheIntermediate)
            {
                ship5.straight_line_equation(CoordinateDepShip5, ship5.coordinateIntermediate);
            }
            else
            {
                ship5.straight_line_equation(ship5.coordinateIntermediate.Copy(), ship5.coordinateArrivalShip);
            }



            //перераспеделение координат для кораблей
            if (ship1.reached)
            {
                CoordinateDepShip1 = ship1.coordinateArrivalShip.Copy();
                ship1 = CreateShip(CoordinateDepShip1);
                ship1.reached = false;
                ship1.reacheIntermediate = false;
                CoordinateDepShip1 = ship1.coordinateDepartureShip.Copy();
            }
            if (ship2.reached)
            {
                CoordinateDepShip2 = ship2.coordinateArrivalShip.Copy();
                ship2 = CreateShip(CoordinateDepShip2);
                ship2.reached = false;
                ship2.reacheIntermediate = false;
                CoordinateDepShip2 = ship2.coordinateDepartureShip.Copy();
            }
            if (ship3.reached)
            {
                CoordinateDepShip3 = ship3.coordinateArrivalShip.Copy();
                ship3 = CreateShip(CoordinateDepShip3);
                ship3.reached = false;
                ship3.reacheIntermediate = false;
                CoordinateDepShip3 = ship3.coordinateDepartureShip.Copy();
            }
            if (ship4.reached)
            {
                CoordinateDepShip4 = ship4.coordinateArrivalShip.Copy();
                ship4 = CreateShip(CoordinateDepShip4);
                ship4.reached = false;
                ship4.reacheIntermediate = false;
                CoordinateDepShip4 = ship4.coordinateDepartureShip.Copy();
            }
            if (ship5.reached)
            {
                CoordinateDepShip5 = ship5.coordinateArrivalShip.Copy();
                ship5 = CreateShip(CoordinateDepShip5);
                ship5.reached = false;
                ship5.reacheIntermediate = false;
                CoordinateDepShip5 = ship5.coordinateDepartureShip.Copy();
            }

            //перераспределение координат для самолетов
            if (airplane1.reached)
            {
                CoordinateDep1 = airplane1.coordinateArrivalAirplane.Copy();
                airplane1 = CreateAirplane(CoordinateDep1);
                airplane1.reached = false;
                CoordinateDep1 = airplane1.coordinateDepartureAirplane.Copy();
            }
            if (airplane2.reached)
            {
                CoordinateDep2 = airplane2.coordinateArrivalAirplane.Copy();
                airplane2 = CreateAirplane(CoordinateDep1);
                airplane2.reached = false;
                CoordinateDep2 = airplane2.coordinateDepartureAirplane.Copy();
            }
            if (airplane3.reached)
            {
                CoordinateDep3 = airplane3.coordinateArrivalAirplane.Copy();
                airplane3 = CreateAirplane(CoordinateDep3);
                airplane3.reached = false;
                CoordinateDep3 = airplane3.coordinateDepartureAirplane.Copy();
            }
            if (airplane4.reached)
            {
                CoordinateDep4 = airplane4.coordinateArrivalAirplane.Copy();
                airplane4 = CreateAirplane(CoordinateDep4);
                airplane4.reached = false;
                CoordinateDep4 = airplane4.coordinateDepartureAirplane.Copy();
            }
            if (airplane5.reached)
            {
                CoordinateDep5 = airplane5.coordinateArrivalAirplane.Copy();
                airplane5 = CreateAirplane(CoordinateDep5);
                airplane5.reached = false;
                CoordinateDep5 = airplane5.coordinateDepartureAirplane.Copy();
            }

        }
    }


    [CustomStyle(
        @"new ol.style.Style({
            image: new ol.style.Circle({
                opacity: 1.0,
                scale: 1.0,
                radius: 3,
                fill: new ol.style.Fill({
                    color: 'rgba(255, 0, 255, 0.4)'
                }),
                stroke: new ol.style.Stroke({
                    color: 'rgba(0, 0, 0, 0.4)',
                    width: 1
                }),
            })
        });
        ")]
    public class Airplane : Point // Унаследуем данный данный класс от стандартной точки.
    {
        public bool reached = true; // Показыает достиг ли самолет точки назначения
        public double Speed { get; } // Скорость самолета
        public Coordinate coordinateArrivalAirplane; // Координаты точки откуда самолет вылетел
        public Coordinate coordinateDepartureAirplane; // координаты точки куда самолет вылетел

        public Airplane(Coordinate coordinate, double speed, Coordinate coordinateArrival) : base(coordinate)
        {
            coordinateArrivalAirplane = coordinateArrival;
            coordinateDepartureAirplane = coordinate;
            Speed = speed;
        }
        /// <summary>
        /// Метод для движения самолета из одной точки в другую по уравнению прямой
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="coordinateArrival"></param>
        public void straight_line_equation(Coordinate coordinate, Coordinate coordinateArrival)
        {
            if (!reached)
            {
                if (X > coordinateArrival.X)
                {
                    X -= Speed;
                }
                else
                {
                    X += Speed;
                }
                Y = (-(coordinate.Y - coordinateArrival.Y) * X - (coordinate.X * coordinateArrival.Y - coordinateArrival.X * coordinate.Y)) / (coordinateArrival.X - coordinate.X);
            }

            if (Y < coordinateArrival.Y + 8000 && X < coordinateArrival.X + 8000 && Y < coordinateArrival.Y + 8000 && X > coordinateArrival.X - 8000 && Y > coordinateArrival.Y - 8000 && X > coordinateArrival.X - 8000 && Y > coordinateArrival.Y - 8000 && X < coordinateArrival.X + 8000)
            {
                reached = true;
            }
        }
    }


    [CustomStyle(
       @"new ol.style.Style({
            image: new ol.style.Circle({
                opacity: 3.0,
                scale: 3.0,
                radius: 6,
                fill: new ol.style.Fill({
                    color: 'rgba(255, 0, 255, 0.4)'
                }),
                stroke: new ol.style.Stroke({
                    color: 'rgba(0, 0, 0, 0.4)',
                    width: 1
                }),
            })
        });
        ")]
    class Airport : Point
    {
        public Airport(Coordinate coordinate) : base(coordinate)
        {
        }
    }

    [CustomStyle(
      @"new ol.style.Style({
            image: new ol.style.Circle({
                opacity: 3.0,
                scale: 3.0,
                radius: 6,
                fill: new ol.style.Fill({
                    color: 'Blue'
                }),
                stroke: new ol.style.Stroke({
                    color: 'rgba(0, 0, 0, 0.4)',
                    width: 1
                }),
            })
        });
        ")]
    class Port : Point
    {
        public Port(Coordinate coordinate) : base(coordinate)
        {
        }
    }

    [CustomStyle(
      @"new ol.style.Style({
            image: new ol.style.Circle({
                opacity: 1.0,
                scale: 1.0,
                radius: 3,
                fill: new ol.style.Fill({
                    color: 'Blue'
                }),
                stroke: new ol.style.Stroke({
                    color: 'rgba(0, 0, 0, 0.4)',
                    width: 1
                }),
            })
        });
        ")]

    public class Ship : Point
    {
        public bool reached = true; //показыает достиг ли корабль точки назначения
        public bool reacheIntermediate = false; //показывает достиг ли корабль промежуточной точки
        public double Speed { get; }//скорость корабля
        public Coordinate coordinateArrivalShip;//координата точки назначения
        public Coordinate coordinateIntermediate;//координаты промежуточной точки
        public Coordinate coordinateDepartureShip;//координаты точки отправления корабля


        /// <summary>
        /// Конструктор который автоматически определяет промежуточную точку при инициализации экземпляра класса
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="speed"></param>
        /// <param name="coordinateArrival"></param>
        public Ship(Coordinate coordinate, double speed, Coordinate coordinateArrival) : base(coordinate)
        {
            coordinateArrivalShip = coordinateArrival;
            coordinateDepartureShip = coordinate;
            Speed = speed;
            if (coordinateDepartureShip.Equals2D(new Coordinate(-3991847, -596820)) && (coordinateArrivalShip.Equals2D(new Coordinate(-4520180, -2299226)) || coordinateArrivalShip.Equals2D(new Coordinate(1193641, -381574)) || coordinateArrivalShip.Equals2D(new Coordinate(2074195, -3982063))))
            {
                coordinateIntermediate = new Coordinate(-1406727, -1496943);
            }
            else if (coordinateDepartureShip.Equals2D(new Coordinate(-3991847, -596820)) && coordinateArrivalShip.Equals2D(new Coordinate(12914800, -4138606)))
            {
                coordinateIntermediate = new Coordinate(-703147, -5899716);
            }

            else if (coordinateDepartureShip.Equals2D(new Coordinate(-4520180, -2299226)) && (coordinateArrivalShip.Equals2D(new Coordinate(-3991847, -596820)) || coordinateArrivalShip.Equals2D(new Coordinate(1193641, -381574)) || coordinateArrivalShip.Equals2D(new Coordinate(2074195, -3982063))))
            {
                coordinateIntermediate = new Coordinate(-1506727, -1356943);
            }
            else if (coordinateDepartureShip.Equals2D(new Coordinate(-4520180, -2299226)) && coordinateArrivalShip.Equals2D(new Coordinate(12914800, -4138606)))
            {
                coordinateIntermediate = new Coordinate(-763147, -5099716);
            }

            else if (coordinateDepartureShip.Equals2D(new Coordinate(1193641, -381574)) && (coordinateArrivalShip.Equals2D(new Coordinate(-3991847, -596820)) || coordinateArrivalShip.Equals2D(new Coordinate(-4520180, -2299226)) || coordinateArrivalShip.Equals2D(new Coordinate(2074195, -3982063))))
            {
                coordinateIntermediate = new Coordinate(-1606727, -1496943);
            }
            else if (coordinateDepartureShip.Equals2D(new Coordinate(1193641, -381574)) && coordinateArrivalShip.Equals2D(new Coordinate(12914800, -4138606)))
            {
                coordinateIntermediate = new Coordinate(-803147, -5899716);
            }

            else if (coordinateDepartureShip.Equals2D(new Coordinate(2074195, -3982063)) && (coordinateArrivalShip.Equals2D(new Coordinate(-3991847, -596820)) || coordinateArrivalShip.Equals2D(new Coordinate(1193641, -381574)) || coordinateArrivalShip.Equals2D(new Coordinate(-4520180, -2299226))))
            {
                coordinateIntermediate = new Coordinate(-1526727, -1496943);
            }
            else if (coordinateDepartureShip.Equals2D(new Coordinate(2074195, -3982063)) && coordinateArrivalShip.Equals2D(new Coordinate(12914800, -4138606)))
            {
                coordinateIntermediate = new Coordinate(-763147, -6000716);
            }

            else if (coordinateDepartureShip.Equals2D(new Coordinate(12914800, -4138606)))
            {
                coordinateIntermediate = new Coordinate(-763147, -5899716);
            }
        }

        /// <summary>
        ///  Метод для движения корабля из одной точки в другую по уравнению прямой
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="coordinateArrival"></param>
        public void straight_line_equation(Coordinate coordinate, Coordinate coordinateArrival)
        {
            if (!reached)
            {
                if (X > coordinateArrival.X)
                {
                    X -= Speed;
                }
                else
                {
                    X += Speed;
                }
                Y = (-(coordinate.Y - coordinateArrival.Y) * X - (coordinate.X * coordinateArrival.Y - coordinateArrival.X * coordinate.Y)) / (coordinateArrival.X - coordinate.X);
            }

            if (!reacheIntermediate && Y < coordinateArrival.Y + 2000 && X < coordinateArrival.X + 2000 && Y < coordinateArrival.Y + 2000 && X > coordinateArrival.X - 2000 && Y > coordinateArrival.Y - 2000 && X > coordinateArrival.X - 2000 && Y > coordinateArrival.Y - 2000 && X < coordinateArrival.X + 2000)
            {
                reacheIntermediate = true;
            }
            else if (reacheIntermediate && Y < coordinateArrival.Y + 2000 && X < coordinateArrival.X + 2000 && Y < coordinateArrival.Y + 2000 && X > coordinateArrival.X - 2000 && Y > coordinateArrival.Y - 2000 && X > coordinateArrival.X - 2000 && Y > coordinateArrival.Y - 2000 && X < coordinateArrival.X + 2000)
            {
                reached = true;
            }
        }
    }


}
