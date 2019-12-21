using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

using Project;
using Project.DataAccessor;
using Project.db;
using BLTests.Extend;
using BL;
using NUnit.Framework;


//Тесты на бизнес-логику
namespace BLTests
{
    [TestFixture]
    public class TestMaterials
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
            DataSet1 dataSet = BL.getMaterials();
            List<DataRow> list = dataSet.material.Select().OfType<DataRow>().ToList();
            list.Sort((a, b) => ((int)a["id"]).CompareTo((int)b["id"]));

            Assert.That(list.Count, Is.EqualTo(8));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name_material"]), Is.EqualTo("Бревна буковые"));
            Assert.That((string)(list[0]["code_material"]), Is.EqualTo("02.20.12.112"));  
        }

        [Test]
        public void materialById()
        {
            DataSet1 dataSet1 = BL.getMaterials();
            List<DataRow> list = dataSet1.material.Select("id = 1").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name_material"]), Is.EqualTo("Бревна буковые"));
            Assert.That((string)(list[0]["code_material"]), Is.EqualTo("02.20.12.112"));  
        }

        [Test]
        public void materialByName()
        {
            DataSet1 dataSet1 = BL.getMaterials();
            List<DataRow> list = dataSet1.material.Select("name_material = 'Бревна буковые'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name_material"]), Is.EqualTo("Бревна буковые"));
            Assert.That((string)(list[0]["code_material"]), Is.EqualTo("02.20.12.112"));
        }

        [Test]
        public void materialByCode()
        {
            DataSet1 dataSet1 = BL.getMaterials();

            List<DataRow> list = dataSet1.material.Select("code_material = '02.20.12.112'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name_material"]), Is.EqualTo("Бревна буковые"));
            Assert.That((string)(list[0]["code_material"]), Is.EqualTo("02.20.12.112"));
        }

        [Test]
        public void materialUpdate()
        {
            DataSet1 dataSet1 = BL.getMaterials();

            List<DataRow> list = dataSet1.material.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            list.Sort((a, b) => ((int)a["id"]).CompareTo((int)b["id"]));

            // Обновляем первую запись
            DataSet1.materialRow oldM = null;
            String oldName = "";

            oldM = dataSet1.material[0];
            oldName = oldM.name_material;

            dataSet1.material[0].name_material = oldM.name_material + "_changed";
            BL.updateMaterials(dataSet1);



            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getMaterials();
            
            // достаем из датасета все записи таблицы
            List<DataRow> list_3 = dataSetUpdated.material.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(8));

            // Достаем ту же запись
            List<DataRow> rows_list = dataSet1.material.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.materialRow updatedM = dataSetUpdated.material[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));

            Assert.That(oldName, !Is.EqualTo(updatedM.name_material));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.name_material));

        }

        [Test]
        public void materialAdd()
        {
            DataSet1 dataSetRead = BL.getMaterials();

            int countRowBefore = 0;
            List<DataRow> rows_list = dataSetRead.material.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            countRowBefore = rows_list.Count();


            // НОВОЕ СОЕДИНЕНИЕ, Добавляем в базу новую запись
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.material.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
///            
            DataRow rowForAdded = dataSetRead.material.NewRow();

            rowForAdded["name_material"] = "Бревна буковые";
            rowForAdded["code_material"] = "02.20.12.112";

            dataSetRead.material.Rows.Add(rowForAdded);
            List<DataRow> list_2 = dataSetRead.material.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            
            BL.updateMaterials(dataSetRead);


            //проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterInsert = BL.getMaterials();

            int countRowAfter = 0;

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.material.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1));

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["name_material"], Is.EqualTo(rowAfterInsert["name_material"]));
            Assert.That(rowForAdded["code_material"], Is.EqualTo(rowAfterInsert["code_material"]));
        }

        [Test]
        public void materialDelete()
        {
            DataSet1 dataSetRead = BL.getMaterials();

            List<DataRow> rows_list = dataSetRead.material.Select("name_material = 'Бревна буковые'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.material.Select("name_material = 'Бревна буковые'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                //dataSetRead.material.Rows.Remove(rowForDel);
                rowForDel.Delete();
            }
            BL.updateMaterials(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterDel = BL.getMaterials();
            //BL.updateMaterials(dataSet_AfterInsert);
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.material.Select("name_material = 'Бревна буковые'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}
