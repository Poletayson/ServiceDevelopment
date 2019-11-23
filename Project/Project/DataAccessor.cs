using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.db;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Project.DataAccessor
{
    public class MaterialDataAccessor
    {
        public void Read (AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand ("SELECT * FROM material", c.get(), t.get());
            a.Fill(ds, "material");
        }
        public void Update (AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM material"; 
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO material (name_material, code_material) VALUES (@name_material, @code_material)";
            adapter.InsertCommand = new MySqlCommand (sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "name_material",
                ParameterName = "@name_material"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "code_material",
                ParameterName = "@code_material"
            });

            //UPDATE
            String sqlUpdate = "Update material SET name_material=@name_material, code_material=@code_material where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "name_material",
                ParameterName = "@name_material"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "code_material",
                ParameterName = "@code_material"
            });

            //DELETE
            String sqlDelete = "DELETE FROM material WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder (adapter);

            adapter.Update(ds, "material");
        }

    }

    public class UnitsDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM unit_of_measurement", c.get(), t.get());
            a.Fill(ds, "unit_of_measurement");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM unit_of_measurement"; 
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO unit_of_measurement (code, unit_name, national_symbol, code_letter) VALUES (@code, @unit_name, @national_symbol, @code_letter)";
            adapter.InsertCommand = new MySqlCommand (sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "code",
                ParameterName = "@code"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "unit_name",
                ParameterName = "@unit_name"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "national_symbol",
                ParameterName = "@national_symbol"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "code_letter",
                ParameterName = "@code_letter"
            });

            //UPDATE
            String sqlUpdate = "Update unit_of_measurement SET code=@code, unit_name=@unit_name, national_symbol=@national_symbol, code_letter=@code_letter where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "code",
                ParameterName = "@code"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "unit_name",
                ParameterName = "@unit_name"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "national_symbol",
                ParameterName = "@national_symbol"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "code_letter",
                ParameterName = "@code_letter"
            });


            //DELETE
            String sqlDelete = "DELETE FROM unit_of_measurement WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder (adapter);

            adapter.Update(ds, "unit_of_measurement");
        }

    }
    public class OrganizationDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM organization", c.get(), t.get());
            a.Fill(ds, "organization");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM organization";
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO organization (okpo_number, organization_name) VALUES (@okpo_number, @organization_name)";
            adapter.InsertCommand = new MySqlCommand(sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "okpo_number",
                ParameterName = "@okpo_number"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "organization_name",
                ParameterName = "@organization_name"
            });


            //UPDATE
            String sqlUpdate = "Update organization SET okpo_number=@okpo_number, organization_name=@organization_name where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "okpo_number",
                ParameterName = "@okpo_number"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "organization_name",
                ParameterName = "@organization_name"
            });

            //DELETE
            String sqlDelete = "DELETE FROM organization WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(adapter);

            adapter.Update(ds, "organization");
        }

    }
    public class InvoiceDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM invoice", c.get(), t.get());
            a.Fill(ds, "invoice");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM invoice";
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO invoice (invoice_number, date_of_creation, organization_id) VALUES (@invoice_number, @date_of_creation, @organization_id)";
            adapter.InsertCommand = new MySqlCommand(sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "invoice_number",
                ParameterName = "@invoice_number"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "date_of_creation",
                ParameterName = "@date_of_creation"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "organization_id",
                ParameterName = "@organization_id"
            });

            //UPDATE
            String sqlUpdate = "UPDATE invoice SET invoice_number=@invoice_number, date_of_creation=@date_of_creation, organization_id=@organization_id where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "invoice_number",
                ParameterName = "@invoice_number"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "date_of_creation",
                ParameterName = "@date_of_creation"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "organization_id",
                ParameterName = "@organization_id"
            });

            //DELETE
            String sqlDelete = "DELETE FROM invoice WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(adapter);

            adapter.Update(ds, "invoice");
        }

    }
    public class PositionDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM position_in_mat_delivery_note", c.get(), t.get());
            a.Fill(ds, "position_in_mat_delivery_note");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM position_in_mat_delivery_note";
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO position_in_mat_delivery_note (amount, invoice_id, material_id, unit_of_measurement_id) VALUES (@amount, @invoice_id, @material_id, @unit_of_measurement_id)";
            adapter.InsertCommand = new MySqlCommand(sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "amount",
                ParameterName = "@amount"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "invoice_id",
                ParameterName = "@invoice_id"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "material_id",
                ParameterName = "@material_id"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "unit_of_measurement_id",
                ParameterName = "@unit_of_measurement_id"
            });

            //UPDATE
            String sqlUpdate = "UPDATE position_in_mat_delivery_note SET amount=@amount, invoice_id=@invoice_id, material_id=@material_id, unit_of_measurement_id=@unit_of_measurement_id where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "amount",
                ParameterName = "@amount"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "invoice_id",
                ParameterName = "@invoice_id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "material_id",
                ParameterName = "@material_id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "unit_of_measurement_id",
                ParameterName = "@unit_of_measurement_id"
            });


            //DELETE
            String sqlDelete = "DELETE FROM position_in_mat_delivery_note WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(adapter);

            adapter.Update(ds, "position_in_mat_delivery_note");
        }

    }
}
