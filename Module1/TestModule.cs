using System;
using NetTopologySuite.Geometries;
using OSMLSGlobalLibrary;
using OSMLSGlobalLibrary.Map;
using OSMLSGlobalLibrary.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1
{
    public class TestModule : OSMLSModule
    {

        Airport airport1;
        Airport airport2;
        Airport airport3;
        Airport airport4;
        Airport airport5;

        protected override void Initialize()
        {
            var airport1Coordinate = MathExtensions.LatLonToSpherMerc(-34.831747, -56.020034);
            var airport2Coordinate = new Coordinate(4942686, 6234338);
            var airport3Coordinate = new Coordinate(-8579292, 4700571);
            var airport4Coordinate = new Coordinate(15547903, 4259071);
            var airport5Coordinate = new Coordinate(5241746, -2178150);
            
            airport1 = new Airport(airport1Coordinate);
            airport2 = new Airport(airport2Coordinate);
            airport3 = new Airport(airport3Coordinate);
            airport4 = new Airport(airport4Coordinate);
            airport5 = new Airport(airport5Coordinate);

            MapObjects.Add(airport1);
            MapObjects.Add(airport2);
            MapObjects.Add(airport3);
            MapObjects.Add(airport4);
            MapObjects.Add(airport5);
        }

        public Airplane CreateAirplane(Coordinate coordinateAirport)
        {
            Airplane airplane;
            Random rnd = new Random();
            Coordinate coordinateDeparture = coordinateAirport;
            double speed = rnd.Next(10000, 100000);
            Coordinate coordinateArrival = new Coordinate(4942686, 6234338);
            airplane = new Airplane(coordinateDeparture, speed, coordinateArrival);
            MapObjects.Add(airplane);
            return airplane;
        }
        /// <summary>
        /// Вызывается постоянно, здесь можно реализовывать логику перемещений и всего остального, требующего времени.
        /// </summary>
        /// <param name="elapsedMilliseconds">TimeNow.ElapsedMilliseconds</param>
        public override void Update(long elapsedMilliseconds)
        {

            CreateAirplane(MathExtensions.LatLonToSpherMerc(-34.831747, -56.020034)).MoveUpRight();
            CreateAirplane(new Coordinate(4942686, 6234338)).MoveUpRight();
            CreateAirplane(new Coordinate(-8579292, 4700571)).MoveUpRight();
            CreateAirplane(new Coordinate(15547903, 4259071)).MoveUpRight();
            CreateAirplane(new Coordinate(5241746, -2178150)).MoveUpRight();
        }
    }

    #region объявления класса, унаследованного от точки, объекты которого будут иметь уникальный стиль отображения на карте

    /// <summary>
    /// Самолет, умеющий летать вверх-вправо с заданной скоростью.
    /// </summary>
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
        ")] // Переопределим стиль всех объектов данного класса, сделав самолет фиолетовым, используя атрибут CustomStyle.
    public class Airplane : Point // Унаследуем данный данный класс от стандартной точки.
    {

        public double Speed { get; }


        public Airplane(Coordinate coordinate, double speed, Coordinate coordinateArrival) : base(coordinate)
        {
            Speed = speed;
        }

        /// <summary>
        /// Двигает самолет вверх-вправо.
        /// </summary>
        public void MoveUpRight()
        {
            X += Speed;
            Y += Speed;
        }
    }
    #endregion


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
}
