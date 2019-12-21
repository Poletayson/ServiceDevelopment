using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;
using Project.DataAccessor;
using Project.db;

namespace BL
{
    public class BusinessLogic
    {
        static void Main()
        {
        }

        public DataSet1 getMaterials(){
            MaterialDataAccessor DA = new MaterialDataAccessor();
            PositionDataAccessor DApos = new PositionDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Read(absConnection, absTransaction, dataSet1);
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateMaterials(DataSet1 dataSet1)
        {
            MaterialDataAccessor DA = new MaterialDataAccessor();
            PositionDataAccessor DApos = new PositionDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getUnits()
        {
            UnitsDataAccessor DA = new UnitsDataAccessor();
            PositionDataAccessor DApos = new PositionDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Read(absConnection, absTransaction, dataSet1);
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateUnits(DataSet1 dataSet1)
        {
            UnitsDataAccessor DA = new UnitsDataAccessor();
            PositionDataAccessor DApos = new PositionDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getOrganizations()
        {
            OrganizationDataAccessor DA = new OrganizationDataAccessor();
            InvoiceDataAccessor DAinvoice = new InvoiceDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Read(absConnection, absTransaction, dataSet1);
                DAinvoice.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateOrganizations(DataSet1 dataSet1)
        {
            OrganizationDataAccessor DA = new OrganizationDataAccessor();
            InvoiceDataAccessor DAinvoice = new InvoiceDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DAinvoice.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getInvoices()
        {
            InvoiceDataAccessor DA = new InvoiceDataAccessor();
            PositionDataAccessor DApos = new PositionDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Read(absConnection, absTransaction, dataSet1);
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateInvoices(DataSet1 dataSet1)
        {
            InvoiceDataAccessor DA = new InvoiceDataAccessor();
            PositionDataAccessor DApos = new PositionDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getPositions()
        {
            PositionDataAccessor DApos = new PositionDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updatePositions(DataSet1 dataSet1)
        {
            PositionDataAccessor DApos = new PositionDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }
    }
}
