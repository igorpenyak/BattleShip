﻿using System.CodeDom;
using BattleShip.GameEngine.Arsenal.Flot;
using BattleShip.GameEngine.Arsenal.Flot.RectangleShips;
using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Fields.Cells;
using BattleShip.GameEngine.Fields.Cells.StatusCell;
using BattleShip.GameEngine.Location;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngineTest.Field.Cells
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void Init()
        {
            var pos = new Position(3, 5);
            var cell = new Cell(pos);

            Assert.IsTrue(cell.Location == pos);

            Assert.IsTrue(cell.Show() == typeof(EmptyCell));

            Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(EmptyCell));
        }

        [TestMethod]
        public void IsWasAttackActually()
        {
            var pos = new Position(3, 5);
            var cell = new Cell(pos);

            Assert.IsTrue(cell.WasAttacked == false);

            var gun = new Gun();
            Position point = new Position();
            cell.Shot(gun, ref point);

            Assert.IsTrue(cell.WasAttacked);
        }

        [TestMethod]
        public void AddObject()
        {
            var pos = new Position(3, 5);
            var cell = new Cell(pos);

            var ship = new OneStoreyRectangleShip(0, pos);

            cell.AddGameObject(ship, true);

            Assert.IsTrue(cell.Show() == typeof(EmptyCell));

            Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(OneStoreyRectangleShip));

            var gun = new Gun();

            cell.Shot(gun, ref pos);

            Assert.IsTrue(cell.Show() == typeof(OneStoreyRectangleShip));

            Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(OneStoreyRectangleShip));
        }

        [TestMethod]
        public void Shot()
        {
            var pos = new Position(3, 5);
            var cell = new Cell(pos);

            ShipBase ship = new OneStoreyRectangleShip(0, pos);

            cell.AddGameObject(ship, true);

            var gun = new Gun();

            var result = cell.Shot(gun, ref pos);

            Assert.IsTrue(result == typeof(OneStoreyRectangleShip));

            Assert.IsTrue(cell.Show() == typeof(OneStoreyRectangleShip));
            Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(OneStoreyRectangleShip));
        }

        [TestMethod]
        public void ShotWithProtectSimpleGun()
        {
            var pos = new Position(3, 5);
            var cell = new Cell(pos);

            var ship = new OneStoreyRectangleShip(0, pos);

            var pvo = new Pvo(0, pos, 10);

            cell.AddGameObject(pvo, true);

            var gun = new Gun();

            Assert.IsTrue(cell.Show() == typeof(EmptyCell));

            Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(Pvo));

            var result = cell.Shot(gun, ref pos);

            Assert.IsTrue(result == typeof(Pvo));

            Assert.IsTrue(cell.Show() == typeof(Pvo));
            Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(Pvo));
        }

        //[TestMethod]
        //public void AddStatus()
        //{
        //    var pos = new Position(3, 5);
        //    var cell = new Cell(pos);

        //    cell.AddStatus(new AroundShip(pos));

        //    Assert.IsTrue(cell.Show() == typeof(EmptyCell));
        //    Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(AroundShip));

        //    var gun = new Gun();
        //    var t = cell.Shot(gun);

        //    Assert.IsTrue(t == typeof(AroundShip));

        //    Assert.IsTrue(cell.GetTypeOfCellObject() == typeof(AroundShip));
        //}

        //[TestMethod]
        //public void ShotWithProtectNotSimleGun()
        //{
        //    var pos = new Position(3, 5);
        //    var cell = new Cell(pos);

        //    var pvo = new Pvo(0, pos, 10);

        //    cell.SetProtect(pvo);

        //    var gun = new Gun();

        //    gun.ChangeCurrentGun(new PlaneDestroy());

        //    var result = cell.Shot(gun);

        //    Assert.IsTrue(cell.WasAttacked == false);

        //    Assert.IsTrue(result == typeof(ProtectedCell));
        //}

        [TestMethod]
        public void ShotWithProtectSimleGun()
        {
            var pos = new Position(3, 5);
            var cell = new Cell(pos);

            var pvo = new Pvo(0, pos, 10);

            cell.AddGameObject(pvo, false);

            var gun = new Gun();

            gun.ChangeCurrentGun(new GunDestroy());

            var result = cell.Shot(gun, ref pos);

            Assert.IsTrue(cell.WasAttacked == true);

            Assert.IsTrue(result == typeof(Pvo));
        }
    }
}