using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetPackage;

//namespace SpeechEmotionRecognition
//{
//    public class SpeechEmotionRecognitionAsset : BaseAsset
//    {
//        public SpeechEmotionRecognitionAsset()
//        {
//        }

//        public void PlaceHolderMethod()
//        {
//            string request = "empty";
//            Dictionary<string, string> headers = new Dictionary<string, string>();

//            headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("username:password"));
//            headers["Content-Type"] = "application/xml";

//            WWW www = new WWW("https://www.l2f.inesc-id.pt/spa/services/dialogAct", System.Text.Encoding.ASCII.GetBytes(request), headers);
//            yield return www;
//            if (www.error != null)
//            {
//                yield break;
//            }

//            string o = www.text;
//            o = o.Remove(0, o.IndexOf("<result>") + 8);
//            o = o.Remove(o.IndexOf("</result>"), o.Length - o.IndexOf("</result>") - 1);
            
//        }
//    }
//}
