using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Productcrudbywindowsform
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private DataSet GetAllProducts()
        {
            string qry = "select * from Product";
            da = new SqlDataAdapter(qry, con);
           
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
          
            da.Fill(ds, "pro");
            return ds;

        }
        private void ClearFileds()
        {
            txtid.Clear();
            txtname.Clear();
            txtprice.Clear();


        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProducts();
              
                DataRow row = ds.Tables["pro"].NewRow();
                row["pname"] = txtname.Text;
                row["pprice"] = txtprice.Text;
               
              
                ds.Tables["pro"].Rows.Add(row);
                int result = da.Update(ds.Tables["pro"]);
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProducts();
                DataRow row = ds.Tables["pro"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    txtname.Text = row["pname"].ToString();
                    txtprice.Text = row["pprice"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProducts();
                DataRow row = ds.Tables["pro"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    
                    row["pname"] = txtname.Text;
                    row["pprice"] = txtprice.Text;
                   
                    int result = da.Update(ds.Tables["pro"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record updated");
                        ClearFileds();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            ds = GetAllProducts();
            dataGridView1.DataSource = ds.Tables["pro"];

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProducts();
                DataRow row = ds.Tables["pro"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["pro"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record deleted");
                        ClearFileds();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
