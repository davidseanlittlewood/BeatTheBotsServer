using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.Classes;

namespace BattleOfTheBots.HTTP
{
    internal class HTTPUtility
    {
        //
        internal static string SendStartInstruction(BotClass bot, BotClass opponentBot,  int health, int arenaSize, int flips, int flipOdds, int fuel, char direction)
        {

            try
            {

                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/start", bot.Url));

                var postData = string.Format("opponentName={0}&health={1}&arenaSize={2}&flips={3}&flipOdds={4}&fuel={5}&direction={6}", opponentBot.Name, health, arenaSize, flips, flipOdds, fuel, direction);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.Timeout = 5000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;

            }
            catch 
            {
                return "failed";
            }
        }

        internal static string PostMove(BotClass bot, string move)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/move", bot.Url));


                var data = Encoding.ASCII.GetBytes(move);

                //request.Timeout = 5000; 
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch
            {
                return "failed";
            }
        }

        internal static string GetMove(BotClass bot)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/move", bot.Url));

                //request.Timeout = 5000;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";



                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch
            {
                return "failed";
            }
        }
    }
}
