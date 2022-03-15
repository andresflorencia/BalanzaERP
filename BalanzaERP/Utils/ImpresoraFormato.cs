using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace BalanzaERP.Utils
{
    public class ImpresoraFormato
    {
        public string Impresora { get; set; }
        public int NumColumn { get; set; }
        public short NumCopias { get; set; }
        public List<TextoFormato> ListImpresion { get; set; }
        public Image image { get; set; }

        private PrinterSettings prtSettings = new PrinterSettings();
        private PrintDocument prtDoc = new PrintDocument();
        public ImpresoraFormato(string Impresora, int NumColumn, short NumCopias)
        {
            this.Impresora = Impresora;
            this.NumColumn = NumColumn;
            this.NumCopias = NumCopias;
            this.ListImpresion = new List<TextoFormato>();
        }

        public void ConfiguraImpresora() {
            if (prtSettings == null) {
                prtSettings = new PrinterSettings();
            }
            prtSettings.PrinterName = this.Impresora;
            prtSettings.Copies = this.NumCopias;
            prtDoc.PrinterSettings = prtSettings;
            prtDoc.PrintPage += new PrintPageEventHandler(print_PrintPage);
        }

        private void print_PrintPage(object sender, PrintPageEventArgs e) {

            short xPos = 8;
            // La fuente a usar
            var prFont = new Font("Arial", 14, FontStyle.Bold);
            var prFontDetalle = new Font("Arial", 10, FontStyle.Regular);
            var prFontDetalleN = new Font("Arial", 12, FontStyle.Bold);
            // la posición superior
            var yPos = prFont.GetHeight(e.Graphics) - 12;
            if (image != null) {
                e.Graphics.DrawImage(image, xPos, yPos, 190, 35);
                yPos += prFontDetalle.GetHeight(e.Graphics) + 7;
            }
            // imprimimos la cadena
            for (var i = 0; i < ListImpresion.Count; i++)
            {
                yPos += prFontDetalle.GetHeight(e.Graphics) - (i == 1 ? 2 : 4);
                e.Graphics.DrawString(ListImpresion[i].Linea, ListImpresion[i].Font, Brushes.Black, xPos + 5, yPos);
            }
            e.HasMorePages = false;
        }

        public void Imprimir() {
            prtDoc.Print();
        }
        public class TextoFormato {
            public string Linea { get; set; }
            public Font Font { get; set; }

            public TextoFormato(string Linea, int SizeFont, FontStyle Style) {
                this.Linea = Linea;
                this.Font = new Font("Arial", SizeFont, Style);
            }
        }
    }
}
