using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Problems.Images
{
    public class ProblemImageInfo
    {
        public Uri ImgLocation { get; private set; }
        public string Answer { get; private set; }

        public ProblemImageInfo(Uri loc, string answer)
        {
            ImgLocation = loc;
            Answer = answer;
        }

        public ProblemImageInfo(string relativeLoc, string answer)
        {
            ImgLocation = new Uri(relativeLoc, UriKind.Relative);
            Answer = answer;
        }
    }

    public static class ProblemImages
    {
        private static Random _random = new Random(GlobalRandom.Next(24,144));
        private const string defaultDir = "/TrianglesVCircles;component/Problems/Images/Assets/";

        private static string fromDir(string file)
        {
            return Path.Combine(defaultDir, file);
        }

        public static readonly ProblemImageInfo[] EasyImages =
        {
            new ProblemImageInfo(fromDir("cat.jpg"), "cat"),
            new ProblemImageInfo(fromDir("dog.jpg"), "dog"),
            new ProblemImageInfo(fromDir("rat.jpg"), "rat"), 
            new ProblemImageInfo(fromDir("kite.jpg"), "kite"),
            new ProblemImageInfo(fromDir("sun.jpg"), "sun"),
            new ProblemImageInfo(fromDir("car.jpg"), "car"), 
            new ProblemImageInfo(fromDir("ufo.jpg"), "ufo"),
            new ProblemImageInfo(fromDir("bed.jpg"), "bed"),
            new ProblemImageInfo(fromDir("bee.jpg"), "bee"),
            new ProblemImageInfo(fromDir("beer.jpg"), "beer"),
            new ProblemImageInfo(fromDir("bell.jpg"), "bell"),
            new ProblemImageInfo(fromDir("bird.jpg"), "bird"),
            new ProblemImageInfo(fromDir("book.jpg"), "book"),
            new ProblemImageInfo(fromDir("cake.jpg"), "cake"),
            new ProblemImageInfo(fromDir("cow.jpg"), "cow"),
            new ProblemImageInfo(fromDir("crab.jpg"), "crab"),
            new ProblemImageInfo(fromDir("dice.jpg"), "dice"),
            new ProblemImageInfo(fromDir("duck.jpg"), "duck"),
            new ProblemImageInfo(fromDir("fish.jpg"), "fish"),
            new ProblemImageInfo(fromDir("five.jpg"), "five"),
            new ProblemImageInfo(fromDir("four.jpg"), "four"),
            new ProblemImageInfo(fromDir("frog.jpg"), "frog"),
            new ProblemImageInfo(fromDir("hat.jpg"), "hat"),
            new ProblemImageInfo(fromDir("nail.jpg"), "nail"),
            new ProblemImageInfo(fromDir("nine.jpg"), "nine"),
            new ProblemImageInfo(fromDir("one.jpg"), "one"),
            new ProblemImageInfo(fromDir("pan.jpg"), "pan"),
            new ProblemImageInfo(fromDir("rain.jpg"), "rain"),
            new ProblemImageInfo(fromDir("shoe.jpg"), "shoe"),
            new ProblemImageInfo(fromDir("six.jpg"), "six"),
            new ProblemImageInfo(fromDir("surf.jpg"), "surf"),
            new ProblemImageInfo(fromDir("two.jpg"), "two"),
            new ProblemImageInfo(fromDir("wolf.jpg"), "wolf"),

        };

        public static readonly ProblemImageInfo[] MediumImages =
        {
            new ProblemImageInfo(fromDir("apple.jpg"), "apple"),
            new ProblemImageInfo(fromDir("camel.jpg"), "camel"),
            new ProblemImageInfo(fromDir("camera.jpg"), "camera"),
            new ProblemImageInfo(fromDir("cherry.jpg"), "cherry"),
            new ProblemImageInfo(fromDir("coffee.jpg"), "coffee"),
            new ProblemImageInfo(fromDir("cookie.jpg"), "cookie"),
            new ProblemImageInfo(fromDir("donkey.jpg"), "donkey"),
            new ProblemImageInfo(fromDir("eight.jpg"), "eight"),
            new ProblemImageInfo(fromDir("hammer.jpg"), "hammer"),
            new ProblemImageInfo(fromDir("hippo.jpg"), "hippo"),
            new ProblemImageInfo(fromDir("house.jpg"), "house"),
            new ProblemImageInfo(fromDir("knife.jpg"), "knife"),
            new ProblemImageInfo(fromDir("lemon.jpg"), "lemon"),
            new ProblemImageInfo(fromDir("ghost.jpg"), "ghost"),
            new ProblemImageInfo(fromDir("grape.jpg"), "grape"),
            new ProblemImageInfo(fromDir("rocket.jpg"), "rocket"),
            new ProblemImageInfo(fromDir("seven.jpg"), "seven"),
            new ProblemImageInfo(fromDir("skunk.jpg"), "skunk"),
            new ProblemImageInfo(fromDir("spider.jpg"), "spider"),
            new ProblemImageInfo(fromDir("sword.jpg"), "sword"),
            new ProblemImageInfo(fromDir("three.jpg"), "three"),
            new ProblemImageInfo(fromDir("turtle.jpg"), "turtle"),
            new ProblemImageInfo(fromDir("witch.jpg"), "witch"),
            new ProblemImageInfo(fromDir("zebra.jpg"), "zebra")
        };

        public static readonly ProblemImageInfo[] HardImages =
        {
            new ProblemImageInfo(fromDir("avocado.jpg"), "avocado"),
            new ProblemImageInfo(fromDir("backpack.jpg"), "backpack"),
            new ProblemImageInfo(fromDir("battery.jpg"), "battery"),
            new ProblemImageInfo(fromDir("dinosaur.jpg"), "dinosaur"),
            new ProblemImageInfo(fromDir("dolphin.jpg"), "dolphin"),
            new ProblemImageInfo(fromDir("hamburger.jpg"), "hamburger"),
            new ProblemImageInfo(fromDir("lightbulb.jpg"), "lightbulb"),
            new ProblemImageInfo(fromDir("lightning.jpg"), "lightning"),
            new ProblemImageInfo(fromDir("mountain.jpg"), "mountain"),
            new ProblemImageInfo(fromDir("pomegranate.jpg"), "pomegranate"),
            new ProblemImageInfo(fromDir("pumpkin.jpg"), "pumpkin"),
            new ProblemImageInfo(fromDir("rainbow.jpg"), "rainbow"),
            new ProblemImageInfo(fromDir("strawberry.jpg"), "strawberry"),
            new ProblemImageInfo(fromDir("snowman.jpg"), "snowman"),
            new ProblemImageInfo(fromDir("toothbrush.jpg"), "toothbrush"),
            new ProblemImageInfo(fromDir("tornado.jpg"), "tornado"),
            new ProblemImageInfo(fromDir("watermelon.jpg"), "watermelon"),
        };

        public static ProblemImageInfo Pick(this ProblemImageInfo[] src)
        {
            var pos = _random.Next(0, src.Length);
            return src[pos];
        }
    }
}
