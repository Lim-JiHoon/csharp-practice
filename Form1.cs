
using System.Data;
using System.Diagnostics;
using WinFormsApp3.Common.Mysql;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();


            button2.Click += button2_Click!;
            //downloadButton.Clicked += async (o, e) =>
            //{ var stringData = await _httpClient.GetStringAsync(URL); DoSomethingWithData(stringData); };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            Stopwatch sw = new();
            sw.Start();
            List<Task<string>> list = new();

            for (var i = 0; i < 100; i++)
            {
                var ttask = await _httpClient.GetStringAsync("https://www.naver.com/");
                //list.Add(_httpClient.GetStringAsync("https://www.naver.com/"));
            }

            foreach (var task in list)
            {
                await task;
            }
            //Console.WriteLine(stringData);

            MessageBox.Show(sw.Elapsed.ToString());

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            Stopwatch sw = new();
            sw.Start();
            //var a = Add1("a");
            //var b = Add1("b");
            //var c = Add1("c");
            //var d = Add1("d");
            //var _e = Add1("e");
            //var f = Add1("f");
            //var g = Add1("g");


            //await a;
            //await b;
            //await c;
            //await d;
            //await _e;
            //await f;
            //await g;

            var list = new List<Task<DataTable>>();
            for (var i = 0; i < 100; i++)
            {
                var taskDataTable = await GetDataTable();
            }


            //var list = new List<Task<DataTable>>();
            //for (var i = 0; i < 100; i++)
            //{
            //    var taskDataTable = GetDataTable();
            //    list.Add(taskDataTable);
            //}

            //while (list.Any())
            //{
            //    var fin = await Task.WhenAny(list);
            //    list.Remove(fin);                
            //}



            //foreach (var task in list)
            //{
            //    var dt = await task;
            //    Console.WriteLine(dt.ToString());
            //}

            //while (list.Any())
            //{
            //    var finished = await Task.WhenAny(list);
            //    if (finished == dt1)
            //    {
            //        var dt = await finished;
            //    }else if (finished == dt2)
            //    {
            //        var dt = await finished;
            //    }
            //    list.Remove(finished);
            //}


            MessageBox.Show(sw.Elapsed.ToString());
        }


        private async Task<DataTable> GetDataTable()
        {
            return await Task.Run(async Task<DataTable>? () =>
            {
                DataTable? dt = null;
                using MySqlEx sqlX = new();
                {
                    dt = await sqlX.GetDT("SELECT * FROM test");
                }
                return dt;
            });
        }

        private async Task Add1(string a)
        {
            await Task.Run(async () =>
            {
                using MySqlEx sqlX = new();
                {
                    //var dt = await sqlX.GetDT("SELECT * FROM test");
                    for (int i = 0; i < 1000; i++)
                    {
                        //await Task.Yield();
                        await sqlX.Execute($"INSERT INTO test(a,b,c) VALUES('{a}{i}','{a}{i + 1}','{a}{i + 2}')");
                    }

                    //Console.WriteLine(dt.ToString());
                    return;
                }
            });
        }






    }
}
