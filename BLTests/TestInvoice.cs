using System;
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
    public class TestInvoice
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
            DataSet1 dataSet = BL.getInvoices();

            List<DataRow> list = dataSet.invoice.Select().OfType<DataRow>().ToList();
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));  //сортируем по ид

            Assert.That(1, Is.EqualTo(1));
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["invoice_number"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["organization_id"]), Is.EqualTo(2));
        }

        [Test]
        public void invoiceById()
        {
            DataSet1 dataSet = BL.getInvoices();
            List<DataRow> list = dataSet.invoice.Select("id = 1").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["invoice_number"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["organization_id"]), Is.EqualTo(2));
        }

        [Test]
        public void invoiceByName()
        {
            DataSet1 dataSet = BL.getInvoices();
            List<DataRow> list = dataSet.invoice.Select("invoice_number = '000001'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["invoice_number"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["organization_id"]), Is.EqualTo(2));
        }

        [Test]
        public void invoiceByCode()
        {
            DataSet1 dataSet = BL.getInvoices();
            List<DataRow> list = dataSet.invoice.Select("organization_id = '2'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["invoice_number"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["organization_id"]), Is.EqualTo(2));
        }

        [Test]
        public void invoiceUpdate()
        {
            DataSet1 dataSet = BL.getInvoices();
            List<DataRow> list = dataSet.invoice.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));

            // Обновляем первую запись
            DataSet1.invoiceRow oldM = dataSet.invoice[0];
            String oldName = oldM.invoice_number;

            dataSet.invoice[0].invoice_number = oldM.invoice_number + "_changed";
            BL.updateInvoices(dataSet);


            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getInvoices();
           
            List<DataRow> list_3 = dataSetUpdated.invoice.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(2));

            // Достаем ту же запись
            List<DataRow> rows_list = dataSet.invoice.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.invoiceRow updatedM = dataSetUpdated.invoice[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));
            Assert.That(oldName, !Is.EqualTo(updatedM.invoice_number));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.invoice_number));

        }

        [Test]
        public void invoiceAdd()
        {
            DataSet1 dataSetRead = BL.getInvoices();
           
            List<DataRow> rows_list = dataSetRead.invoice.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            int countRowBefore = rows_list.Count();
     
            //Добавляем в базу новую запись
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.invoice.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
     
            DataRow rowForAdded = dataSetRead.invoice.NewRow();

            rowForAdded["invoice_number"] = "000000";
            rowForAdded["date_of_creation"] = "2011-02-20";
            rowForAdded["organization_id"] = "2";

            dataSetRead.invoice.Rows.Add(rowForAdded);

            List<DataRow> list_2 = dataSetRead.invoice.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            BL.updateInvoices(dataSetRead);

            // Новый коннекшн, проверяем что теперь записей стало на одну больше
            DataSet1 dataSet_AfterInsert = BL.getInvoices();

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.invoice.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            int countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1)); ///!!!!!!!!

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["invoice_number"], Is.EqualTo(rowAfterInsert["invoice_number"]));
            Assert.That(rowForAdded["organization_id"], Is.EqualTo(rowAfterInsert["organization_id"]));
        }

        [Test]
        public void invoiceDelete()
        {
            DataSet1 dataSetRead = BL.getInvoices();

            List<DataRow> rows_list = dataSetRead.invoice.Select("invoice_number = '000001'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.invoice.Select("invoice_number = '000001'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                rowForDel.Delete();
            }
            BL.updateInvoices(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше
            DataSet1 dataSet_AfterDel = BL.getInvoices();
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.invoice.Select("invoice_number = '000001'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}
