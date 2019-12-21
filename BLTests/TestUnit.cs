﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

using Project;
using Project.DataAccessor;
using Project.db;
using BL;
using BLTests.Extend;
using NUnit.Framework;



namespace BLTests
{
    [TestFixture]
    public class TestUnit
    {
        private BusinessLogic BL = new BusinessLogic();
        [SetUp]
        public void Setup()
        {
            TableInit.init();
        }

        [Test]
        public void GetAll()
        {
            DataSet1 dataSet = BL.getUnits();

            List<DataRow> list = dataSet.unit_of_measurement.Select().OfType<DataRow>().ToList();
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));  //сортируем по ид

            Assert.That(list.Count, Is.EqualTo(15));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["unit_name"]), Is.EqualTo("Миллиметр"));
            Assert.That((string)(list[0]["code"]), Is.EqualTo("003"));
            Assert.That((string)(list[0]["national_symbol"]), Is.EqualTo("мм"));
        }

        [Test]
        public void unit_of_measurementById()
        {
            DataSet1 dataSet = BL.getUnits();

            List<DataRow> list = dataSet.unit_of_measurement.Select("id = 4").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["unit_name"]), Is.EqualTo("Миллиметр"));
            Assert.That((string)(list[0]["code"]), Is.EqualTo("003"));
            Assert.That((string)(list[0]["national_symbol"]), Is.EqualTo("мм"));
        }

        [Test]
        public void unit_of_measurementByName()
        {
            DataSet1 dataSet = BL.getUnits();

            List<DataRow> list = dataSet.unit_of_measurement.Select("unit_name = 'Миллиметр'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["unit_name"]), Is.EqualTo("Миллиметр"));
            Assert.That((string)(list[0]["code"]), Is.EqualTo("003"));
            Assert.That((string)(list[0]["national_symbol"]), Is.EqualTo("мм"));
        }

        [Test]
        public void unit_of_measurementByCode()
        {
            DataSet1 dataSet = BL.getUnits();
            List<DataRow> list = dataSet.unit_of_measurement.Select("code = '003'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["unit_name"]), Is.EqualTo("Миллиметр"));
            Assert.That((string)(list[0]["code"]), Is.EqualTo("003"));
            Assert.That((string)(list[0]["national_symbol"]), Is.EqualTo("мм"));
        }

        [Test]
        public void unit_of_measurementUpdate()
        {
            DataSet1 dataSet = BL.getUnits();
            List<DataRow> list = dataSet.unit_of_measurement.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));


            // Обновляем первую запись
            DataSet1.unit_of_measurementRow oldM = null;
            AbstractConnection connectionN = null;
            AbstractTransaction transactionN = null;
            String oldName = "";

            oldM = dataSet.unit_of_measurement[0];
            oldName = oldM.unit_name;
            dataSet.unit_of_measurement[0].unit_name = oldM.unit_name + "_changed";
            BL.updateUnits(dataSet);


            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getUnits();
            List<DataRow> list_3 = dataSetUpdated.unit_of_measurement.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(15));

            // Достаем ту же запись
            List<DataRow> rows_list = dataSet.unit_of_measurement.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.unit_of_measurementRow updatedM = dataSetUpdated.unit_of_measurement[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));

            Assert.That(oldName, !Is.EqualTo(updatedM.unit_name));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.unit_name));
        }

        [Test]
        public void unit_of_measurementAdd()
        {
            DataSet1 dataSetRead = BL.getUnits();

            int countRowBefore = 0;

            List<DataRow> rows_list = dataSetRead.unit_of_measurement.Select("").OfType<DataRow>().ToList();
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            countRowBefore = rows_list.Count();

            //Добавляем в базу новую запись
            List<DataRow> list_1 = dataSetRead.unit_of_measurement.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            ///            
            DataRow rowForAdded = dataSetRead.unit_of_measurement.NewRow();

            rowForAdded["unit_name"] = "Миллиметр";
            rowForAdded["code"] = "003";
            rowForAdded["national_symbol"] = "мм";

            dataSetRead.unit_of_measurement.Rows.Add(rowForAdded);

            List<DataRow> list_2 = dataSetRead.unit_of_measurement.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            BL.updateUnits(dataSetRead);

            // Новый коннекшн, проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterInsert = BL.getUnits();

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.unit_of_measurement.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            int countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1));

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["unit_name"], Is.EqualTo(rowAfterInsert["unit_name"]));
            Assert.That(rowForAdded["code"], Is.EqualTo(rowAfterInsert["code"]));
            Assert.That(rowForAdded["national_symbol"], Is.EqualTo(rowAfterInsert["national_symbol"]));
        }

        [Test]
        public void unit_of_measurementDelete()
        {
            DataSet1 dataSetRead = BL.getUnits();

            List<DataRow> rows_list = dataSetRead.unit_of_measurement.Select("unit_name = 'Миллиметр'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.unit_of_measurement.Select("unit_name = 'Миллиметр'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                rowForDel.Delete();
            }
            BL.updateUnits(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше
            DataSet1 dataSet_AfterDel = BL.getUnits();
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.unit_of_measurement.Select("unit_name = 'Миллиметр'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}
