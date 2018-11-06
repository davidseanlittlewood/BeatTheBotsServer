using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.Classes;
using BattleOfTheBots.State;
using BattleOfTheBots.Logic;

namespace BattleOfTheBots.HTTP
{
    internal class HTTPUtility
    {
        public static string SendStartInstruction(string url, string opponentBotName, int startPosition, int health, int arenaSize, int flips, int flipOdds, int fuel, char direction)
        {

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/start", url));

                var postData = string.Format("opponentName={0}&health={1}&arenaSize={2}&flips={3}&flipOdds={4}&fuel={5}&direction={6}&startPosition={7}",
                    opponentBotName,
                    health,
                    arenaSize,
                    flips,
                    flipOdds,
                    fuel,
                    direction,
                    startPosition);

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

        internal static string PostMove(string url, string move)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/move", url));


                var data = Encoding.ASCII.GetBytes(move);

                request.Timeout = 5000; 
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

        internal static string PostFlipped(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/flipped", url));


                var data = Encoding.ASCII.GetBytes("TRUE");

                request.Timeout = 5000; 
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
        internal static string PostOpponentFlipped(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/opponentflipped", url));


                var data = Encoding.ASCII.GetBytes("TRUE");

                request.Timeout = 5000;
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

        internal static Move GetMove(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/move", url));

                request.Timeout = 5000;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";



                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return (Move)Enum.Parse(typeof(Move), responseString);
            }
            catch
            {
                return Move.Invalid;
            }
        }
    }
}
