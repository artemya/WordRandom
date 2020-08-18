using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace first_server_dn
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public string RandomElement(string[] array)
        {
            var rand = new Random();
            int randomElement = rand.Next(0, array.Length);
            return array[randomElement];
        }


        public string[] Who (string host) {
            string[] who = {};
            string tempWho = GetHeader(host + "who");
            who = tempWho.Split("/*/");
            return who;
        }

        public string[] How (string host) {
            string[] how = {};
            string tempHow = GetHeader(host + "how");
            how = tempHow.Split("/*/");
            return how;
        }

        public string[] Does(string host) {
            string[] does = {};
            string tempDoes = GetHeader(host + "does");
            does = tempDoes.Split("/*/");
            return does;
        }

        public string[] What(string host) {
            string[] what = {};
            string tempWhat = GetHeader(host + "what");
            what = tempWhat.Split("/*/");
            return what;
        }
        public string GetHeaderInfo(string host)
        {
            long ellapledTicks = DateTime.Now.Ticks;

            string[] who = {}; 
            var thread1 = new Thread(
                () =>
                {
                    who = Who(host);
                });
            thread1.Start();
            thread1.Join();
           

            string[] how = {};
            var thread2 = new Thread(
                () =>
                {
                    how = How(host);
                });
            thread2.Start();
            thread2.Join();


            string[] does = {};
            var thread3 = new Thread(
                () =>
                {
                    does = Does(host);
                });
            thread3.Start();
            thread3.Join();


            string[] what = What(host);
            var thread4 = new Thread(
                () =>
                {
                    what = What(host);
                });
            thread4.Start();
            thread4.Join();


            string result = "";

            result += who[0] + " " + how[0] + " " + does[0] + " " + what[0] + "\n";

            result += who[0] + " Received from: " + who[1] + "\n";
            result += how[0] + " Received from: " + how[1] + "\n";
            result += does[0] + " Received from: " + does[1] + "\n";
            result += what[0] + " Received from: " + what[1] + "\n";
            ellapledTicks = DateTime.Now.Ticks - ellapledTicks;
            result += ellapledTicks;

            return result;
        }

        public string GetHeaderInfo2(string host)
        {
            long ellapledTicks = DateTime.Now.Ticks;
            string[] who = Who(host);            

            string[] how = How(host);

            string[] does = Does(host);

            string[] what = What(host);

            string result = "";
            result += who[0] + " " + how[0] + " " + does[0] + " " + what[0] + "\n";

            result += who[0] + " Received from: " + who[1] + "\n";
            result += how[0] + " Received from: " + how[1] + "\n";
            result += does[0] + " Received from: " + does[1] + "\n";
            result += what[0] + " Received from: " + what[1] + "\n";
            ellapledTicks = DateTime.Now.Ticks - ellapledTicks;
            result += ellapledTicks;
            return result;
        }
        public string GetHeader(string host)
        {
            string word = "";
            string header = "";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(host);
            HttpWebResponse myRes = (HttpWebResponse)myReq.GetResponse();
            using (StreamReader stream = new StreamReader(
                myRes.GetResponseStream()))
                {
                    word = stream.ReadToEnd();
                }
                header = myRes.Headers["InCamp-Student"];
                myRes.Close();
            return word + "/*/" + header;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public string[] who = {"Шрек", "Осёл", "Кот в сапогах"};
        public string[] how = {"ужасно", "харизматично", "по дурацки"};
        public string[] does = {"рычит", "танцует", "бьет"};
        public string[] what = {"танго", "людей", "код"};
        public string[] hosts = {"http://localhost:3000/"};

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string args)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {  
                endpoints.MapGet("/", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "ArtemYa");
                    await context.Response.WriteAsync("Hello");
                });
                endpoints.MapGet("/who", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "ArtemYa");
                    await context.Response.WriteAsync(RandomElement(who));
                });

                endpoints.MapGet("/how", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "ArtemYa");
                    await context.Response.WriteAsync(RandomElement(how));
                });

                endpoints.MapGet("/does", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "ArtemYa");
                    await context.Response.WriteAsync(RandomElement(does));
                });

                endpoints.MapGet("/what", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "ArtemYa");
                    await context.Response.WriteAsync(RandomElement(what));
                });

                endpoints.MapGet("/quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "ArtemYa");
                    await context.Response.WriteAsync($"{RandomElement(who)} {RandomElement(how)} {RandomElement(does)} {RandomElement(what)}");
                });

                endpoints.MapGet("/incamp18-quote", async context =>
                {
                    Console.WriteLine(args.Length);
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "ArtemYa");
                    await context.Response.WriteAsync(GetHeaderInfo(RandomElement(hosts)));
                });

            });
        }
    }
}
