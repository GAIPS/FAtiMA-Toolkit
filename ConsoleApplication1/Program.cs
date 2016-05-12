using SpeechEmotionRecognition;
using TextEmotionRecognition;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechEmotionRecognitionAsset emotionRecognition = new SpeechEmotionRecognitionAsset();
            TextEmotionRecognitionAsset textRecognition = new TextEmotionRecognitionAsset();

            //emotionRecognition.Test();
            textRecognition.Test();
        }
    }
}
