using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win10Config.Interfaces;

namespace Win10Config.Utils
{
    class W10ListViewItem : ListViewItem
    {
        public IConfiguration config { get; set; }

        public W10ListViewItem(IConfiguration config) : base (new String[] { config.getDisplayName(), config.getDisplayValue() })
        {
            this.config = config;       
        }

        public void changeValue()
        {
            config.changeValue();
            this.SubItems[1].Text = config.getDisplayValue();
        }
    }
}
