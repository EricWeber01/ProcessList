using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ProcessList
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Form1 : Form
    {
        public Form1()
        {
            Load += FormLoad;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            var listbox = new ListBox() { Dock = DockStyle.Fill };
            Controls.Add(listbox);

            var timer = new Timer() { Interval = 1000 };
            timer.Tick += (o, args) =>
            {
                var processInfo = Process.GetProcesses();
                var results = processInfo.Select(p => (object)string.Format("Name: {0}; ID: {1}", p.ProcessName, p.Id)).ToArray();
                Invoke(new Action(() =>
                {
                    listbox.Items.Clear();
                    listbox.Items.AddRange(results);
                }));
            };
            timer.Start();
        }
    }
}