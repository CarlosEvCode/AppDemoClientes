using BOL;
using DAL;
namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //Flujo UI -> BOL -> DAL (Entidad) -> | API REST -> DB |
            var http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7138/"),
                Timeout = TimeSpan.FromSeconds(15)
            };

            var dal = new ClienteDal(http);
            var bol = new ClienteBol(dal);
            Application.Run(new FrmCliente(bol));
        }
    }
}