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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Productcrudbywindowsform
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1:write query
                string qry = "insert into Product values(@pname ,@pprice)";
               
                cmd = new SqlCommand(qry, con);
               

                cmd.Parameters.AddWithValue("@pname", txtname.Text);
                cmd.Parameters.AddWithValue("@pprice", txtprice.Text);
              
                //fire the query
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res >= 1)
                {
                    MessageBox.Show("Product added successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1:write query
                string qry = "update  Product set pname=@pname,pprice=@pprice where pid=@pid";
               
                cmd = new SqlCommand(qry, con);
               

                cmd.Parameters.AddWithValue("@pname", txtname.Text);
                cmd.Parameters.AddWithValue("@pprice", txtprice.Text);
                cmd.Parameters.AddWithValue("@pid", txtid.Text);


                //fire the query
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res >= 1)
                {
                    MessageBox.Show("Product Updated successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "select * from Product where pid=@pid";
                //step 2
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@pid", txtid.Text);
                //step3 exceute query
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtname.Text =dr["pname"].ToString();
                        txtprice.Text = dr["pprice"].ToString();
                      

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
            finally
            {
                con.Close();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

                // step1 - write query
                string qry = "delete from Product where pid=@pid";
               
                cmd = new SqlCommand(qry, con);
                // assign values to parameters
                cmd.Parameters.AddWithValue("@pid", txtid.Text);
                // fire the query
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Product deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Product";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                
                DataTable table = new DataTable();
                table.Load(dr);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
