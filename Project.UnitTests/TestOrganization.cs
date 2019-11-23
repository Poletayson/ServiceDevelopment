using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

using Project;
using Project.DataAccessor;
using Project.db;
using DATests.Extend;
using NUnit.Framework;



namespace DATests
{
    [TestFixture]
    public class TestOrganization
    {
        [SetUp]
        public void Setup()
        {
            TableInit.init();
        }

        [Test]
        public void GetAll()
        {
            //Setup();
            OrganizationDataAccessor dalOrganization = new OrganizationDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                dalOrganization.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.organization.Select().OfType<DataRow>().ToList();
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));  //сортируем по ид

            Assert.That(1, Is.EqualTo(1));
            Assert.That(list.Count, Is.EqualTo(5));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));  
        }

        [Test]
        public void organizationById()
        {
            OrganizationDataAccessor dalOrganization = new OrganizationDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                dalOrganization.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.organization.Select("id = 2").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));  
        }

        [Test]
        public void organizationByName()
        {
            OrganizationDataAccessor dalOrganization = new OrganizationDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                dalOrganization.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));
        }

        [Test]
        public void organizationByCode()
        {
            OrganizationDataAccessor dalOrganization = new OrganizationDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                dalOrganization.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.organization.Select("okpo_number = '47828137'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["organization_name"]), Is.EqualTo("ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\""));
            Assert.That((int)(list[0]["okpo_number"]), Is.EqualTo(47828137));
        }

        [Test]
        public void organizationUpdate()
        {
            OrganizationDataAccessor dalOrganization = new OrganizationDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                dalOrganization.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));


            // Обновляем первую запись
            DataSet1.organizationRow oldM = null;
            AbstractConnection connectionN = null;
            AbstractTransaction transactionN = null;
            String oldName = "";
            try
            {
                connectionN = DBFactory.createConnection();
                connectionN.open();
                transactionN = connectionN.beginTransaction();
                oldM = dataSet1.organization[0];
                oldName = oldM.organization_name;
//                oldCarDTO = new CarDTO(dataSet1.car[0]);

                dataSet1.organization[0].organization_name = oldM.organization_name + "_changed";
                dalOrganization.Update(connectionN, transactionN, dataSet1);
                transactionN.commit();
            }
            catch (Exception e)
            {
                transactionN.rollback();
            }
            finally
            {
                connectionN.close();
            }


            // Заново читаем из базы, проверяем, что поменялось
            OrganizationDataAccessor daUpdated = new OrganizationDataAccessor();
            DataSet1 dataSetUpdated = new DataSet1();
            AbstractConnection connectionUpdated = null;
            AbstractTransaction transactionUpdated = null;
            try
            {
                connectionUpdated = DBFactory.createConnection();
                connectionUpdated.open();
                transactionUpdated = connectionUpdated.beginTransaction();
                daUpdated.Read(connectionUpdated, transactionUpdated, dataSetUpdated);
                transactionUpdated.commit();
            }
            catch (Exception e)
            {
                transactionUpdated.rollback();
            }
            finally
            {
                connectionUpdated.close();
            }

            // достаем из датасета все записи таблицы
            List<DataRow> list_3 = dataSetUpdated.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(5));

            // Достае ту же запись
            List<DataRow> rows_list = dataSet1.organization.Select("id = " + oldM.id).OfType<DataRow>().ToList();
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
            DataSet1 dataSetRead = new DataSet1();
            OrganizationDataAccessor dalOrganization = new OrganizationDataAccessor();
            AbstractConnection absCon_Read = null;
            AbstractTransaction absTran_Read = null;
            int countRowBefore = 0;
            try
            {
                absCon_Read = DBFactory.createConnection();
                absCon_Read.open();
                absTran_Read = absCon_Read.beginTransaction();
                dalOrganization.Read(absCon_Read, absTran_Read, dataSetRead);

                List<DataRow> rows_list = dataSetRead.organization.Select("").OfType<DataRow>().ToList();
                // Сортируем строки по id в порядке возрастания
                rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
                // Количество записей до внесения новой
                countRowBefore = rows_list.Count();
                absTran_Read.commit();
            }
            catch (Exception e)
            {
                absTran_Read.rollback();
            }
            finally
            {
                absCon_Read.close();
            }


            // НОВОЕ СОЕДИНЕНИЕ, Добавляем в базу новую запись
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
///            
            DataRow rowForAdded = dataSetRead.organization.NewRow();


            rowForAdded["organization_name"] = "ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ2\"";
            rowForAdded["okpo_number"] = "47828138";

            dataSetRead.organization.Rows.Add(rowForAdded);

            List<DataRow> list_2 = dataSetRead.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            
            try
            {
                absCon_Update = DBFactory.createConnection();
                absCon_Update.open();
                absTran_Update = absCon_Update.beginTransaction();
                dalOrganization.Update(absCon_Update, absTran_Update, dataSetRead);

                absTran_Update.commit();
            }
            catch (Exception e)
            {
                absTran_Update.rollback();
            }
            finally
            {
                absCon_Update.close();
            }


            // Новый коннекшн, проверяем что теперь записей стало на одну больше
            AbstractConnection absCon_AfterInsert = null;
            AbstractTransaction absTran_AfterInsert = null;
            DataSet1 dataSet_AfterInsert = new DataSet1();
            OrganizationDataAccessor organizationDA_AfterInsert = new OrganizationDataAccessor();
            int countRowAfter = 0;
            try
            {
                absCon_AfterInsert = DBFactory.createConnection();
                absCon_AfterInsert.open();
                absTran_AfterInsert = absCon_AfterInsert.beginTransaction();
                organizationDA_AfterInsert.Read( absCon_AfterInsert, absTran_AfterInsert,dataSet_AfterInsert);
                absTran_AfterInsert.commit();
            }
            catch (Exception e)
            {
                absTran_AfterInsert.commit();
            }
            finally
            {
                absCon_AfterInsert.close();
            }

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.organization.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            countRowAfter = rows_list_AfterInsert.Count();

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
            DataSet1 dataSetRead = new DataSet1();
            OrganizationDataAccessor dalOrganization = new OrganizationDataAccessor();
            AbstractConnection absCon_Read = null;
            AbstractTransaction absTran_Read = null;
            int countRowBefore = 0;
            try
            {
                absCon_Read = DBFactory.createConnection();
                absCon_Read.open();
                absTran_Read = absCon_Read.beginTransaction();
                dalOrganization.Read(absCon_Read, absTran_Read, dataSetRead);

                List<DataRow> rows_list = dataSetRead.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();
                // Сортируем строки по id в порядке возрастания
                rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
                // Количество записей до удаления
                countRowBefore = rows_list.Count();
                absTran_Read.commit();
            }
            catch (Exception e)
            {
                absTran_Read.rollback();
            }
            finally
            {
                absCon_Read.close();
            }

            Assert.That(countRowBefore, Is.EqualTo(1));


            // НОВОЕ СОЕДИНЕНИЕ, удаляем
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();

            foreach (DataRow rowForDel in list_1)
            {
                dataSetRead.organization.Rows.Remove(rowForDel);
            }
            

            try
            {
                absCon_Update = DBFactory.createConnection();
                absCon_Update.open();
                absTran_Update = absCon_Update.beginTransaction();
                dalOrganization.Update(absCon_Update, absTran_Update, dataSetRead);

                absTran_Update.commit();
            }
            catch (Exception e)
            {
                absTran_Update.rollback();
            }
            finally
            {
                absCon_Update.close();
            }


            // Новый коннекшн, проверяем что теперь записей стало на одну больше
            AbstractConnection absCon_AfterInsert = null;
            AbstractTransaction absTran_AfterInsert = null;
            DataSet1 dataSet_AfterInsert = new DataSet1();
            OrganizationDataAccessor organizationDA_AfterInsert = new OrganizationDataAccessor();
            try
            {
                absCon_AfterInsert = DBFactory.createConnection();
                absCon_AfterInsert.open();
                absTran_AfterInsert = absCon_AfterInsert.beginTransaction();
                organizationDA_AfterInsert.Read(absCon_AfterInsert, absTran_AfterInsert, dataSet_AfterInsert);
                absTran_AfterInsert.commit();
            }
            catch (Exception e)
            {
                absTran_AfterInsert.commit();
            }
            finally
            {
                absCon_AfterInsert.close();
            }

            List<DataRow> rows_list_AfterInsert = dataSetRead.organization.Select("organization_name = 'ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ\"'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}