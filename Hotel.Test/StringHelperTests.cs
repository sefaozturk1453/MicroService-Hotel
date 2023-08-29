using Hotel.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Test
{
    public class StringHelperTests
    {

        [Test]
        public void Bosluklar_Silindi_Mi()
        {
            // Arrenge

            var str = "Muhammed     Ali";
            var beklenen = "Muhammed Ali";


            // Action

            var gerceklesen = StringHelper.BosluklariSil(str);



            // Assert

            Assert.AreEqual(beklenen, gerceklesen);

        }
    }
}
