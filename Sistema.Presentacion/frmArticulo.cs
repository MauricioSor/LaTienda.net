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
using BarcodeStandard;
using SkiaSharp;
using BarcodeLib;
using System.Drawing.Imaging;
using System.IO;
namespace Sistema.Presentacion
{
    public partial class frmArticulo : Form
    {
        private string _nombreAnt;
        private string _rutaOrigen;
        private string _directorio = "D:\\sistema\\";
        private string _rutaDestino;
        public frmArticulo()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Listar();
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
                DgvListado.DataSource = NArticulo.Buscar(txtBuscar.Text);
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
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[3].HeaderText = "Categoría";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Código";
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio Venta";
            DgvListado.Columns[7].Width = 60;
            DgvListado.Columns[8].Width = 200;
            DgvListado.Columns[8].HeaderText = "Imagen";
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;
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
            _rutaDestino = "";
            _rutaDestino = "";
            PicImagen.Image = null;
            pnlCodigo.BackgroundImage = null;
            TxtStock.Clear();
            txtImagen.Clear();
            TxtPrecioVenta.Clear();
               
            DgvListado.Columns[0].Visible=false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            btnEliminar.Visible = false;
            chkSeleccionar.Checked=false;

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
                    rpta = NArticulo.Insertar(Convert.ToInt32(cboCategoria.SelectedValue), txtNombre.Text.Trim(), TxtCodigo.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta), Convert.ToInt32(TxtStock.Text), txtImagen.Text.Trim(), txtDescripcion.Text.Trim());
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            tabGeneral.SelectedIndex = 0;
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                this._nombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
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
                    rpta = NArticulo.Actualizar(Convert.ToInt32(txtNombre.Text), Convert.ToInt32(cboCategoria.SelectedValue), this._nombreAnt, TxtCodigo.Text, Convert.ToDecimal(TxtPrecioVenta), txtImagen.Text.Trim(), Convert.ToInt32(TxtStock.Text), txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
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
            if (chkSeleccionar.Checked)
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
                Opcion = MessageBox.Show("Deseas realmente eliminar el registro seleccionado?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = NArticulo.Eliminar(Codigo);
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

            }
            catch (Exception ex)
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
                            rpta = NArticulo.Activar(Codigo);
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
                            rpta = NArticulo.Desactivar(Codigo);
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

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoria();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            this.Buscar();
        }
        private void CargarCategoria()
        {
            try
            {
                cboCategoria.DataSource = NCategoria.Seleccionar();
                cboCategoria.ValueMember = "idcategoria";
                cboCategoria.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image Files (*.jpg, *.jpeg, *jpeg, *.jfif, *.png) | *.jpg; *.jpeg;";
            if (file.ShowDialog() == DialogResult.OK)
            {
                PicImagen.Image = Image.FromFile(file.FileName);
                txtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                this._rutaOrigen = file.FileName;

            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            pnlCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, TxtCodigo.Text, Color.White, Color.Black, 400, 100);
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)pnlCodigo.BackgroundImage.Clone();
            SaveFileDialog dialogGuardar = new SaveFileDialog();
            dialogGuardar.AddExtension = true;
            dialogGuardar.Filter = "Image PNG (*.png)|*.png";
            dialogGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(dialogGuardar.FileName))
            {
                imgFinal.Save(dialogGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void btnInsertar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (cboCategoria.Text == string.Empty||txtNombre.Text==string.Empty ||TxtStock.Text==string.Empty||TxtPrecioVenta.Text==string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    ErrorIcono.SetError(cboCategoria, "Seleccione una categoria");
                    ErrorIcono.SetError(TxtStock, "Ingresar stock.");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingresar Precio de venta.");
                    ErrorIcono.SetError(txtNombre, "Ingresar un nombre.");
                }
                else
                {
                    rpta = NArticulo.Insertar(Convert.ToInt32(cboCategoria.SelectedValue),txtNombre.Text.Trim(),TxtCodigo.Text,Convert.ToDecimal(TxtPrecioVenta.Text),Convert.ToInt32(TxtStock.Text),(txtImagen.Text).Trim(),txtDescripcion.Text.Trim());
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se insertó de forma correcta el registro");
                        if (txtImagen.Text != string.Empty) { 
                            this._rutaOrigen = this._directorio + txtImagen.Text;
                            File.Copy(this._rutaOrigen, this._rutaDestino);
                        }
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
    }
}
