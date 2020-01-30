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
        private System.Windows.Forms.Label logHistory;
        public Logger(System.Windows.Forms.Label logger, System.Windows.Forms.Label logHistory)
        {
            this.logger = logger;
            this.logHistory = logHistory;
            Log("Initiatint logger");
        }
        public void Log(string message, bool error = false)
        {
            string[] history = logHistory.Text.Split('\n');
            
            logHistory.Text += "\n" + logger.Text;
            logHistory.Height += 16;
            logHistory.Location = new System.Drawing.Point(logHistory.Location.X, logHistory.Location.Y - 16);
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
