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



namespace BLTests
{
    [TestFixture]
    public class TestOrganization
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
            DataSet1 dataSet = BL.getOrganizations();
            List<DataRow> list = dataSet.organization.Select().OfType<DataRow>().ToList();
            list.Sort((a, b) => ((int)a["id"]).CompareTo((int)b["id"]));

            Assert.That(list.Count, Is.EqualTo(5));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));  
        }

        [Test]
        public void organizationById()
        {
            DataSet1 dataSet = BL.getOrganizations();
            List<DataRow> list = dataSet.organization.Select("id = 2").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));  
        }

        [Test]
        public void organizationByName()
        {
            DataSet1 dataSet = BL.getOrganizations();

            List<DataRow> list = dataSet.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));
        }

        [Test]
        public void organizationByCode()
        {
            DataSet1 dataSet = BL.getOrganizations();

            List<DataRow> list = dataSet.organization.Select("okpo_number = '47828137'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));
        }

        [Test]
        public void organizationUpdate()
        {
            DataSet1 dataSet = BL.getOrganizations();
            List<DataRow> list = dataSet.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));


            // Обновляем первую запись
            DataSet1.organizationRow oldM = null;

            String oldName = "";

            oldM = dataSet.organization[0];
            oldName = oldM.organization_name;

            dataSet.organization[0].organization_name = oldM.organization_name + "_changed";
            BL.updateOrganizations(dataSet);


            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getOrganizations();

            // достаем из датасета все записи таблицы
            List<DataRow> list_3 = dataSetUpdated.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(5));

            // Достае ту же запись
            List<DataRow> rows_list = dataSet.organization.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.organizationRow updatedM = dataSetUpdated.organization[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));

            Assert.That(oldName, !Is.EqualTo(updatedM.organization_name));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.organization_name));
        }

        [Test]
        public void organizationAdd()
        {
            DataSet1 dataSetRead = BL.getOrganizations();

            int countRowBefore = 0;

            List<DataRow> rows_list = dataSetRead.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            countRowBefore = rows_list.Count();


            // Добавляем в базу новую запись
            List<DataRow> list_1 = dataSetRead.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));       
            DataRow rowForAdded = dataSetRead.organization.NewRow();

            rowForAdded["organization_name"] = "ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ2\"";
            rowForAdded["okpo_number"] = "47828138";

            dataSetRead.organization.Rows.Add(rowForAdded);

            List<DataRow> list_2 = dataSetRead.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            BL.updateOrganizations(dataSetRead);

            // проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterInsert = BL.getOrganizations();

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            int countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1));

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["organization_name"], Is.EqualTo(rowAfterInsert["organization_name"]));
            Assert.That(rowForAdded["okpo_number"], Is.EqualTo(rowAfterInsert["okpo_number"]));
        }

        [Test]
        public void organizationDelete()
        {
            DataSet1 dataSetRead = BL.getOrganizations();

            List<DataRow> rows_list = dataSetRead.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                rowForDel.Delete();
            }
            BL.updateOrganizations(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterDel = BL.getOrganizations();
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}