using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseName
{
    class Logger
    {
        private System.Windows.Forms.Label logger;
        public Logger(System.Windows.Forms.Label logger)
        {
            this.logger = logger;
        }
        public void Log(string message, bool error = false)
        {
            logger.Text = message;
            logger.ForeColor = error ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }
        public void AddLog(string message)
        {
            logger.Text = logger.Text + " | " + message;
        }
        public void Clear()
        {
            logger.Text = "";
        }
    }
}
