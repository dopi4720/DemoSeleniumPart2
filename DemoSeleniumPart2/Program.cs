using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSeleniumPart2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.w3schools.com/html/default.asp");
            //var img_Element = driver.FindElement(By.Id("img_mylearning"));
            string Script = @"let text = '';
function getBase64Image(url) {
  var promise = new Promise(function(resolve, reject) {

    var img = new Image();
    img.crossOrigin = 'Anonymous'; 
    img.onload = function() {
      var canvas = document.createElement('canvas');
      canvas.width = img.width;
      canvas.height = img.height;
      var ctx = canvas.getContext('2d');
      ctx.drawImage(img, 0, 0);
      var dataURL = canvas.toDataURL('image/png');
      resolve(dataURL.replace(/^data:image\/(png|jpg|jpeg|pdf);base64,/, ''));
    };  
    img.src = url;      
  });

  return promise;
};

var url = 'https://www.w3schools.com/images/mylearning.png';
var promise = getBase64Image(url);

await promise.then(function (dataURL) {
  text = dataURL
});
return text";
            string Base64Image = (string)((IJavaScriptExecutor)driver).ExecuteScript(Script);
            var img = LoadImage(Base64Image);
            img.Save("image.png");
        }
        public static Image LoadImage(string Base64Image)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(Base64Image);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }
}
