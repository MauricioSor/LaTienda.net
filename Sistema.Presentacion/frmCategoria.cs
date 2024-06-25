using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class frmCategoria : Form
    {
        private string _nombreAnt;
        public frmCategoria()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NCategoria.Listar();
                this.Formato();
                this.Limpiar();
                lblTotal.Text = "Total registros:" + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NCategoria.Buscar(txtBuscar.Text);
                this.Formato();
                lblTotal.Text = "Total registros:" + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].Width = 150;
            DgvListado.Columns[3].Width = 400;
            DgvListado.Columns[4].HeaderText = "Estado";
            DgvListado.Columns[4].Width= 100;
        }
        private void frmCategoria_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }
        private void Limpiar()
        {
            txtBuscar.Clear();
            txtNombre.Clear();
            txtId.Clear();
            txtDescripcion.Clear();
            ErrorIcono.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            DgvListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            btnEliminar.Visible = false;
            chkSeleccionar.Checked = false;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seraan remarcados");
                    ErrorIcono.SetError(txtNombre, "Ingresar un nombre.");
                }
                else
                {
                    rpta = NCategoria.Insertar(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se insertó de forma correcta el registro");
                        this.Limpiar();
                        this.Listar();

                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            tabGeneral.SelectedIndex=0;
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
            this.Limpiar();
            btnActualizar.Visible = true;
            btnInsertar.Visible = false;
            this._nombreAnt= Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
            txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
            txtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
            txtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
            tabGeneral.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione uno de los registros");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (txtNombre.Text == string.Empty || txtId.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seraan remarcados");
                    ErrorIcono.SetError(txtNombre, "Ingresar un nombre.");
                }
                else
                {
                    rpta = NCategoria.Actualizar(Convert.ToInt32(txtId.Text),this._nombreAnt,txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se actualizó de forma correctamente el registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Deseas realmente eliminar el registro seleccionado?","Sistema de ventas",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if(Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string rpta = "";
                    
                    foreach(DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = NCategoria.Eliminar(Codigo);
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeOk(rpta);
                            }
                        }
                        this.Listar();                       
                    }
                }
                else
                {

                }

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Deseas realmente activar el registro seleccionado?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = NCategoria.Activar(Codigo);
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se activó el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeOk(rpta);
                            }
                        }
                        this.Listar();
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Deseas realmente desactivar el registro seleccionado?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = NCategoria.Desactivar(Codigo);
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivó el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeOk(rpta);
                            }
                        }
                        this.Listar();
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
